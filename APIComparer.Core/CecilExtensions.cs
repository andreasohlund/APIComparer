using System.ComponentModel;
using System.Linq;
using Mono.Cecil;

namespace APIComparer
{
    public static class CecilExtensions
    {
        public static bool HasObsoleteAttribute(this ICustomAttributeProvider value)
        {
            return value.CustomAttributes.Any(a => a.AttributeType.Name == "ObsoleteAttribute" || a.AttributeType.Name == "ObsoleteExAttribute");
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

        public static bool IsPublic(this IMemberDefinition memberDefinition)
        {
            var type = memberDefinition as TypeDefinition;
            if (type != null)
                return type.IsPublic;

            var field = memberDefinition as FieldDefinition;
            if (field != null)
                return field.IsPublic;

            var method = memberDefinition as MethodDefinition;
            if (method != null)
                return method.IsPublic;

            return false;
        }
    }
}