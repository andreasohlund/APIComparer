namespace APIComparer.VersionComparisons
{
    public class CompareSet
    {
        public override string ToString()
        {
            return Name;
        }

        public string ComparedTo;
        public AssemblyGroup LeftAssemblyGroup;

        public string Name;
        public AssemblyGroup RightAssemblyGroup;
    }
}