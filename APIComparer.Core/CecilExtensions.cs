using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace APIComparer
{
    public static class CecilExtensions
    {
        public static bool HasObsoleteAttribute(this ICustomAttributeProvider value)
        {
            return value.GetObsoleteAttribute() != null;
        }

        public static bool ContainsAttribute<T>(this TypeDefinition typeDefinition)
        {
            var fullName = typeof(T).FullName;
            return typeDefinition.CustomAttributes.Any(x => x.AttributeType.FullName == fullName);
        }

        public static void RemoveTypes(this ModuleDefinition module, IEnumerable<TypeDefinition> typesToRemove)
        {
            foreach (var typeToRemove in typesToRemove)
            {
                module.RemoveType(typeToRemove);
            }
        }
        public static void RemoveType(this ModuleDefinition module, TypeDefinition typeDefinition)
        {

            if (typeDefinition.DeclaringType != null)
            {
                typeDefinition.DeclaringType.NestedTypes.Remove(typeDefinition);

            }
            module.Types.Remove(typeDefinition);
        }
        public static CustomAttribute GetObsoleteAttribute(this ICustomAttributeProvider value)
        {
            var obsoleteAttribute = value.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ObsoleteAttribute");

            if (obsoleteAttribute != null)
            {
                return obsoleteAttribute;
            }

            var property = value as PropertyDefinition;
            if (property != null)
            {
                obsoleteAttribute = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ObsoleteAttribute");

                if (obsoleteAttribute != null)
                {
                    return obsoleteAttribute;
                }

            }
            var @event = value as EventDefinition;
            if (@event != null)
            {
                obsoleteAttribute = @event.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ObsoleteAttribute");

                if (obsoleteAttribute != null)
                {
                    return obsoleteAttribute;
                }
            }
            var method = value as MethodDefinition;

            if (method != null)
            {
                if (method.IsGetter || method.IsSetter)
                {
                    foreach (var prop in method.DeclaringType.Properties)
                    {
                        if (prop.GetMethod == method || prop.SetMethod == method)
                        {
                            obsoleteAttribute = prop.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ObsoleteAttribute");

                            if (obsoleteAttribute != null)
                            {
                                return obsoleteAttribute;
                            }
                        }
                    }              
                }
            }
      
            return null;
        }
        
        public static string GetObsoleteString(this ICustomAttributeProvider value)
        {
            var arguments = value.GetObsoleteAttribute().ConstructorArguments;
            var message = "";
            if (arguments.Count == 1)
            {
                message = (string) arguments[0].Value;
            }
            var treatAsError = false;
            if (arguments.Count == 2)
            {
                message = (string) arguments[0].Value;
                treatAsError = (bool) arguments[1].Value;
            }

            if (!message.EndsWith("."))
            {
                message += ".";
            }
            if (treatAsError)
            {
                return message + " Obsoleted with error.";
            }
            return message + " Obsoleted with warning.";
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

        public static IEnumerable<TypeDefinition> RealTypes(this IEnumerable<TypeDefinition> types)
        {
            var typeDefinitions = new List<TypeDefinition>();
            foreach (var type in types)
            {
                if (type.IsAnonymous())
                {
                    continue;
                }
                typeDefinitions.AddRange(type.NestedTypes.Where(nestedType => !nestedType.IsAnonymous()));
                typeDefinitions.Add(type);
            }
            return typeDefinitions;
        }

        public static IEnumerable<FieldDefinition> RealFields(this TypeDefinition type)
        {
            return type.Fields.Where(x => !x.IsAnonymous());
        }

        public static IEnumerable<MethodDefinition> RealMethods(this TypeDefinition type)
        {
            return type.Methods.Where(x => !x.IsAnonymous() && x.Name !=  ".cctor");
        }

        public static bool IsAnonymous(this IMemberDefinition member)
        {

            return member.Name.StartsWith("<") ||
                member.Name.Contains(".<") || 
                member.Name.Contains("<>") || 
                member.Name.Contains("$<");
        }

        public static string GetName(this FieldDefinition field)
        {
            return field.FieldType.GetName() + " " + field.Name;
        }


        public static IEnumerable<TypeDefinition> TypeWithObsoletes(this IEnumerable<TypeDefinition> diff)
        {
            foreach (var typeDefinition in diff)
            {
                if (!typeDefinition.IsPublic)
                {
                    continue;
                }
                if (typeDefinition.HasObsoleteAttribute())
                {
                    yield return typeDefinition;
                    continue;
                }
                if (typeDefinition.GetObsoleteFields().Any())
                {
                    yield return typeDefinition;
                }
                if (typeDefinition.GetObsoleteMethods().Any())
                {
                    yield return typeDefinition;
                }
            }
        }

        public static IEnumerable<FieldDefinition> GetObsoleteFields(this TypeDefinition typeDefinition)
        {
            return typeDefinition.Fields.Where(x => x.IsPublic && x.HasObsoleteAttribute());
        }
        public static IEnumerable<MethodDefinition> GetObsoleteMethods(this TypeDefinition typeDefinition)
        {
            return typeDefinition.Methods.Where(x => x.IsPublic && x.HasObsoleteAttribute());
        }

        public static SequencePoint GetValidSequencePoint(this TypeDefinition type)
        {
            var sp = GetFirstSequencePoint(
                type.Methods.Select(x => x.GetValidSequencePoint()));

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
            return name;
        }

        static bool IsSystemType(this TypeReference type)
        {
            var assemblyNameReference = type.Scope as AssemblyNameReference;
            return assemblyNameReference != null && assemblyNameReference.FullName.Contains("b77a5c561934e089");
        }


    }
}