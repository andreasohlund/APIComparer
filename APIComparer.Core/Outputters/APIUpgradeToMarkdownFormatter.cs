using System.IO;
using System.Text;

namespace APIComparer.Outputters
{
    public class APIUpgradeToMarkdownFormatter : StringBuilderFormatter
    {
        string markdownFilePath;

        public APIUpgradeToMarkdownFormatter(string markdownFilePath, string leftUrl = null, string rightUrl = null)
            : base(leftUrl, rightUrl)
        {
            this.markdownFilePath = markdownFilePath;
        }

        public override void WriteOut(Diff diff)
        {
            var sb = new StringBuilder();

            WriteOut(diff, sb);

            File.WriteAllText(markdownFilePath, sb.ToString());
        }
    }
}