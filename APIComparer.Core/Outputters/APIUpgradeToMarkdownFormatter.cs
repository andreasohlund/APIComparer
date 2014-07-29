using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mono.Cecil;

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

            var missingTypes = MissingMembers(diff.LeftOrphanTypes, diff.MatchingTypeDiffs.Select(td => Tuple.Create(td.LeftType, td.RightType)))
                .OrderBy(t => t.FullName)
                .Select(FormatType);

            sb.AppendLine("The following types are missing in the new API.");
            sb.AppendLine();
            foreach (var missingType in missingTypes)
            {
                sb.AppendLine("    " + missingType);
            }

            sb.AppendLine();

            sb.AppendLine("The following members are missing on the public types.");
            sb.AppendLine();
            foreach (var typeDiff in diff.MatchingTypeDiffs.OrderBy(m => m.LeftType.FullName))
            {
                WriteOut(typeDiff, sb);
            }

            File.WriteAllText(markdownFilePath, sb.ToString());
        }

        private void WriteOut(TypeDiff typeDiff, StringBuilder sb)
        {
            var missingFields = MissingMembers(typeDiff.LeftOrphanFields, typeDiff.MatchingFields).OrderBy(f => f.Name).Select(FormatField);
            var missingMethods = MissingMembers(typeDiff.LeftOrphanMethods, typeDiff.MatchingMethods).OrderBy(m => m.Name).Select(FormatMethod);

            if (missingFields.Any() || missingMethods.Any())
            {
                sb.AppendLine("    " + FormatType(typeDiff.LeftType));

                foreach (var missingField in missingFields)
                {
                    sb.AppendLine("        " + missingField);
                }

                foreach (var missingMethod in missingMethods)
                {
                    sb.AppendLine("        " + missingMethod);
                }

                sb.AppendLine();
            }
        }

        private IEnumerable<T> MissingMembers<T>(IEnumerable<T> members, IEnumerable<Tuple<T, T>> matching) where T : IMemberDefinition
        {
            return members
                .Concat(matching.Select(t => t.Item1));
        }

        private string FormatField(FieldDefinition field)
        {
            return FormatType(field.FieldType) + " " + field.Name;
        }

        private string FormatMethod(MethodDefinition method)
        {
            var methodParams = method.Parameters.Select(p => FormatType(p.ParameterType));

            return FormatType(method.ReturnType) + " " + method.Name + "(" + string.Join(", ", methodParams) + ")";
        }

        private string FormatType(TypeReference type)
        {
            var name = type.Namespace + "." + type.Name;
            var genericParams = Enumerable.Empty<string>();

            if (type.IsArray)
                name = name.Substring(0, name.Length - 2);

            if (name == "System.Void")
                name = "void";
            if (name == "System.Boolean")
                name = "bool";
            if (name == "System.String")
                name = "string";
            if (name == "System.Int32")
                name = "int";
            if (name == "System.Object")
                name = "object";

            var genericType = type as GenericInstanceType;
            if (genericType != null)
            {
                name = type.FullName.Split('`')[0];
                genericParams = genericType.GenericArguments.Select(FormatType);
            }

            if (type.HasGenericParameters)
            {
                name = type.FullName.Split('`')[0];
                genericParams = type.GenericParameters.Select(FormatType);
            }

            if (genericParams.Any())
                name = name + "<" + string.Join(", ", genericParams) + ">";

            if (type.IsArray)
                name = name + "[]";

            return name;
        }
    }
}