namespace APIComparer
{
    using System.Collections.Generic;
    using System.Linq;
    using EqualityComparers;
    using Mono.Cecil;

    public class ComparerEngine
    {
        IEqualityComparer<TypeDefinition> typeComparer;
        IEqualityComparer<FieldDefinition> fieldComparer;
        IEqualityComparer<MethodDefinition> methodComparer;

        public ComparerEngine()
        {
            typeComparer = EqualityCompare<TypeDefinition>.EquateBy(t => t.FullName);
            fieldComparer = EqualityCompare<FieldDefinition>.EquateBy(f => f.FullName);
            methodComparer = EqualityCompare<MethodDefinition>.EquateBy(m => m.FullName);
        }

        //MethodComparer = EqualityCompare<MethodDefinition>
        //    .EquateBy(m => m.DeclaringType.FullName)
        //    .ThenEquateBy(m => m.Name);

        public Diff CreateDiff(string leftAssembly, string rightAssembly)
        {
            return CreateDiff(ReadTypes(leftAssembly), ReadTypes(rightAssembly));
        }
        public Diff CreateDiff(IEnumerable<string> leftAssemblyGroup, IEnumerable<string> rightAssemblyGroup)
        {
            return CreateDiff(ReadTypes(leftAssemblyGroup), ReadTypes(rightAssemblyGroup));
        }
        static IEnumerable<TypeDefinition> ReadTypes(IEnumerable<string> assemblyGroup)
        {
            var readerParams = new ReaderParameters
            {
                ReadSymbols = true
            };
            return assemblyGroup.SelectMany(assembly => AssemblyDefinition.ReadAssembly(assembly, readerParams).MainModule.Types);
        }
        static IEnumerable<TypeDefinition> ReadTypes(string leftAssembly)
        {
            var readerParams = new ReaderParameters
            {
                ReadSymbols = true
            };

            return AssemblyDefinition.ReadAssembly(leftAssembly, readerParams).MainModule.Types.ToList();
        }

        public Diff CreateDiff(IEnumerable<TypeDefinition> left, IEnumerable<TypeDefinition> right)
        {
            List<TypeDefinition> leftOrphans;
            List<TypeDefinition> rightOrphans;
            List<MatchingMember<TypeDefinition>> matching;

            var leftRealTypes = left.RealTypes().ToList();
            var rightRealTypes = right.RealTypes().ToList();

            Diff(leftRealTypes, rightRealTypes, typeComparer, out leftOrphans, out rightOrphans, out matching);

            var typeDiffs = matching.Select(t => DiffTypes(t.Left, t.Right))
                .ToList();

            return new Diff
            {
                RightAllTypes = rightRealTypes,
                LeftOrphanTypes = leftOrphans.OrderBy(x=>x.FullName).ToList(),
                RightOrphanTypes = rightOrphans.OrderBy(x => x.FullName).ToList(),
                MatchingTypeDiffs = typeDiffs.OrderBy(x => x.RightType.FullName).ToList(),
            };
        }

        TypeDiff DiffTypes(TypeDefinition leftType, TypeDefinition rightType)
        {
            List<FieldDefinition> leftOrphanFields;
            List<FieldDefinition> rightOrphanFields;
            List<MatchingMember<FieldDefinition>> matchingFields;

            Diff(leftType.RealFields(), rightType.RealFields(), fieldComparer, out leftOrphanFields, out rightOrphanFields, out matchingFields);

            List<MethodDefinition> leftOrphanMethods;
            List<MethodDefinition> rightOrphanMethods;
            List<MatchingMember<MethodDefinition>> matchingMethods;

            Diff(leftType.RealMethods(), rightType.RealMethods(), methodComparer, out leftOrphanMethods, out rightOrphanMethods, out matchingMethods);

            return new TypeDiff
            {
                LeftType = leftType,
                RightType = rightType,

                LeftOrphanFields = leftOrphanFields.OrderBy(x => x.Name).ToList(),
                RightOrphanFields = rightOrphanFields.OrderBy(x => x.Name).ToList(),
                MatchingFields = matchingFields.OrderBy(x => x.Right.Name).ToList(),
                LeftOrphanMethods = leftOrphanMethods.OrderBy(x => x.Name).ToList(),
                RightOrphanMethods = rightOrphanMethods.OrderBy(x => x.Name).ToList(),
                MatchingMethods = matchingMethods.OrderBy(x => x.Right.Name).ToList(),
            };
        }

        static void Diff<TSource>(IEnumerable<TSource> left, IEnumerable<TSource> right, IEqualityComparer<TSource> comparer, out List<TSource> leftOrphans, out List<TSource> rightOrphans, out List<MatchingMember<TSource>> matching) where TSource : IMemberDefinition
        {
            leftOrphans = left.ToList();
            rightOrphans = new List<TSource>();
            matching = new List<MatchingMember<TSource>>();

            foreach (var item in right)
            {
                var index = leftOrphans.IndexOf(item, comparer);
                if (index < 0)
                {
                    rightOrphans.Add(item);
                    continue;
                }
                var leftOrphan = leftOrphans[index];
                var matchingMember = new MatchingMember<TSource>
                {
                    Left = leftOrphan,
                    Right = item
                };
                matching.Add(matchingMember);
                leftOrphans.RemoveAt(index);
            }

            //var leftRunning = true;
            //var rightRunning = true;
            //using (var leftEnum = left.GetEnumerator())
            //using (var rightEnum = right.GetEnumerator())
            //{
            //    while (leftRunning && rightRunning)
            //    {
            //        leftRunning = leftEnum.MoveNext();

            //        if (leftRunning)
            //        {
            //            var index = rightOrphans.IndexOf(leftEnum.Current, comparer);
            //            if (index < 0)
            //            {
            //                leftOrphans.Add(leftEnum.Current);
            //            }
            //            else
            //            {
            //                var rightMember = rightOrphans[index];

            //                var matchingMember = new MatchingMember<TSource>
            //                {
            //                    Left = leftEnum.Current,
            //                    Right = rightMember
            //                };
            //                matching.Add(matchingMember);
            //                rightOrphans.RemoveAt(index);
            //            }
            //        }

            //        rightRunning = rightEnum.MoveNext();

            //        if (rightRunning)
            //        {
            //            var index = leftOrphans.IndexOf(rightEnum.Current, comparer);
            //            if (index < 0)
            //            {
            //                rightOrphans.Add(rightEnum.Current);
            //            }
            //            else
            //            {
            //                var matchingMember = new MatchingMember<TSource>
            //                {
            //                    Left = leftOrphans[index],
            //                    Right = rightEnum.Current
            //                };
            //                matching.Add(matchingMember);
            //                leftOrphans.RemoveAt(index);
            //            }
            //        }
            //    }

            //    while (leftRunning)
            //    {
            //        leftRunning = leftEnum.MoveNext();
            //        if (leftRunning)
            //            leftOrphans.Add(leftEnum.Current);
            //    }

            //    while (rightRunning)
            //    {
            //        rightRunning = rightEnum.MoveNext();
            //        if (rightRunning)
            //            rightOrphans.Add(rightEnum.Current);
            //    }
            //}
        }
    }
}