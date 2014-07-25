using System;
using System.Collections.Generic;
using System.Linq;
using EqualityComparers;
using Mono.Cecil;

namespace APIComparer
{
    public class ComparerEngine
    {
        public static Diff CreateDiff(string leftAssembly, string rightAssembly)
        {
            var l = AssemblyDefinition.ReadAssembly(leftAssembly);
            var r = AssemblyDefinition.ReadAssembly(rightAssembly);

            return Diff(l, r);
        }

        private static Diff Diff(AssemblyDefinition leftAssembly, AssemblyDefinition rightAssembly)
        {
            IList<TypeDefinition> leftOrphans;
            IList<TypeDefinition> rightOrphans;
            IList<Tuple<TypeDefinition, TypeDefinition>> diffs;

            var typeEquality = EqualityCompare<TypeDefinition>.EquateBy(t => t.FullName);

            Diff(leftAssembly.MainModule.Types, rightAssembly.MainModule.Types, typeEquality, out leftOrphans, out rightOrphans, out diffs);

            var typeDiffs = diffs.Select(t => DiffTypes(t.Item1, t.Item2)).ToList();

            return new Diff()
            {
                LeftAssembly = leftAssembly,
                RightAssembly = rightAssembly,
                LeftOrphanTypes = leftOrphans,
                RightOrphanTypes = rightOrphans,
                MatchingTypeDiffs = typeDiffs
            };
        }

        private static TypeDiff DiffTypes(TypeDefinition leftType, TypeDefinition rightType)
        {
            IList<FieldDefinition> leftOrphanFields;
            IList<FieldDefinition> rightOrphanFields;
            IList<Tuple<FieldDefinition, FieldDefinition>> matchingFields;

            Diff(leftType.Fields, rightType.Fields, EqualityCompare<FieldDefinition>.EquateBy(f => f.FullName), out leftOrphanFields, out rightOrphanFields, out matchingFields);

            IList<PropertyDefinition> leftOrphanProperties;
            IList<PropertyDefinition> rightOrphanProperties;
            IList<Tuple<PropertyDefinition, PropertyDefinition>> matchingProperties;

            Diff(leftType.Properties, rightType.Properties, EqualityCompare<PropertyDefinition>.EquateBy(f => f.FullName), out leftOrphanProperties, out rightOrphanProperties, out matchingProperties);

            IList<MethodDefinition> leftOrphanMethods;
            IList<MethodDefinition> rightOrphanMethods;
            IList<Tuple<MethodDefinition, MethodDefinition>> matchingMethods;

            Diff(leftType.Methods, rightType.Methods, EqualityCompare<MethodDefinition>.EquateBy(f => f.FullName), out leftOrphanMethods, out rightOrphanMethods, out matchingMethods);

            return new TypeDiff()
            {
                LeftOrphanFields = leftOrphanFields,
                RightOrphanFields = rightOrphanFields,
                MatchingFields = matchingFields,

                LeftOrphanProperties = leftOrphanProperties,
                RightOrphanProperties = rightOrphanProperties,
                MatchingProperties = matchingProperties,

                LeftOrphanMethods = leftOrphanMethods,
                RightOrphanMethods = rightOrphanMethods,
                MatchingMethods = matchingMethods,
            };
        }

        private static void Diff<TSource>(IEnumerable<TSource> left, IEnumerable<TSource> right, IEqualityComparer<TSource> comparer, out IList<TSource> leftOrphans, out IList<TSource> rightOrphans, out IList<Tuple<TSource, TSource>> diffs)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;

            leftOrphans = new List<TSource>();
            rightOrphans = new List<TSource>();
            diffs = new List<Tuple<TSource, TSource>>();

            var leftRunning = true;
            var rightRunning = true;

            using (var leftEnum = left.GetEnumerator())
            using (var rightEnum = right.GetEnumerator())
            {
                while (leftRunning && rightRunning)
                {
                    leftRunning = leftEnum.MoveNext();

                    if (leftRunning)
                    {
                        var index = rightOrphans.IndexOf(leftEnum.Current, comparer);
                        if (index < 0)
                        {
                            leftOrphans.Add(leftEnum.Current);
                        }
                        else
                        {
                            diffs.Add(Tuple.Create(leftEnum.Current, rightOrphans[index]));
                            rightOrphans.RemoveAt(index);
                        }
                    }

                    rightRunning = rightEnum.MoveNext();

                    if (rightRunning)
                    {
                        var index = leftOrphans.IndexOf(rightEnum.Current, comparer);
                        if (index < 0)
                        {
                            rightOrphans.Add(rightEnum.Current);
                        }
                        else
                        {
                            diffs.Add(Tuple.Create(leftOrphans[index], rightEnum.Current));
                            leftOrphans.RemoveAt(index);
                        }
                    }
                }

                while (leftRunning)
                {
                    leftRunning = leftEnum.MoveNext();
                    if (leftRunning)
                        leftOrphans.Add(leftEnum.Current);
                }

                while (rightRunning)
                {
                    rightRunning = rightEnum.MoveNext();
                    if (rightRunning)
                        rightOrphans.Add(rightEnum.Current);
                }
            }
        }
    }

    public class Diff
    {
        public AssemblyDefinition LeftAssembly { get; set; }
        public AssemblyDefinition RightAssembly { get; set; }

        public IList<TypeDefinition> LeftOrphanTypes { get; set; }
        public IList<TypeDefinition> RightOrphanTypes { get; set; }

        public IList<TypeDiff> MatchingTypeDiffs { get; set; }
    }

    public class TypeDiff
    {
        public IList<FieldDefinition> LeftOrphanFields { get; set; }
        public IList<FieldDefinition> RightOrphanFields { get; set; }
        public IList<Tuple<FieldDefinition, FieldDefinition>> MatchingFields { get; set; }

        public IList<PropertyDefinition> LeftOrphanProperties { get; set; }
        public IList<PropertyDefinition> RightOrphanProperties { get; set; }
        public IList<Tuple<PropertyDefinition, PropertyDefinition>> MatchingProperties { get; set; }

        public IList<MethodDefinition> LeftOrphanMethods { get; set; }
        public IList<MethodDefinition> RightOrphanMethods { get; set; }
        public IList<Tuple<MethodDefinition, MethodDefinition>> MatchingMethods { get; set; }
    }
}