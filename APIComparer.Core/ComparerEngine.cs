namespace APIComparer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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

        public Diff CreateDiff(string leftAssembly, string rightAssembly)
        {
            return CreateDiff(ReadTypes(leftAssembly,true), ReadTypes(rightAssembly,true));
        }

        public Diff CreateDiff(AssemblyGroup leftAssemblyGroup, AssemblyGroup rightAssemblyGroup)
        {
            if (rightAssemblyGroup is EmptyAssemblyGroup)
            {
                return new EmptyDiff();
            }

            return CreateDiff(ReadTypes(leftAssemblyGroup.Assemblies, leftAssemblyGroup.ReadSymbols), ReadTypes(rightAssemblyGroup.Assemblies, rightAssemblyGroup.ReadSymbols));
        }

        IEnumerable<TypeDefinition> ReadTypes(IEnumerable<string> assemblyGroup, bool readSymbols)
        {
            var readerParams = new ReaderParameters
            {
                ReadSymbols = readSymbols
            };

            return assemblyGroup
                .Where(assembly => IsManagedAssembly(assembly))
                .SelectMany(assembly => AssemblyDefinition.ReadAssembly(assembly, readerParams).MainModule.Types);
        }

        IEnumerable<TypeDefinition> ReadTypes(string leftAssembly, bool readSymbols)
        {
            return ReadTypes(new[] { leftAssembly }, readSymbols);
        }

        private bool IsManagedAssembly(string assemblyPath)
        {
            // From https://github.com/scriptcs/scriptcs/pull/250/files by @aaronpowell
            // License: Apache 2.0 (https://github.com/scriptcs/scriptcs/blob/dev/LICENSE.md)

            try
            {
                AssemblyName.GetAssemblyName(assemblyPath);
            }
            catch(BadImageFormatException)
            {
                return false;
            }

            return true;
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
        }
    }
}