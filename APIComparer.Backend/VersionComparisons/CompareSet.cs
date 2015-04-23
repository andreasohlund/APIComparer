namespace APIComparer.VersionComparisons
{
    public class CompareSet
    {
        public AssemblyGroup LeftAssemblyGroup;
        public AssemblyGroup RightAssemblyGroup;

        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }
}