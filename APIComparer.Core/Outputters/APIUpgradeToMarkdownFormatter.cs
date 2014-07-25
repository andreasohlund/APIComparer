using System.IO;
using System.Linq;
using System.Text;

namespace APIComparer.Outputters
{
    public class APIUpgradeToMarkdownFormatter : IOutputter
    {
        private readonly string markdownFilePath;

        public APIUpgradeToMarkdownFormatter(string markdownFilePath)
        {
            this.markdownFilePath = markdownFilePath;
        }

        public void WriteOut(Diff diff)
        {
            var sb = new StringBuilder();

            var missingTypes = diff.LeftOrphanTypes.Where(t => t.IsPublic).Select(t => t.FullName).OrderBy(s => s);

            sb.AppendLine("The following types are missing in the new API.");
            sb.AppendLine();
            foreach (var missingType in missingTypes)
            {
                sb.AppendLine("    " + missingType);
            }

            File.WriteAllText(markdownFilePath, sb.ToString());
        }
    }
}