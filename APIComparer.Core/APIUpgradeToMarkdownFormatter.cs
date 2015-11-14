namespace APIComparer
{
    using System.IO;
    using System.Linq;
    using System.Web;

    public class APIUpgradeToMarkdownFormatter
    {
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

        private void WriteOut(TypeDiff typeDiff, TextWriter writer, FormattingInfo info)
        {
            var typeObsoleted = typeDiff.TypeObsoleted();
            writer.WriteLine();
            writer.Write("### {0}", HttpUtility.HtmlEncode(typeDiff.RightType.GetName()));
            if (typeObsoleted)
            {
                writer.Write("(Obsoleted)");
            }

            writer.WriteLine();

            if (typeObsoleted)
            {
                writer.WriteLine(typeDiff.RightType.GetObsoleteString());
            }

            WriteFields(typeDiff, writer);
            WriteMethods(typeDiff, writer, info);

            writer.WriteLine();
        }

        private void WriteFields(TypeDiff typeDiff, TextWriter writer)
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

            var obsoleted = typeDiff.PublicFieldsObsoleted().ToList();
            if (obsoleted.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Fields Obsoleted");
                writer.WriteLine();
                foreach (var field in obsoleted)
                {
                    writer.WriteLine("  - `{0}`: {1}", field.Right.GetName(), field.Right.GetObsoleteString());
                }
            }
        }

        private void WriteMethods(TypeDiff typeDiff, TextWriter writer, FormattingInfo info)
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

            var obsoleted = typeDiff.PublicMethodsObsoleted().ToList();
            if (obsoleted.Any())
            {
                writer.WriteLine();
                writer.WriteLine("#### Methods Obsoleted");
                writer.WriteLine();
                foreach (var method in obsoleted)
                {
                    writer.WriteLine("  - `{0}`: {1}", method.Right.GetName(), method.Right.GetObsoleteString());
                }
            }
        }
    }
}