using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace APIComparer
{
    public static class CecilExtensions
    {

        public static bool HasObsoleteAttribute(this ICustomAttributeProvider value)
        {
            return value.CustomAttributes.Any(a => a.AttributeType.Name == "ObsoleteAttribute");
        }

        public static string GetName(this PropertyDefinition property)
        {
            return String.Format("{0} {1} {{ {2} {3} }}", property.PropertyType.GetName(), property.Name, property.GetMethod != null ? "get;" : "", property.SetMethod != null ? "set;" : "");
        }


        public static string GetName(this MethodDefinition method)
        {
            var genericParams = method.GenericParameters.Select(x => x.GetName()).ToList();
            var methodParams = method.Parameters.Select(p => p.ParameterType.GetName());

            var result = method.ReturnType.GetName() + " " + method.Name;

            if (genericParams.Any())
                result = String.Format("{0}<{1}>", result, string.Join(", ", genericParams));

            return String.Format("{0}({1})", result, string.Join(", ", methodParams));
        }

        public static SequencePoint GetValidSequencePoint(this PropertyDefinition property)
        {
            var gsp = property.GetMethod != null ? property.GetMethod.GetValidSequencePoint() : null;
            var ssp = property.SetMethod != null ? property.SetMethod.GetValidSequencePoint() : null;

            return GetFirstSequencePoint(new[] { gsp, ssp });
        }

        public static SequencePoint GetValidSequencePoint(this TypeDefinition type)
        {
            var sp = GetFirstSequencePoint(
                type.Methods.Select(x => x.GetValidSequencePoint())
                .Concat(type.Properties.Select(GetValidSequencePoint)));

            if (sp == null)
                return null;

            return new SequencePoint(sp.Document);
        }

        public static SequencePoint GetFirstSequencePoint(this IEnumerable<SequencePoint> sequencePoints)
        {
            return sequencePoints.Where(sp => sp != null)
                .DefaultIfEmpty()
                .Aggregate((a, sp) => sp.StartLine < a.StartLine ? sp : a);
        }

        public static SequencePoint GetValidSequencePoint(this MethodDefinition method)
        {
            if (!method.HasBody)
            {
                return null;
            }
            return method.Body.Instructions
                .Select(i => i.SequencePoint)
                .FirstOrDefault(s => s != null && s.StartLine != 16707566);
        }

        public static string GetName(this TypeReference self)
        {
            if (self.FullName == "System.Void")
            {
                return "void";
            }
            if (self.FullName == "System.Boolean")
            {
                return "bool";
            }
            if (self.FullName == "System.String")
            {
                return "string";
            }
            if (self.FullName == "System.Int32")
            {
                return "int";
            }
            if (self.FullName == "System.Int64")
            {
                return "long";
            }
            if (self.FullName == "System.Object")
            {
                return "object";
            }

            string name;
            if (self.IsSystemType())
            {
                name = self.Name;
            }
            else
            {
                name = self.FullName;   
            }
            var genericInstanceType = self as GenericInstanceType;
            if (genericInstanceType != null)
            {
                var first = name.Split('`').First();
                return first + "<" + string.Join(", ", genericInstanceType.GenericArguments.Select(x=>x.GetName())) + ">";
            }

            if (self.HasGenericParameters)
            {
                var first = name.Split('`').First();
                return first + "<" + string.Join(", ", self.GenericParameters.Select(x => x.GetName())) + ">";
            }
            if (self.IsArray)
            {
                return name + "[]";
            }
            return name;
        }

        static bool IsSystemType(this TypeReference type)
        {
            var assemblyNameReference = type.Scope as AssemblyNameReference;
            return assemblyNameReference != null && assemblyNameReference.FullName.Contains("b77a5c561934e089");
        }

        public static bool EditorBrowsableStateNever(this ICustomAttributeProvider value)
        {
            var editorBrowsable = value.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "EditorBrowsableAttribute");

            if (editorBrowsable != null && editorBrowsable.ConstructorArguments.Count == 1)
            {
                var state = (EditorBrowsableState)editorBrowsable.ConstructorArguments[0].Value;
                return state == EditorBrowsableState.Never;
            }

            return false;
        }

        public static bool IsPublic(this PropertyDefinition property)
        {
            return (property.GetMethod != null && property.GetMethod.IsPublic) ||
                   (property.SetMethod != null && property.SetMethod.IsPublic);
        }
    }
}