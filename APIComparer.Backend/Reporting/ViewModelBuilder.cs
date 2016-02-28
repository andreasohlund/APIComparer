namespace APIComparer.Backend.Reporting
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using APIComparer;

    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class ViewModelBuilder
    {
        public static APIComparison Build(PackageDescription description, DiffedCompareSet[] diffedCompareSets)
        {
            return new APIComparison
            {
                targets = BuildTargets(diffedCompareSets)
            };
        }

        static IEnumerable<TargetReport> BuildTargets(DiffedCompareSet[] diffedCompareSets)
        {
            var setCount = 0;
            return
                from diffedSet in diffedCompareSets
                let diff = diffedSet.Diff
                let set = diffedSet.Set
                let removedPublicTypes = BuildRemovedPublicTypes(diff)
                let typesMadeInternal = BuildTypesMadeInternal(diff)
                let typeDifferences = BuildTypeDifferences(diff)
                let hasChanges = removedPublicTypes.Any() || typesMadeInternal.Any() || typeDifferences.Any()
                let count = setCount++
                select new TargetReport
                {
                    Name = set.Name,
                    ShortName = diffedCompareSets.Length < 5 ? set.Name : "Set #" + count,
                    ComparedTo = !string.IsNullOrEmpty(set.ComparedTo) ? $" (Compared To {set.ComparedTo})" : null,
                    noLongerSupported = diff is EmptyDiff,
                    hasRemovedPublicTypes = removedPublicTypes.Any(),
                    removedPublicTypes = removedPublicTypes,
                    hasTypesMadeInternal = typesMadeInternal.Any(),
                    typesMadeInternal = typesMadeInternal,
                    hasTypeDifferences = typeDifferences.Any(),
                    typeDifferences = typeDifferences,
                    hasChanges = hasChanges
                };
        }

        static IEnumerable<TypeDifferencesReport> BuildTypeDifferences(Diff diff)
        {
            return from typeDiff in diff.MatchingTypeDiffs
                   where typeDiff.LeftType.IsPublic
                   where typeDiff.RightType.IsPublic
                   where typeDiff.HasDifferences()
                   let fieldsChangedToNonPublic = BuildFieldsChangedToNonPublic(typeDiff)
                   let fieldsRemoved = BuildFieldsRemoved(typeDiff)
                   let fieldsObsoleted = BuildFieldsObsoleted(typeDiff)
                   let methodsChangedToNonPublic = BuildMethodsChangedToNonPublic(typeDiff)
                   let methodsRemoved = BuildMethodsRemoved(typeDiff)
                   let methodsObsoleted = BuildMethodsObsoleted(typeDiff)
                   let hasBeenObsoleted = !typeDiff.LeftType.HasObsoleteAttribute() && typeDiff.RightType.HasObsoleteAttribute() 
                   select new TypeDifferencesReport
                   {
                       name = typeDiff.RightType.GetName(),
                       hasBeenObsoleted = hasBeenObsoleted,
                       obsoleteMessage = hasBeenObsoleted ? typeDiff.RightType.GetObsoleteString() : "",
                       hasFieldsChangedToNonPublic = fieldsChangedToNonPublic.Any(),
                       fieldsChangedToNonPublic = fieldsChangedToNonPublic,
                       hasFieldsObsoleted = fieldsObsoleted.Any(),
                       fieldsObsoleted = fieldsObsoleted,
                       hasFieldsRemoved = fieldsRemoved.Any(),
                       fieldsRemoved = fieldsRemoved,
                       hasMethodsChangedToNonPublic = methodsChangedToNonPublic.Any(),
                       methodsChangedToNonPublic = methodsChangedToNonPublic,
                       hasMethodsRemoved = methodsRemoved.Any(),
                       methodsRemoved = methodsRemoved,
                       hasMethodsObsoleted = methodsObsoleted.Any(),
                       methodsObsoleted = methodsObsoleted,
                   };
        }

       


        static IEnumerable<RemovedItem> BuildMethodsRemoved(TypeDiff typeDiff)
        {
            foreach (var method in typeDiff.PublicMethodsRemoved())
            {
                yield return new RemovedItem
                {
                    name = method.GetName()
                };
            }
        }
        static IEnumerable<ObsoletedItem> BuildMethodsObsoleted(TypeDiff typeDiff)
        {
            foreach (var method in typeDiff.PublicMethodsObsoleted())
            {
                yield return new ObsoletedItem
                {
                    name = method.Right.GetName(),
                    obsolete = method.Right.GetObsoleteString()
                };
            }
        }

        static IEnumerable<RemovedItem> BuildMethodsChangedToNonPublic(TypeDiff typeDiff)
        {
            foreach (var method in typeDiff.MethodsChangedToNonPublic())
            {
                yield return new RemovedItem
                {
                    name = method.Left.GetName()
                };
            }
        }

        static IEnumerable<RemovedItem> BuildFieldsRemoved(TypeDiff typeDiff)
        {
            foreach (var field in typeDiff.PublicFieldsRemoved())
            {
                yield return new RemovedItem
                {
                    name = field.GetName()
                };
            }
        }

        static IEnumerable<RemovedItem> BuildFieldsChangedToNonPublic(TypeDiff typeDiff)
        {
            foreach (var field in typeDiff.FieldsChangedToNonPublic())
            {
                yield return new RemovedItem
                {
                    name = field.Right.GetName()
                };
            }
        }
        static IEnumerable<ObsoletedItem> BuildFieldsObsoleted(TypeDiff typeDiff)
        {
            foreach (var field in typeDiff.PublicFieldsObsoleted())
            {
                yield return new ObsoletedItem
                {
                    name = field.Right.GetName(),      
                    obsolete = field.Right.GetObsoleteString()
                };
            }
        }

        static IEnumerable<RemovedItem> BuildRemovedPublicTypes(Diff diff)
        {
            foreach (var definition in diff.RemovedPublicTypes())
            {
                yield return new RemovedItem
                {
                    name = definition.GetName()
                };
            }
        }

        static IEnumerable<RemovedItem> BuildTypesMadeInternal(Diff diff)
        {
            foreach (var typeDiff in diff.TypesChangedToNonPublic())
            {
                yield return new RemovedItem
                {
                    name = typeDiff.RightType.GetName()
                };
            }
        }
    }
}