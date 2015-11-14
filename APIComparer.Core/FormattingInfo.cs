namespace APIComparer
{
    public class FormattingInfo
    {
        public FormattingInfo(string leftUrl, string rightUrl)
        {
            RightUrl = rightUrl;
            LeftUrl = leftUrl;
        }

        public string RightUrl { get; private set; }
        public string LeftUrl { get; private set; }
    }
}