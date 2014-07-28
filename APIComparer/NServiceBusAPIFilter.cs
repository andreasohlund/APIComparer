using APIComparer.Filters;
using Mono.Cecil;

namespace APIComparer
{
    public class NServiceBusAPIFilter : BaseAPIFilter
    {
        public override bool FilterLeftType(TypeDefinition type)
        {
            return !type.Name.StartsWith("<>") && type.IsPublic && !type.HasObsoleteAttribute();
        }

        public override bool FilterRightType(TypeDefinition type)
        {
            return false;
        }

        public override bool FilterMatchedType(TypeDiff diff)
        {
            return !diff.LeftType.Name.StartsWith("<>") && diff.LeftType.IsPublic && !diff.LeftType.HasObsoleteAttribute() && !diff.RightType.IsPublic;
        }

        public override bool FilterLeftField(FieldDefinition field)
        {
            return field.IsPublic && !field.HasObsoleteAttribute();
        }

        public override bool FilterMatchedField(FieldDefinition left, FieldDefinition right)
        {
            return left.IsPublic && !left.HasObsoleteAttribute() && !right.IsPublic;
        }

        public override bool FilterLeftProperty(PropertyDefinition property)
        {
            return property.IsPublic() && !property.HasObsoleteAttribute();
        }

        public override bool FilterLeftMethod(MethodDefinition method)
        {
            return method.IsPublic && !method.HasObsoleteAttribute() && FilterMethods(method);
        }

        public override bool FilterMatchedMethod(MethodDefinition left, MethodDefinition right)
        {
            return left.IsPublic && !left.HasObsoleteAttribute() && !right.IsPublic && FilterMethods(left);
        }

        private bool FilterMethods(MethodDefinition method)
        {
            return !method.IsConstructor // Not constructors
                && (!method.IsVirtual || !method.IsReuseSlot); // Not public overrides
        }
    }
}