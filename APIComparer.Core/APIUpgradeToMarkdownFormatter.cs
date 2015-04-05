namespace APIComparer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public class APIUpgradeToMarkdownFormatter 
    {
        string CreateLinks(SequencePoint leftSP, SequencePoint rightSP, FormattingInfo info, int offset = 0)
        {
            if (leftSP != null && rightSP != null)
            {
                var leftSPUrl = CreateSequencePointUrl(info.LeftUrl, leftSP, offset);
                var rightSPUrl = CreateSequencePointUrl(info.RightUrl, rightSP, offset);
                return String.Format("[ {0} | {1} ]", CreateMarkdownUrl("old", leftSPUrl), CreateMarkdownUrl("new", rightSPUrl));
            }
            if (leftSP != null)
            {
                var leftSPUrl = CreateSequencePointUrl(info.LeftUrl, leftSP, offset);
                return String.Format("[ {0} ]", CreateMarkdownUrl("old", leftSPUrl));
            }
            if (rightSP != null)
            {
                var rightSPUrl = CreateSequencePointUrl(info.RightUrl, rightSP, offset);
                return String.Format("[ {0} ]", CreateMarkdownUrl("new", rightSPUrl));
            }
            return "";
        }

        string CreateLeftLink(SequencePoint rightSP, FormattingInfo info, int offset = 0)
        {
            if (rightSP != null)
            {
                var rightSPUrl = CreateSequencePointUrl(info.LeftUrl, rightSP, offset);
                return String.Format("[ {0} ]", CreateMarkdownUrl("link", rightSPUrl));
            }
            return "";
        }

        string CreateRightLink(SequencePoint rightSP, FormattingInfo info, int offset = 0)
        {
            if (rightSP != null)
            {
                var rightSPUrl = CreateSequencePointUrl(info.RightUrl, rightSP, offset);
                return String.Format("[ {0} ]", CreateMarkdownUrl("link", rightSPUrl));
            }
            return "";
        }

        void WriteObsoletes(IEnumerable<TypeDefinition> allTypes, TextWriter writer, FormattingInfo info)
        {
            var obsoleteTypes = allTypes.TypeWithObsoletes().ToList();
                
            if (obsoleteTypes.Any())
            {
                writer.WriteLine();
                writer.WriteLine("## The following types have Obsoletes.");
                writer.WriteLine();
                foreach (var type in obsoleteTypes)
                {
                    var link = CreateRightLink(type.GetValidSequencePoint(), info);
                    writer.WriteLine("### " + HttpUtility.HtmlEncode(type.GetName()) + "  " + link);
                    writer.WriteLine();
                    if (type.HasObsoleteAttribute())
                    {
                        writer.WriteLine(type.GetObsoleteString());
                        writer.WriteLine();
                    }

                    WriteObsoleteFields(type, writer);
                    WriteObsoleteMethods(type, writer, info);
                }
                writer.WriteLine();
            }
        }

        void WriteObsoleteFields(TypeDefinition type, TextWriter writer)
        {
            var obsoletes = type.GetObsoleteFields().ToList();
            if (obsoletes.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Obsolete Fields");
                writer.WriteLine();
                foreach (var field in obsoletes)
                {
                    writer.Write("  - `{0}`", field.GetName());
                    writer.WriteLine("<br>" + field.GetObsoleteString());
                }
            }
            writer.WriteLine();
        }
        void WriteObsoleteMethods(TypeDefinition type, TextWriter writer, FormattingInfo info)
        {
            var obsoletes = type.GetObsoleteMethods().ToList();
            if (obsoletes.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Obsolete Methods");
                writer.WriteLine();
                foreach (var method in obsoletes)
                {
                    var link = CreateRightLink(method.GetValidSequencePoint(), info);
                    writer.Write("  - `{0}` {1}", method.GetName(), link);
                    writer.WriteLine("<br>" + method.GetObsoleteString());
                }
            }
            writer.WriteLine();
        }

        public void WriteOut(Diff diff, TextWriter writer, FormattingInfo info)
        {
            var removePublicTypes = diff.RemovedPublicTypes().ToList();
            if (removePublicTypes.Any())
            {
                writer.WriteLine();
                writer.WriteLine("## The following public types have been removed.");
                writer.WriteLine();
                foreach (var type in removePublicTypes)
                {
                    var link = CreateLeftLink(type.GetValidSequencePoint(), info);
                    writer.WriteLine("- `{0}` {1}", type.GetName(), link);
                }
                writer.WriteLine();
            }
            var typesChangedToNonPublic = diff.TypesChangedToNonPublic().ToList();
            if (typesChangedToNonPublic.Any())
            {
                writer.WriteLine();
                writer.WriteLine("## The following public types have been made internal.");
                writer.WriteLine();
                foreach (var type in typesChangedToNonPublic)
                {
                    var links = CreateLinks(type.LeftType.GetValidSequencePoint(), type.RightType.GetValidSequencePoint(), info);
                    writer.WriteLine("- `{0}` {1}", type.RightType.GetName(), links);
                }
                writer.WriteLine();
            }

            var matchingTypeDiffs = diff.MatchingTypeDiffs.ToList();
            if (matchingTypeDiffs.Any())
            {
                writer.WriteLine();
                writer.WriteLine("## The following types have differences.");
                writer.WriteLine();
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
                        WriteOut(typeDiff, writer, info);
                    }
                }
            }

            WriteObsoletes(diff.RightAllTypes, writer, info);
        }


        static bool HasDifferences(TypeDiff typeDiff)
        {
            return
                typeDiff.PublicFieldsRemoved().Any() ||
                typeDiff.PublicMethodsRemoved().Any() ||
                typeDiff.FieldsChangedToNonPublic().Any() ||
                typeDiff.MethodsChangedToNonPublic().Any()
                ;
        }

        void WriteOut(TypeDiff typeDiff, TextWriter writer, FormattingInfo info)
        {
            writer.WriteLine();
            var links = CreateLinks(typeDiff.LeftType.GetValidSequencePoint(), typeDiff.RightType.GetValidSequencePoint(), info);
            writer.Write("### {0}  {1}", HttpUtility.HtmlEncode(typeDiff.RightType.GetName()), links);
            writer.WriteLine();

            WriteFields(typeDiff, writer);
            WriteMethods(typeDiff, writer, info);

            writer.WriteLine();
        }

        void WriteFields(TypeDiff typeDiff, TextWriter writer)
        {
            var changedToNonPublic = typeDiff.FieldsChangedToNonPublic().ToList();
            if (changedToNonPublic.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Fields changed to non-public");
                writer.WriteLine();
                foreach (var field in changedToNonPublic)
                {
                    writer.WriteLine("  - `{0}`", field.Right.GetName());
                }
            }


            //var added = typeDiff.RightOrphanFields.ToList();
            //if (added.Any())
            //{
            //    writer.WriteLine();
            //    writer.WriteLine("#### Fields Added");
            //    writer.WriteLine();
            //    foreach (var field in added)
            //    {
            //        writer.WriteLine("  - " + field.HtmlEncodedName());
            //    }
            //}

            var removed = typeDiff.PublicFieldsRemoved().ToList();
            if (removed.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Fields Removed"); 
                writer.WriteLine();
                foreach (var field in removed)
                {
                    writer.WriteLine("  - `{0}`", field.GetName());
                }
            }
        }

        void WriteMethods(TypeDiff typeDiff, TextWriter writer, FormattingInfo info)
        {
            var changedToNonPublic = typeDiff.MethodsChangedToNonPublic().ToList();
            if (changedToNonPublic.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Methods changed to non-public");
                writer.WriteLine();
                foreach (var method in changedToNonPublic)
                {
                    var leftSP = method.Left.GetValidSequencePoint();
                    var rightSP = method.Right.GetValidSequencePoint();
                    writer.WriteLine("  - `{0}` {1}", method.Left.GetName(), CreateLinks(leftSP, rightSP, info));
                }
            }


            //var added = typeDiff.RightOrphanMethods.ToList();
            //if (added.Any())
            //{
            //    writer.WriteLine();
            //    writer.WriteLine("#### Methods Added");
            //    writer.WriteLine();
            //    foreach (var method in added)
            //    {
            //        var sequencePoint = method.GetValidSequencePoint();
            //        writer.WriteLine(string.Format("  - {0} {1}", method.HtmlEncodedName(), CreateLinks(null, sequencePoint, 2)));
            //    }
            //}

            var removed = typeDiff.PublicMethodsRemoved().ToList();
            if (removed.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Methods Removed");
                writer.WriteLine();
                foreach (var method in removed)
                {
                    var sequencePoint = method.GetValidSequencePoint();
                    writer.WriteLine("  - `{0}` {1}", method.GetName(), CreateLeftLink(sequencePoint, info));
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