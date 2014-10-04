using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace APIComparer
{
    public class APIUpgradeToMarkdownFormatter 
    {
        StringBuilder stringBuilder;
        public string leftUrl;
        public string rightUrl;

        public APIUpgradeToMarkdownFormatter(StringBuilder stringBuilder, string leftUrl, string rightUrl)
        {
            this.stringBuilder = stringBuilder;
            this.leftUrl = leftUrl;
            this.rightUrl = rightUrl;
        }
        
        string CreateLinks(SequencePoint leftSP, SequencePoint rightSP, int offset = 0)
        {
            if (leftSP != null && rightSP != null)
            {
                var leftSPUrl = CreateSequencePointUrl(leftUrl, leftSP, offset);
                var rightSPUrl = CreateSequencePointUrl(rightUrl, rightSP, offset);
                return String.Format("[ {0} | {1} ]", CreateMarkdownUrl("old", leftSPUrl), CreateMarkdownUrl("new", rightSPUrl));
            }
            if (leftSP != null)
            {
                var leftSPUrl = CreateSequencePointUrl(leftUrl, leftSP, offset);
                return String.Format("[ {0} ]", CreateMarkdownUrl("old", leftSPUrl));
            }
            if (rightSP != null)
            {
                var rightSPUrl = CreateSequencePointUrl(rightUrl, rightSP, offset);
                return String.Format("[ {0} ]", CreateMarkdownUrl("new", rightSPUrl));
            }
            return "";
        }

        string CreateLeftLink(SequencePoint rightSP, int offset = 0)
        {
            if (rightSP != null)
            {
                var rightSPUrl = CreateSequencePointUrl(leftUrl, rightSP, offset);
                return String.Format("[ {0} ]", CreateMarkdownUrl("link", rightSPUrl));
            }
            return "";
        }

        string CreateRightLink(SequencePoint rightSP, int offset = 0)
        {
            if (rightSP != null)
            {
                var rightSPUrl = CreateSequencePointUrl(rightUrl, rightSP, offset);
                return String.Format("[ {0} ]", CreateMarkdownUrl("link", rightSPUrl));
            }
            return "";
        }

        void WriteObsoletes(IEnumerable<TypeDefinition> allTypes)
        {
            var obsoleteTypes = allTypes.TypeWithObsoletes().ToList();
                
            if (obsoleteTypes.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("## The following types have Obsoletes.");
                stringBuilder.AppendLine();
                foreach (var type in obsoleteTypes)
                {
                    var link = CreateRightLink(type.GetValidSequencePoint());
                    stringBuilder.AppendLine("### " + HttpUtility.HtmlEncode(type.GetName()) + "  " + link);
                    stringBuilder.AppendLine();
                    if (type.HasObsoleteAttribute())
                    {
                        stringBuilder.AppendLine(type.GetObsoleteString());
                        stringBuilder.AppendLine();
                    }

                    WriteObsoleteFields(type);
                    WriteObsoleteMethods(type);
                }
                stringBuilder.AppendLine();
            }

        }

        void WriteObsoleteFields(TypeDefinition type)
        {
            var obsoletes = type.GetObsoleteFields().ToList();
            if (obsoletes.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("#### Obsolete Fields");
                stringBuilder.AppendLine();
                foreach (var field in obsoletes)
                {
                    stringBuilder.AppendFormat("  - `{0}`", field.GetName());
                    stringBuilder.AppendLine("<br>" + field.GetObsoleteString());
                }
            }
            stringBuilder.AppendLine();
        }
        void WriteObsoleteMethods(TypeDefinition type)
        {
            var obsoletes = type.GetObsoleteMethods().ToList();
            if (obsoletes.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("#### Obsolete Methods");
                stringBuilder.AppendLine();
                foreach (var method in obsoletes)
                {
                    var link = CreateRightLink(method.GetValidSequencePoint());
                    stringBuilder.Append(string.Format("  - `{0}` {1}", method.GetName(), link));
                    stringBuilder.AppendLine("<br>" + method.GetObsoleteString());
                }
            }
            stringBuilder.AppendLine();
        }



        public void WriteOut(Diff diff)
        {
            var removePublicTypes = diff.RemovePublicTypes().ToList();
            if (removePublicTypes.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("## The following public types have been removed.");
                stringBuilder.AppendLine();
                foreach (var type in removePublicTypes)
                {
                    var link = CreateLeftLink(type.GetValidSequencePoint());
                    stringBuilder.AppendLine(string.Format("- `{0}` {1}", type.GetName(), link));
                }
                stringBuilder.AppendLine();
            }
            var typesChangedToNonPublic = diff.TypesChangedToNonPublic().ToList();
            if (typesChangedToNonPublic.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("## The following public types have been made internal.");
                stringBuilder.AppendLine();
                foreach (var type in typesChangedToNonPublic)
                {
                    var links = CreateLinks(type.LeftType.GetValidSequencePoint(), type.RightType.GetValidSequencePoint());
                    stringBuilder.AppendLine(string.Format("- `{0}` {1}", type.RightType.GetName(), links));
                }
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("## The following types have differences.");
            stringBuilder.AppendLine();
            foreach (var typeDiff in diff.MatchingTypeDiffs)
            {
                if (typeDiff.LeftType.HasObsoleteAttribute())
                {
                    continue;
                }
                if (typeDiff.RightType.HasObsoleteAttribute())
                {
                    continue;
                }

                if (!typeDiff.LeftType.IsPublic)
                {
                    continue;
                }
                if (!typeDiff.RightType.IsPublic)
                {
                    continue;
                }
                if (HasDifferences(typeDiff))
                {
                    WriteOut(typeDiff);
                }
            }

            WriteObsoletes(diff.RightAllTypes);
        }


        public bool HasDifferences(TypeDiff typeDiff)
        {
            return
                typeDiff.PublicFieldsRemoved().Any() ||
                typeDiff.PublicMethodsRemoved().Any() ||
                typeDiff.FieldsChangedToNonPublic().Any() ||
                typeDiff.MethodsChangedToNonPublic().Any()
                ;
        }
        void WriteOut(TypeDiff typeDiff)
        {
            stringBuilder.AppendLine();
            var links = CreateLinks(typeDiff.LeftType.GetValidSequencePoint(), typeDiff.RightType.GetValidSequencePoint());
            stringBuilder.AppendFormat("### {0}  {1}", HttpUtility.HtmlEncode(typeDiff.RightType.GetName()), links);
            stringBuilder.AppendLine();

            WriteFields(typeDiff);
            WriteMethods(typeDiff);

            stringBuilder.AppendLine();
        }

        void WriteFields(TypeDiff typeDiff)
        {
            var changedToNonPublic = typeDiff.FieldsChangedToNonPublic().ToList();
            if (changedToNonPublic.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("#### Fields changed to non-public");
                stringBuilder.AppendLine();
                foreach (var field in changedToNonPublic)
                {
                    stringBuilder.AppendLine(string.Format("  - `{0}`", field.Right.GetName()));
                }
            }


            //var added = typeDiff.RightOrphanFields.ToList();
            //if (added.Any())
            //{
            //    stringBuilder.AppendLine();
            //    stringBuilder.AppendLine("#### Fields Added");
            //    stringBuilder.AppendLine();
            //    foreach (var field in added)
            //    {
            //        stringBuilder.AppendLine("  - " + field.HtmlEncodedName());
            //    }
            //}

            var removed = typeDiff.PublicFieldsRemoved().ToList();
            if (removed.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("#### Fields Removed"); 
                stringBuilder.AppendLine();
                foreach (var field in removed)
                {
                    stringBuilder.AppendLine(string.Format("  - `{0}`", field.GetName()));
                }
            }
        }

        void WriteMethods(TypeDiff typeDiff)
        {
            var changedToNonPublic = typeDiff.MethodsChangedToNonPublic().ToList();
            if (changedToNonPublic.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("#### Methods changed to non-public");
                stringBuilder.AppendLine();
                foreach (var method in changedToNonPublic)
                {
                    var leftSP = method.Left.GetValidSequencePoint();
                    var rightSP = method.Right.GetValidSequencePoint();
                    stringBuilder.AppendLine(string.Format("  - `{0}` {1}", method.Left.GetName(), CreateLinks(leftSP, rightSP)));
                }
            }


            //var added = typeDiff.RightOrphanMethods.ToList();
            //if (added.Any())
            //{
            //    stringBuilder.AppendLine();
            //    stringBuilder.AppendLine("#### Methods Added");
            //    stringBuilder.AppendLine();
            //    foreach (var method in added)
            //    {
            //        var sequencePoint = method.GetValidSequencePoint();
            //        stringBuilder.AppendLine(string.Format("  - {0} {1}", method.HtmlEncodedName(), CreateLinks(null, sequencePoint, 2)));
            //    }
            //}

            var removed = typeDiff.PublicMethodsRemoved().ToList();
            if (removed.Any())
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("#### Methods Removed");
                stringBuilder.AppendLine();
                foreach (var method in removed)
                {
                    var sequencePoint = method.GetValidSequencePoint();
                    stringBuilder.AppendLine(string.Format("  - `{0}` {1}", method.GetName(), CreateLeftLink(sequencePoint)));
                }
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