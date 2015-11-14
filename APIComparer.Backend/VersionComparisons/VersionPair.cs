namespace APIComparer.VersionComparisons
{
    public class VersionPair
    {
        public VersionPair(string leftVersion, string rightVersion)
        {
            LeftVersion = leftVersion;
            RightVersion = rightVersion;
        }

        public string LeftVersion { get; }
        public string RightVersion { get; }

        public override string ToString()
        {
            return $"{LeftVersion}..{RightVersion}";
        }
    }
}