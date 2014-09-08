using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace APIComparer
{
    public class APIUpgradeToMarkdownFormatter 
    {
        string markdownFilePath;
        public string leftUrl;
        public string rightUrl;

        public APIUpgradeToMarkdownFormatter(string markdownFilePath, string leftUrl, string rightUrl)
        {
            this.markdownFilePath = markdownFilePath;
            this.leftUrl = leftUrl;
            this.rightUrl = rightUrl;
        }

        public void WriteOut(Diff diff)
        {
            var sb = new StringBuilder();

            WriteOut(diff, sb);

            File.WriteAllText(markdownFilePath, sb.ToString());
        }

        string CreateLinkedLine(string format, SequencePoint leftSP, SequencePoint rightSP, int offset = 0)
        {
            if (leftSP != null && rightSP != null)
            {
                var leftSPUrl = CreateSequencePointUrl(leftUrl, leftSP, offset);
                var rightSPUrl = CreateSequencePointUrl(rightUrl, rightSP, offset);
                return String.Format("{0} [ {1} | {2} ]", format, CreateMarkdownUrl("old", leftSPUrl), CreateMarkdownUrl("new", rightSPUrl));
            }
            if (leftSP != null)
            {
                var leftSPUrl = CreateSequencePointUrl(leftUrl, leftSP, offset);
                return String.Format("{0} [ {1} ]", format, CreateMarkdownUrl("old", leftSPUrl));
            }
            if (rightSP != null)
            {
                var rightSPUrl = CreateSequencePointUrl(rightUrl, rightSP, offset);
                return String.Format("{0} [ {1} ]", format, CreateMarkdownUrl("new", rightSPUrl));
            }
            return format;
        }

        string FormatTypes(TypeDefinition left, TypeDefinition right)
        {
            var leftSP = left.GetValidSequencePoint();
            var rightSP = right != null ? right.GetValidSequencePoint():null;
            return CreateLinkedLine(left.GetName(), leftSP, rightSP);
        }

        void WriteOut(Diff diff, StringBuilder sb)
        {
            var missingTypes = diff.LeftOrphanTypes.Select(t => new Tuple<TypeDefinition, TypeDefinition>(t, null))
                .OrderBy(t => t.Item1.FullName)
                .Select(t => FormatTypes(t.Item1, t.Item2));

            sb.AppendLine("#### The following public types are missing in the new API.");
            sb.AppendLine();
            foreach (var missingType in missingTypes)
            {
                sb.AppendLine("- " + HttpUtility.HtmlEncode(missingType) + "  ");
            }

            sb.AppendLine();

            missingTypes = diff.MatchingTypeDiffs.Select(td => Tuple.Create(td.LeftType, td.RightType))
                .OrderBy(t => t.Item1.FullName)
                .Select(t => FormatTypes(t.Item1, t.Item2));

            sb.AppendLine("#### The following types changed visibility in the new API.");
            sb.AppendLine();
            foreach (var missingType in missingTypes)
            {
                sb.AppendLine("- " + HttpUtility.HtmlEncode(missingType) + "  ");
            }

            sb.AppendLine();

            sb.AppendLine("#### The following members are missing on the public types.");
            sb.AppendLine();
            foreach (var typeDiff in diff.MemberTypeDiffs.OrderBy(m => m.LeftType.FullName))
            {
                WriteOut(typeDiff, sb);
            }
        }

        string FormatMethods(MethodDefinition left, MethodDefinition right, TypeDefinition fallbackRightType)
        {
            var leftSP = left.GetValidSequencePoint();
            var rightSP = right != null ? right.GetValidSequencePoint() :
                fallbackRightType != null ? fallbackRightType.GetValidSequencePoint() : null;
            return CreateLinkedLine(left.GetName(), leftSP, rightSP, 2);
        }

        string FormatField(FieldDefinition field)
        {
            return field.FieldType.GetName() + " " + field.Name;
        }

        void WriteOut(TypeDiff typeDiff, StringBuilder sb)
        {
            var missingFields = typeDiff.GetMissingFields()
                .Select(t => FormatField(t.Item1))
                .ToList();

            var missingMethods = typeDiff.GetMissingMethods()
                .Select(t => FormatMethods(t.Item1, t.Item2, typeDiff.RightType))
                .ToList();

            if (missingFields.Any() || missingMethods.Any())
            {
                sb.AppendLine("- " + HttpUtility.HtmlEncode(typeDiff.LeftType.GetName()));

                foreach (var missingField in missingFields)
                {
                    sb.AppendLine("  - " + HttpUtility.HtmlEncode(missingField));
                }

                foreach (var missingMethod in missingMethods)
                {
                    sb.AppendLine("  - " + HttpUtility.HtmlEncode(missingMethod));
                }

                sb.AppendLine();
            }
        }
        
        string CreateSequencePointUrl(string githubBase, SequencePoint sequencePoint, int offset = 0)
        {
            if (sequencePoint == null)
                return null;

            var line = sequencePoint.StartLine - offset;

            var buildPathElementCount = 4;

            var url = githubBase + string.Join("/", sequencePoint.Document.Url.Split('\\').Skip(buildPathElementCount));

            if (line > 0)
                url = url + "#L" + line;

            return url;
        }
        
        string CreateMarkdownUrl(string text, string url = null)
        {
            if (string.IsNullOrEmpty(url))
                return text;

            return String.Format("[{0}]({1})", text, url);
        }
        
    }
}