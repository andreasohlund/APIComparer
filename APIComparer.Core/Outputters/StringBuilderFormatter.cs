using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
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

    public abstract class StringBuilderFormatter : IOutputter
    {
        string leftUrl;
        string rightUrl;

        public StringBuilderFormatter(string leftUrl = null, string rightUrl = null)
        {
            this.leftUrl = leftUrl;
            this.rightUrl = rightUrl;
        }

        public abstract void WriteOut(Diff diff);

        protected virtual void WriteOut(Diff diff, StringBuilder sb)
        {
            var missingTypes = diff.LeftOrphanTypes.Select(t => new Tuple<TypeDefinition, TypeDefinition>(t, null))
                .OrderBy(t => t.Item1.FullName)
                .Select(t => FormatTypes(t.Item1, t.Item2));

            sb.AppendLine("#### The following public types are missing in the new API.");
            sb.AppendLine();
            foreach (var missingType in missingTypes)
            {
                sb.AppendLine(HttpUtility.HtmlEncode(missingType) + "  ");
            }

            sb.AppendLine();

            missingTypes = diff.MatchingTypeDiffs.Select(td => Tuple.Create(td.LeftType, td.RightType))
                .OrderBy(t => t.Item1.FullName)
                .Select(t => FormatTypes(t.Item1, t.Item2));

            sb.AppendLine("#### The following types changed visibility in the new API.");
            sb.AppendLine();
            foreach (var missingType in missingTypes)
            {
                sb.AppendLine(HttpUtility.HtmlEncode(missingType) + "  ");
            }

            sb.AppendLine();

            sb.AppendLine("#### The following members are missing on the public types.");
            sb.AppendLine();
            foreach (var typeDiff in diff.MemberTypeDiffs.OrderBy(m => m.LeftType.FullName))
            {
                WriteOut(typeDiff, sb);
            }
        }

        protected virtual void WriteOut(TypeDiff typeDiff, StringBuilder sb)
        {
            var missingFields = typeDiff.LeftOrphanFields.Select(f => new Tuple<FieldDefinition, FieldDefinition>(f, null))
                .Concat(typeDiff.MatchingFields)
                .OrderBy(f => f.Item1.Name)
                .Select(t => FormatFields(t.Item1, t.Item2, typeDiff.RightType));

            var missingMethods = typeDiff.LeftOrphanMethods.Select(m => new Tuple<MethodDefinition, MethodDefinition>(m, null))
                .Concat(typeDiff.MatchingMethods)
                .OrderBy(t => t.Item1.Name)
                .Select(t => FormatMethods(t.Item1, t.Item2, typeDiff.RightType));

            var missingProperties = typeDiff.LeftOrphanProperties.Select(p => new Tuple<PropertyDefinition, PropertyDefinition>(p, null))
                .Concat(typeDiff.MatchingProperties)
                .OrderBy(t => t.Item1.Name)
                .Select(t => FormatProperties(t.Item1, t.Item2, typeDiff.RightType));

            if (missingFields.Any() || missingMethods.Any() || missingProperties.Any())
            {
                sb.AppendLine("- " + HttpUtility.HtmlEncode(FormatType(typeDiff.LeftType)));

                foreach (var missingField in missingFields)
                {
                    sb.AppendLine("  - " + HttpUtility.HtmlEncode(missingField));
                }

                foreach (var missingMethod in missingMethods)
                {
                    sb.AppendLine("  - " + HttpUtility.HtmlEncode(missingMethod));
                }

                foreach (var missingProperty in missingProperties)
                {
                    sb.AppendLine("  - " + HttpUtility.HtmlEncode(missingProperty));
                }

                sb.AppendLine();
            }
        }

        protected virtual string FormatField(FieldDefinition field)
        {
            return FormatType(field.FieldType) + " " + field.Name;
        }

        protected virtual string FormatMethod(MethodDefinition method)
        {
            var genericParams = method.GenericParameters.Select(FormatType);
            var methodParams = method.Parameters.Select(p => FormatType(p.ParameterType));

            var result = FormatType(method.ReturnType) + " " + method.Name;

            if (genericParams.Any())
                result = String.Format("{0}<{1}>", result, string.Join(", ", genericParams));

            result = String.Format("{0}({1})", result, string.Join(", ", methodParams));

            return result;
        }

        protected virtual string FormatProperty(PropertyDefinition property)
        {
            return String.Format("{0} {1} {{ {2} {3} }}", FormatType(property.PropertyType), property.Name, property.GetMethod != null ? "get;" : "", property.SetMethod != null ? "set;" : "");
        }

        protected virtual string FormatTypes(TypeDefinition left, TypeDefinition right)
        {
            return CreateLinkedLine(FormatType, GetValidSequencePoint, left, right);
        }

        protected virtual string FormatFields(FieldDefinition left, FieldDefinition right, TypeDefinition fallbackRightType)
        {
            return FormatField(left);
        }

        protected virtual string FormatMethods(MethodDefinition left, MethodDefinition right, TypeDefinition fallbackRightType)
        {
            return CreateLinkedLine(FormatMethod, GetValidSequencePoint, left, right, fallbackRightType, 2);
        }

        protected virtual string FormatProperties(PropertyDefinition left, PropertyDefinition right, TypeDefinition fallbackRightType)
        {
            return CreateLinkedLine(FormatProperty, GetValidSequencePoint, left, right, fallbackRightType, 2);
        }

        protected virtual string CreateLinkedLine<T>(Func<T, string> formatter, Func<T, SequencePoint> getSequencePoint, T left, T right, TypeDefinition fallbackRightType = null, int offset = 0) where T : class
        {
            var format = formatter(left);

            var leftSP = getSequencePoint(left);
            var rightSP = right != null ? getSequencePoint(right) :
                fallbackRightType != null ? GetValidSequencePoint(fallbackRightType) : null;

            var leftSPUrl = CreateSequencePointUrl(leftUrl, leftSP, offset);
            var rightSPUrl = rightSP != null ? CreateSequencePointUrl(rightUrl, rightSP, offset) : null;

            if (leftSPUrl != null && rightSPUrl != null)
            {
                return String.Format("{0} [ {1} | {2} ]", format, CreateMarkdownUrl("old", leftSPUrl), CreateMarkdownUrl("new", rightSPUrl));
            }
            if (leftSPUrl != null)
            {
                return String.Format("{0} [ {1} ]", format, CreateMarkdownUrl("old", leftSPUrl));
            }
            if (rightSPUrl != null)
            {
                return String.Format("{0} [ {1} ]", format, CreateMarkdownUrl("new", rightSPUrl));
            }
            return format;
        }

        protected virtual string CreateMarkdownUrl(string text, string url = null)
        {
            if (string.IsNullOrEmpty(url))
                return text;

            return String.Format("[{0}]({1})", text, url);
        }

        protected virtual SequencePoint GetValidSequencePoint(MethodDefinition method)
        {
            return method.HasBody ? method.Body.Instructions.Select(i => i.SequencePoint).FirstOrDefault(s => s != null && s.StartLine != 16707566) : null;
        }

        protected virtual SequencePoint GetValidSequencePoint(PropertyDefinition property)
        {
            var gsp = property.GetMethod != null ? GetValidSequencePoint(property.GetMethod) : null;
            var ssp = property.SetMethod != null ? GetValidSequencePoint(property.SetMethod) : null;

            return GetFirstSequencePoint(new[] { gsp, ssp });
        }

        protected virtual SequencePoint GetValidSequencePoint(TypeDefinition type)
        {
            var sp = GetFirstSequencePoint(
                type.Methods.Select(GetValidSequencePoint)
                .Concat(type.Properties.Select(GetValidSequencePoint)));

            if (sp == null)
                return null;

            return new SequencePoint(sp.Document);
        }

        protected virtual SequencePoint GetFirstSequencePoint(IEnumerable<SequencePoint> sequencePoints)
        {
            return sequencePoints.Where(sp => sp != null).DefaultIfEmpty().Aggregate((a, sp) => sp.StartLine < a.StartLine ? sp : a);
        }

        protected virtual string FormatType(TypeReference type)
        {
            var name = type.Name;
            var genericParams = Enumerable.Empty<string>();

            if (!string.IsNullOrEmpty(type.Namespace))
                name = type.Namespace + "." + name;

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

        protected virtual string CreateSequencePointUrl(string githubBase, SequencePoint sequencePoint, int offset = 0)
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
    }
}