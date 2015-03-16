namespace APIComparer.VersionComparisons
{
    class CompareSet
    {
        public AssemblyGroup LeftAssemblyGroup;
        public AssemblyGroup RightAssemblyGroup;

        public VersionPair Versions;

        public string Name;

        public override string ToString()
        {
            return string.Format("{0}-{1}", Name, Versions);
        }
    }
}