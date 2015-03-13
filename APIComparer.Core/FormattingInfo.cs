namespace APIComparer
{
    public class FormattingInfo
    {
        public string RightUrl { get; private set; }
        public string LeftUrl { get; private set; }

        public FormattingInfo(string leftUrl, string rightUrl)
        {
            RightUrl = rightUrl;
            LeftUrl = leftUrl;
        }
    }
}