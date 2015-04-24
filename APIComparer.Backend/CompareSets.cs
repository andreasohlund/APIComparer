namespace APIComparer.Backend
{
    using System.Collections.Generic;
    using System.Linq;
    using APIComparer.VersionComparisons;

    public static class CompareSets
    {
        public static IEnumerable<CompareSet> Create(List<Target> leftTargets, List<Target> rightTargets)
        {

            var allTargets = new List<string>();

            allTargets.AddRange(leftTargets.Select(t => t.Name));

            allTargets.AddRange(rightTargets.Select(t => t.Name));

            foreach (var uniqueTarget in allTargets.Distinct().OrderBy(s => s))
            {
                var leftTarget = leftTargets.SingleOrDefault(t => t.Name == uniqueTarget);
                AssemblyGroup leftAsmGroup;

                var compareSetName = uniqueTarget;

                if (leftTarget != null)
                {
                    leftAsmGroup = new AssemblyGroup(leftTarget.Files);
                }
                else
                {
                    var highestLeftTarget = leftTargets.OrderByDescending(t => t.Name).First();
                    leftAsmGroup = new AssemblyGroup(highestLeftTarget.Files);

                    compareSetName += string.Format("(Compared to {0})", highestLeftTarget.Name);
                }

                var rightTarget = rightTargets.SingleOrDefault(t => t.Name == uniqueTarget);
                AssemblyGroup rightAsmGroup;

                if (rightTarget != null)
                {
                    rightAsmGroup = new AssemblyGroup(rightTarget.Files);
                }
                else
                {
                    rightAsmGroup = new EmptyAssemblyGroup();
                }


                yield return new CompareSet
                {
                    LeftAssemblyGroup = leftAsmGroup,
                    RightAssemblyGroup = rightAsmGroup,
                    Name = compareSetName
                };
            }

        }
    }
}