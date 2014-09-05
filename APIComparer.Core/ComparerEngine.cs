using System;
using System.Collections.Generic;
using System.Linq;
using APIComparer.Filters;
using Mono.Cecil;

namespace APIComparer
{
    public class ComparerEngine
    {
        public ComparerEngine()
        {
            Filter = new BaseAPIFilter();
        }

        public BaseAPIFilter Filter { get; set; }

        public Diff CreateDiff(string leftAssembly, string rightAssembly)
        {
            var readerParams = new ReaderParameters { ReadSymbols = true };

            var l = AssemblyDefinition.ReadAssembly(leftAssembly, readerParams);
            var r = AssemblyDefinition.ReadAssembly(rightAssembly, readerParams);

            return Diff(l, r);
        }

        Diff Diff(AssemblyDefinition leftAssembly, AssemblyDefinition rightAssembly)
        {
            IList<TypeDefinition> leftOrphans;
            IList<TypeDefinition> rightOrphans;
            IList<Tuple<TypeDefinition, TypeDefinition>> diffs;

            Diff(leftAssembly.MainModule.Types, rightAssembly.MainModule.Types, Filter.TypeComparer, out leftOrphans, out rightOrphans, out diffs);

            var typeDiffs = diffs.Select(t => DiffTypes(t.Item1, t.Item2)).ToList();

            return new Diff
            {
                LeftAssembly = leftAssembly,
                RightAssembly = rightAssembly,
                LeftOrphanTypes = leftOrphans.Where(Filter.FilterLeftType).ToList(),
                RightOrphanTypes = rightOrphans.Where(Filter.FilterRightType).ToList(),
                MatchingTypeDiffs = typeDiffs.Where(Filter.FilterMatchedType).ToList(),
                MemberTypeDiffs = typeDiffs.Where(Filter.FilterMemberTypeDiff).ToList()
            };
        }

        TypeDiff DiffTypes(TypeDefinition leftType, TypeDefinition rightType)
        {
            IList<FieldDefinition> leftOrphanFields;
            IList<FieldDefinition> rightOrphanFields;
            IList<Tuple<FieldDefinition, FieldDefinition>> matchingFields;

            Diff(leftType.Fields, rightType.Fields, Filter.FieldComparer, out leftOrphanFields, out rightOrphanFields, out matchingFields);

            IList<PropertyDefinition> leftOrphanProperties;
            IList<PropertyDefinition> rightOrphanProperties;
            IList<Tuple<PropertyDefinition, PropertyDefinition>> matchingProperties;

            Diff(leftType.Properties, rightType.Properties, Filter.PropertyComparer, out leftOrphanProperties, out rightOrphanProperties, out matchingProperties);

            IList<MethodDefinition> leftOrphanMethods;
            IList<MethodDefinition> rightOrphanMethods;
            IList<Tuple<MethodDefinition, MethodDefinition>> matchingMethods;

            Diff(leftType.Methods, rightType.Methods, Filter.MethodComparer, out leftOrphanMethods, out rightOrphanMethods, out matchingMethods);

            return new TypeDiff
            {
                LeftType = leftType,
                RightType = rightType,

                LeftOrphanFields = leftOrphanFields.Where(Filter.FilterLeftField).ToList(),
                RightOrphanFields = rightOrphanFields.Where(Filter.FilterRightField).ToList(),
                MatchingFields = matchingFields.Where(t => Filter.FilterMatchedField(t.Item1, t.Item2)).ToList(),

                LeftOrphanProperties = leftOrphanProperties.Where(Filter.FilterLeftProperty).ToList(),
                RightOrphanProperties = rightOrphanProperties.Where(Filter.FilterRightProperty).ToList(),
                MatchingProperties = matchingProperties.Where(t => Filter.FilterMatchedProperty(t.Item1, t.Item2)).ToList(),

                LeftOrphanMethods = leftOrphanMethods.Where(Filter.FilterLeftMethod).ToList(),
                RightOrphanMethods = rightOrphanMethods.Where(Filter.FilterRightMethod).ToList(),
                MatchingMethods = matchingMethods.Where(t => Filter.FilterMatchedMethod(t.Item1, t.Item2)).ToList(),
            };
        }

        static void Diff<TSource>(IEnumerable<TSource> left, IEnumerable<TSource> right, IEqualityComparer<TSource> comparer, out IList<TSource> leftOrphans, out IList<TSource> rightOrphans, out IList<Tuple<TSource, TSource>> diffs)
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
}