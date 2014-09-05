using System;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace APIComparer.Outputters
{
    public class RawOutputFormatter : StringBuilderFormatter
    {
        StringBuilder stringBuilder;

        public RawOutputFormatter()
            : base(null, null)
        {
            stringBuilder = new StringBuilder();
        }

        public override void WriteOut(Diff diff)
        {
            stringBuilder.Clear();

            WriteOut(diff, stringBuilder);
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        protected override void WriteOut(Diff diff, StringBuilder sb)
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

        protected override string CreateLinkedLine<T>(Func<T, string> formatter, Func<T, SequencePoint> getSequencePoint, T left, T right,
            TypeDefinition fallbackRightType = null, int offset = 0)
        {
            return Environment.NewLine + formatter(left) + Environment.NewLine + (right != null ? formatter(right) : "[MISSING]");
        }

        protected override string FormatField(FieldDefinition field)
        {
            return "[" + field.Attributes + "] " + base.FormatField(field);
        }

        protected override string FormatMethod(MethodDefinition method)
        {
            return "[" + method.Attributes + "] " + base.FormatMethod(method);
        }

        protected override string FormatProperty(PropertyDefinition property)
        {
            return String.Format(
                "[{0}][{1}][{2}] {3}",
                property.Attributes,
                (property.GetMethod != null ? property.GetMethod.Attributes.ToString() : "MISSING"),
                (property.SetMethod != null ? property.SetMethod.Attributes.ToString() : "MISSING"),
                base.FormatProperty(property));
        }

        protected override string FormatFields(FieldDefinition left, FieldDefinition right, TypeDefinition fallbackRightType)
        {
            return Environment.NewLine + FormatField(left) + Environment.NewLine + (right != null ? FormatField(right) : "[MISSING]");
        }
    }
}