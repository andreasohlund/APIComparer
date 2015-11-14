namespace APIComparer.VersionComparisons
{
    class CompareSet
    {
        public override string ToString()
        {
            return $"{Name}-{Versions}";
        }

        public AssemblyGroup LeftAssemblyGroup;

        public string Name;
        public AssemblyGroup RightAssemblyGroup;

        public VersionPair Versions;
    }
}