using System;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace APIComparer
{
    public class RawOutputFormatter 
    {
        StringBuilder stringBuilder;

        public RawOutputFormatter()
        {
            stringBuilder = new StringBuilder();
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        public void WriteOut(Diff diff)
        {
            stringBuilder.Clear();

            WriteOut(diff, stringBuilder);
        }
        
        string FormatTypes(TypeDefinition left, TypeDefinition right)
        {
            return CreateLinkedLine(CecilExtensions.GetName, left, right);
        }

        void WriteOut(Diff diff, StringBuilder sb)
        {
            var missingTypes = diff.LeftOrphanTypes.Select(t => new Tuple<TypeDefinition, TypeDefinition>(t, null))
                .OrderBy(t => t.Item1.FullName)
                .Select(t => FormatTypes(t.Item1, t.Item2));

            sb.AppendLine("Public -> [MISSING]");
            foreach (var missingType in missingTypes)
            {
                sb.AppendLine(missingType);
            }

            sb.AppendLine();

            missingTypes = diff.MatchingTypeDiffs.Select(td => Tuple.Create(td.LeftType, td.RightType))
                .OrderBy(t => t.Item1.FullName)
                .Select(t => FormatTypes(t.Item1, t.Item2));

            sb.AppendLine("Public -> Internal");
            foreach (var missingType in missingTypes)
            {
                sb.AppendLine(missingType);
            }

            sb.AppendLine();

            sb.AppendLine("Members");
            sb.AppendLine();
            foreach (var typeDiff in diff.MemberTypeDiffs.OrderBy(m => m.LeftType.FullName))
            {
                WriteOut(typeDiff, sb);
            }
        }

        string CreateLinkedLine<T>(Func<T, string> formatter, T left, T right)
        {
            return Environment.NewLine + formatter(left) + Environment.NewLine + (right != null ? formatter(right) : "[MISSING]");
        }

        string FormatField(FieldDefinition field)
        {
            return "[" + field.Attributes + "] " + field.FieldType.GetName() + " " + field.Name;
        }

        string FormatFields(FieldDefinition left, FieldDefinition right)
        {
            return Environment.NewLine + FormatField(left) + Environment.NewLine + (right != null ? FormatField(right) : "[MISSING]");
        }

        string FormatMethods(MethodDefinition left, MethodDefinition right)
        {
            return CreateLinkedLine(FormatMethod, left, right);
        }
        
        void WriteOut(TypeDiff typeDiff, StringBuilder sb)
        {
            var missingFields = typeDiff.GetMissingFields()
                .Select(t => FormatFields(t.Item1, t.Item2))
                .ToList();

            var missingMethods = typeDiff.GetMissingMethods()
                .Select(t => FormatMethods(t.Item1, t.Item2))
                .ToList();

            if (missingFields.Any() || missingMethods.Any())
            {
                sb.AppendLine("- " + typeDiff.LeftType.GetName());

                foreach (var missingField in missingFields)
                {
                    sb.AppendLine("  - " + missingField);
                }

                foreach (var missingMethod in missingMethods)
                {
                    sb.AppendLine("  - " + missingMethod);
                }

                sb.AppendLine();
            }
        }

        
        string FormatMethod(MethodDefinition method)
        {
            return "[" + method.Attributes + "] " + method.GetName();
        }

    }
}