namespace APIComparer
{
    using System.Collections.Generic;
    using System.Linq;

    public class ApiChanges
    {
        public string Version { get; }
        public bool NoLongerSupported { get; }
        public List<RemovedType> RemovedTypes { get; }
        public List<ChangedType> ChangedTypes { get; }

        ApiChanges()
        {
            NoLongerSupported = true;
            Version = "Current";
        }

        public static List<ApiChanges> FromDiff(Diff diff)
        {
            if (diff is EmptyDiff)
            {
                return new List<ApiChanges>
                {
                    new ApiChanges()
                };
            }

            var removedTypes = diff.LeftOrphanTypes
                .Where(t => t.IsPublic &&
                            !t.IsObsoleteWithError())
                .Select(td => new RemovedType(td))
                .ToList();

            var typesChangedToNonPublic = diff.MatchingTypeDiffs
                .Where(td => td.LeftType.IsPublic &&
                            !td.RightType.IsPublic &&
                            !td.LeftType.IsObsoleteWithError())
                .Select(td => new RemovedType(td.RightType, td.LeftType.HasObsoleteAttribute() ? td.LeftType.GetObsoleteInfo() : null))
                .ToList();

            var publicTypeDiffs = diff.MatchingTypeDiffs
                .Where(td => td.LeftType.IsPublic &&
                             td.RightType.IsPublic)
                .ToList();

            var obsoletedTypes = publicTypeDiffs
                .Where(td => !td.LeftType.IsObsoleteWithError() &&
                             td.RightType.HasObsoleteAttribute())
                .Select(td => td.RightType)
                .ToList();



            var currentObsoletes = obsoletedTypes
                .Where(o => o.IsObsoleteWithError())
                .Select(td => new RemovedType(td, td.GetObsoleteInfo()))
                .ToList();

            removedTypes.AddRange(typesChangedToNonPublic);
            removedTypes.AddRange(currentObsoletes);

            var typesWithMemberDiffs = publicTypeDiffs
              .Where(td => td.LeftType.IsPublic &&
              !td.LeftType.IsObsoleteWithError() &&
              td.HasDifferences())
              .ToList();

            var removedMethods = typesWithMemberDiffs
                .SelectMany(td => td.LeftOrphanMethods.Where(t => t.IsPublic && !t.IsObsoleteWithError()))
                .Select(md => new
                {
                    Version = "Current",
                    Type = md.DeclaringType,
                    RemovedMember = new ChangedType.RemovedMember(md)
                })
                .ToList();

            var methodsNotAvailableForUse = typesWithMemberDiffs
                .SelectMany(td => td.MatchingMethods
                    .Where(mm => mm.Left.IsPublic && !mm.Left.IsObsoleteWithError() && (!mm.Right.IsPublic || mm.Right.HasObsoleteAttribute()))
                    .Select(mm => mm.Right))
                .Select(rm => new
                {
                    Version = rm.HasObsoleteAttribute() ? rm.GetObsoleteInfo().TargetVersion : "Current",
                    Type = rm.DeclaringType,
                    RemovedMember = new ChangedType.RemovedMember(rm)
                })
                .ToList();

            var fieldsNotAvailableForUse = typesWithMemberDiffs
               .SelectMany(td => td.MatchingFields
                   .Where(mf => mf.Left.IsPublic && !mf.Left.IsObsoleteWithError() &&
                   (!mf.Right.IsPublic || mf.Right.HasObsoleteAttribute()))
                   .Select(mf => mf.Right))
               .Select(rm => new
               {
                   Version = rm.HasObsoleteAttribute() ? rm.GetObsoleteInfo().TargetVersion : "Current",
                   Type = rm.DeclaringType,
                   RemovedMember = new ChangedType.RemovedMember(rm)
               })
               .ToList();


            var removedFields = typesWithMemberDiffs
                .SelectMany(td => td.LeftOrphanFields.Where(t => t.IsPublic && !t.IsObsoleteWithError()))
                .Select(fd => new
                {
                    Version = "Current",
                    Type = fd.DeclaringType,
                    RemovedMember = new ChangedType.RemovedMember(fd)
                })
                .ToList();

            var changedEnumMembers = typesWithMemberDiffs
                .SelectMany(td => td.EnumFieldsWithChangedValue().Where(t => t.Left.IsPublic && !t.Left.IsObsoleteWithError()))
                .Select(fd => new
                {
                    Version = "Current",
                    Type = fd.Left.DeclaringType,
                    ChangedMember = new ChangedType.ChangedEnumMember(fd.Left, fd.Right)
                })
                .ToList();

            var changedEnumMemberChanges = changedEnumMembers
                .Where(ce => ce.Version == "Current")
                .GroupBy(ce => ce.Type)
                .Select(g => new ChangedType(g.Key, null, g.Select(e => e.ChangedMember).ToList()))
                .ToList();

            var removedMembers = removedFields
                .Concat(removedMethods)
                .Concat(methodsNotAvailableForUse)
                .Concat(fieldsNotAvailableForUse)
                .ToList();

            var changedTypesInCurrentVersion = removedMembers
                .Where(rm => rm.Version == "Current")
                .GroupBy(rm => rm.Type)
                .Select(g => new ChangedType(g.Key, g.Select(r => r.RemovedMember).ToList()))
                .ToList();

            var result = new List<ApiChanges>
            {
                new ApiChanges("Current", removedTypes, changedTypesInCurrentVersion.Concat(changedEnumMemberChanges).ToList())
            };

            var futureChanges = removedMembers
              .Where(rm => rm.Version != "Current")
              .GroupBy(rm => rm.Version)
              .ToList();

            var futureObsoletes = obsoletedTypes
                .Where(o => !o.IsObsoleteWithError())
                .Select(td => new
                {
                    Version = td.GetObsoleteInfo().TargetVersion,
                    RemovedType = new RemovedType(td, td.GetObsoleteInfo())
                })
                .GroupBy(rt => rt.Version)
                .ToList();

            var uniqueFutureVersions = futureObsoletes
                .Select(fo => fo.Key)
                .Concat(futureChanges.Select(fc => fc.Key))
                .Distinct();

            foreach (var futureVersion in uniqueFutureVersions)
            {
                var obsoletesForVersion = futureObsoletes.SingleOrDefault(fo =>
                fo.Key == futureVersion)?.Select(fo => fo.RemovedType).ToList()
                ?? new List<RemovedType>();

                var changesForVersion = futureChanges.SingleOrDefault(fc =>
         fc.Key == futureVersion)?
         .GroupBy(fc => fc.Type)
         .Select(fc => new ChangedType(fc.Key, fc.Select(rm => rm.RemovedMember).ToList())).ToList()
         ?? new List<ChangedType>();

                result.Add(new ApiChanges(futureVersion, obsoletesForVersion, changesForVersion));
            }

            return result;
        }

        ApiChanges(string version, List<RemovedType> removedTypes, List<ChangedType> changedTypes)
        {
            Version = version;
            RemovedTypes = removedTypes;
            ChangedTypes = changedTypes;
        }
    }
}