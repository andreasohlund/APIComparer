namespace APIComparer.Backend
{
    using System.Linq;
    using APIComparer.VersionComparisons;

    public class CompareSetDiffer
    {
        public DiffedCompareSet[] Diff(CompareSet[] compareSets)
        {
            var engine = new ComparerEngine();
            return
                (
                    from set in compareSets.OrderByDescending(cs => cs.Name)
                    let diff = engine.CreateDiff(set.LeftAssemblyGroup, set.RightAssemblyGroup)
                    select new DiffedCompareSet
                    {
                        Set = set,
                        Diff = diff
                    }
                    ).ToArray();
        }
    }
}