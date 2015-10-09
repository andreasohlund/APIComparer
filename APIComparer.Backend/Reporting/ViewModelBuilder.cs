namespace APIComparer.Backend.Reporting
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using APIComparer;

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
                let hasChanges = removedPublicTypes.Any() || typesMadeInternal.Any() || typeDifferences.Any()
                select new
                {
                    set.Name,
                    ComparedTo = !string.IsNullOrEmpty(set.ComparedTo) ? $" (Compared To {set.ComparedTo})" : null,
                    noLongerSupported = diff is EmptyDiff,
                    hasRemovedPublicTypes = removedPublicTypes.Any(),
                    removedPublicTypes,
                    hasTypesMadeInternal = typesMadeInternal.Any(),
                    typesMadeInternal,
                    hasTypeDifferences = typeDifferences.Any(),
                    typeDifferences,
                    hasChanges
                };
        }

        static IEnumerable<object> BuildTypeDifferences(Diff diff)
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
                   select new
                   {
                       name = typeDiff.RightType.GetName(),
                       hasBeenObsoleted,
                       obsoleteMessage = hasBeenObsoleted ? typeDiff.RightType.GetObsoleteString() : "",
                       hasFieldsChangedToNonPublic = fieldsChangedToNonPublic.Any(),
                       fieldsChangedToNonPublic,
                       hasFieldsObsoleted = fieldsObsoleted.Any(),
                       fieldsObsoleted,
                       hasFieldsRemoved = fieldsRemoved.Any(),
                       fieldsRemoved,
                       hasMethodsChangedToNonPublic = methodsChangedToNonPublic.Any(),
                       methodsChangedToNonPublic,
                       hasMethodsRemoved = methodsRemoved.Any(),
                       methodsRemoved,
                       hasMethodsObsoleted = fieldsObsoleted.Any(),
                       methodsObsoleted,
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
        static IEnumerable<object> BuildMethodsObsoleted(TypeDiff typeDiff)
        {
            foreach (var method in typeDiff.PublicMethodsObsoleted())
            {
                yield return new
                {
                    name = method.Right.GetName(),
                    obsolete = method.Right.GetObsoleteString()
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
        static IEnumerable<object> BuildFieldsObsoleted(TypeDiff typeDiff)
        {
            foreach (var field in typeDiff.PublicFieldsObsoleted())
            {
                yield return new
                {
                    name = field.Right.GetName(),      
                    obsolete = field.Right.GetObsoleteString()
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
    }
}