namespace APIComparer.VersionComparisons
{
    class VersionPair
    {
        public VersionPair(string leftVersion,string rightVersion)
        {
            LeftVersion = leftVersion;
            RightVersion = rightVersion;
        }

        public string LeftVersion { get; private set; }
        public string RightVersion { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}..{1}", LeftVersion, RightVersion);
        }
    }
}