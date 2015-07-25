namespace APIComparer.Backend.Reporting
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Mono.Cecil;

    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class ViewModelBuilder
    {
        public static object Build(PackageDescription description, DiffedCompareSet[] diffedCompareSets)
        {
            return new
            {
                targets = BuildTargets(diffedCompareSets)
            };
        }

        static IEnumerable<object> BuildTargets(DiffedCompareSet[] diffedCompareSets)
        {
            return
                from diffedSet in diffedCompareSets
                let diff = diffedSet.Diff
                let set = diffedSet.Set
                let removedPublicTypes = BuildRemovedPublicTypes(diff)
                let typesMadeInternal = BuildTypesMadeInternal(diff)
                let typeDifferences = BuildTypeDifferences(diff)
                let obsoletes = BuildTypesObsoleted(diff)
                select new
                {
                    set.Name,
                    ComparedTo = !string.IsNullOrEmpty(set.ComparedTo) ? string.Format(" (Compared To {0})", set.ComparedTo) : null,
                    noLongerSupported = diff is EmptyDiff,
                    hasRemovedPublicTypes = removedPublicTypes.Any(),
                    removedPublicTypes,
                    hasTypesMadeInternal = typesMadeInternal.Any(),
                    typesMadeInternal,
                    hasTypeDifferences = typeDifferences.Any(),
                    typeDifferences,
                    hasObsoletes = obsoletes.Any(),
                    obsoletes,
                    hasChanges = removedPublicTypes.Any() || typesMadeInternal.Any() || typeDifferences.Any() || obsoletes.Any()
                };
        }

        static IEnumerable<object> BuildTypeDifferences(Diff diff)
        {
            return from typeDiff in diff.MatchingTypeDiffs
                where !typeDiff.LeftType.HasObsoleteAttribute()
                where !typeDiff.RightType.HasObsoleteAttribute()
                where typeDiff.LeftType.IsPublic
                where typeDiff.RightType.IsPublic
                where typeDiff.HasDifferences()
                let fieldsChangedToNonPublic = BuildFieldsChangedToNonPublic(typeDiff)
                let fieldsRemoved = BuildFieldsRemoved(typeDiff)
                let methodsChangedToNonPublic = BuildMethodsChangedToNonPublic(typeDiff)
                let methodsRemoved = BuildMethodsRemoved(typeDiff)
                select new
                {
                    name = typeDiff.RightType.GetName(),
                    hasFieldsChangedToNonPublic = fieldsChangedToNonPublic.Any(),
                    fieldsChangedToNonPublic,
                    hasFieldsRemoved = fieldsRemoved.Any(),
                    fieldsRemoved,
                    hasMethodsChangedToNonPublic = methodsChangedToNonPublic.Any(),
                    methodsChangedToNonPublic,
                    hasMethodsRemoved = methodsRemoved.Any(),
                    methodsRemoved
                };
        }

        static IEnumerable<object> BuildMethodsRemoved(TypeDiff typeDiff)
        {
            foreach (var method in typeDiff.PublicMethodsRemoved())
            {
                yield return new
                {
                    name = method.GetName()
                };
            }
        }

        static IEnumerable<object> BuildMethodsChangedToNonPublic(TypeDiff typeDiff)
        {
            foreach (var method in typeDiff.MethodsChangedToNonPublic())
            {
                yield return new
                {
                    name = method.Left.GetName()
                };
            }
        }

        static IEnumerable<object> BuildFieldsRemoved(TypeDiff typeDiff)
        {
            foreach (var field in typeDiff.PublicFieldsRemoved())
            {
                yield return new
                {
                    name = field.GetName()
                };
            }
        }

        static IEnumerable<object> BuildFieldsChangedToNonPublic(TypeDiff typeDiff)
        {
            foreach (var field in typeDiff.FieldsChangedToNonPublic())
            {
                yield return new
                {
                    name = field.Right.GetName()
                };
            }
        }

        static IEnumerable<object> BuildRemovedPublicTypes(Diff diff)
        {
            foreach (var definition in diff.RemovedPublicTypes())
            {
                yield return new
                {
                    name = definition.GetName()
                };
            }
        }

        static IEnumerable<object> BuildTypesMadeInternal(Diff diff)
        {
            foreach (var typeDiff in diff.TypesChangedToNonPublic())
            {
                yield return new
                {
                    name = typeDiff.RightType.GetName()
                };
            }
        }

        static IEnumerable<object> BuildTypesObsoleted(Diff diff)
        {
            return from typeDiff in diff.RightAllTypes.TypeWithObsoletes()
            let obsoleteMessage = typeDiff.HasObsoleteAttribute() ? typeDiff.GetObsoleteString() : null
            let obsoleteFields = BuildObsoleteFields(typeDiff) 
            let obsoleteMethods = BuildObsoleteMethods(typeDiff)
            select
            new
            {
                name = typeDiff.GetName(),
                obsolete = obsoleteMessage,
                hasObsoleteFields = obsoleteFields.Any(),
                obsoleteFields,
                hasObsoleteMethods = obsoleteMethods.Any(),
                obsoleteMethods
            };
        }

        static IEnumerable<object> BuildObsoleteFields(TypeDefinition typeDiff)
        {
            return from field in typeDiff.GetObsoleteFields()
            select new
            {
                name = field.GetName(),
                obsolete = field.GetObsoleteString()
            };
        }

        static IEnumerable<object> BuildObsoleteMethods(TypeDefinition typeDiff)
        {
            return from method in typeDiff.GetObsoleteMethods()
                select new
                {
                    name = method.GetName(),
                    obsolete = method.GetObsoleteString()
                };
        }
    }
}