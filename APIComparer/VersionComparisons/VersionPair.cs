namespace APIComparer.VersionComparisons
{
    public class VersionPair
    {
        public VersionPair(string leftVersion, string rightVersion)
        {
            LeftVersion = leftVersion;
            RightVersion = rightVersion;
        }

        public string LeftVersion { get; private set; }
        public string RightVersion { get; private set; }

        public override string ToString()
        {
            return $"{LeftVersion}..{RightVersion}";
        }
    }
}