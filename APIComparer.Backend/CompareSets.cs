namespace APIComparer.Backend
{
    using System.Collections.Generic;
    using System.Linq;
    using APIComparer.VersionComparisons;

    public static class CompareSets
    {
        public static IEnumerable<CompareSet> Create(List<Target> leftTargets, List<Target> rightTargets)
        {
            yield return new CompareSet
            {
                LeftAssemblyGroup = new AssemblyGroup(leftTargets.First().Files),
                RightAssemblyGroup = new AssemblyGroup(rightTargets.First().Files),
                Name = "net40"//todo               
            };
        }
    }
}