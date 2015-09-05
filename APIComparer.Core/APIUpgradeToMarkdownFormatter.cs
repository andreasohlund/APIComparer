namespace APIComparer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public class APIUpgradeToMarkdownFormatter
    {
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
                    writer.Write("  - `{0}`", method.GetName());
                    writer.WriteLine("<br>" + method.GetObsoleteString());
                }
            }
            writer.WriteLine();
        }

        public void WriteOut(Diff diff, TextWriter writer, FormattingInfo info)
        {
            if (diff is EmptyDiff)
            {
                writer.WriteLine("No longer supported");
                return;
            }

            var removePublicTypes = diff.RemovedPublicTypes().ToList();
            if (removePublicTypes.Any())
            {
                writer.WriteLine();
                writer.WriteLine("## The following public types have been removed.");
                writer.WriteLine();
                foreach (var type in removePublicTypes)
                {
                    writer.WriteLine("- `{0}`", type.GetName());
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
                    writer.WriteLine("- `{0}`", type.RightType.GetName());
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
                    if (typeDiff.HasDifferences())
                    {
                        WriteOut(typeDiff, writer, info);
                    }
                }
            }
        }

        void WriteOut(TypeDiff typeDiff, TextWriter writer, FormattingInfo info)
        {
            writer.WriteLine();
            writer.Write("### {0}", HttpUtility.HtmlEncode(typeDiff.RightType.GetName()));
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
                    writer.WriteLine("  - `{0}`", method.Left.GetName());
                }
            }

            var removed = typeDiff.PublicMethodsRemoved().ToList();
            if (removed.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Methods Removed");
                writer.WriteLine();
                foreach (var method in removed)
                {
                    writer.WriteLine("  - `{0}`", method.GetName());
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