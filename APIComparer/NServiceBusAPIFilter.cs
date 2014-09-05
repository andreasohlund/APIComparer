using EqualityComparers;
using Mono.Cecil;

namespace APIComparer
{
    public class NServiceBusAPIFilter : BaseAPIFilter
    {
        public NServiceBusAPIFilter()
        {
            MethodComparer = EqualityCompare<MethodDefinition>
                .EquateBy(m => m.DeclaringType.FullName)
                .ThenEquateBy(m => m.Name);
        }

        public override bool FilterLeftType(TypeDefinition type)
        {
            return
                !IsAnonymousType(type) &&

                type.IsPublic &&
                !type.EditorBrowsableStateNever() &&
                !type.HasObsoleteAttribute();
        }

        public override bool FilterRightType(TypeDefinition type)
        {
            return false;
        }

        public override bool FilterMatchedType(TypeDiff diff)
        {
            return !(
                IsAnonymousType(diff.LeftType) ||

                !diff.LeftType.IsPublic ||
                diff.LeftType.EditorBrowsableStateNever() ||

                diff.RightType.IsPublic ||

                diff.LeftType.HasObsoleteAttribute()

                );
        }

        public override bool FilterMemberTypeDiff(TypeDiff diff)
        {
            return !IsAnonymousType(diff.LeftType) &&
                diff.LeftType.IsPublic &&
                !diff.LeftType.EditorBrowsableStateNever() &&
                diff.RightType.IsPublic &&
                !diff.RightType.HasObsoleteAttribute();
        }

        public override bool FilterLeftField(FieldDefinition field)
        {
            return
                field.IsPublic &&
                !field.EditorBrowsableStateNever() &&
                !field.HasObsoleteAttribute();
        }

        public override bool FilterRightField(FieldDefinition field)
        {
            return false;
        }

        public override bool FilterMatchedField(FieldDefinition left, FieldDefinition right)
        {
            return
                left.IsPublic &&
                !left.EditorBrowsableStateNever() &&
                !left.HasObsoleteAttribute() &&
                !right.IsPublic;
        }

        public override bool FilterLeftProperty(PropertyDefinition property)
        {
            return
                property.IsPublic() &&
                !property.EditorBrowsableStateNever() &&
                !property.HasObsoleteAttribute();
        }

        public override bool FilterRightProperty(PropertyDefinition property)
        {
            return false;
        }

        public override bool FilterMatchedProperty(PropertyDefinition left, PropertyDefinition right)
        {
            return
                !left.EditorBrowsableStateNever() &&
                !left.HasObsoleteAttribute() &&
                (left.GetMethod != null && right.GetMethod != null && CommonFilterMatchedMethod(left.GetMethod, right.GetMethod)) &&
                (left.SetMethod != null && right.SetMethod != null && CommonFilterMatchedMethod(left.SetMethod, right.SetMethod));
        }

        public override bool FilterLeftMethod(MethodDefinition method)
        {
            return
                method.IsPublic &&
                !method.EditorBrowsableStateNever() &&
                !method.HasObsoleteAttribute() &&
                FilterMethods(method);
        }

        public override bool FilterRightMethod(MethodDefinition method)
        {
            return false;
        }

        public override bool FilterMatchedMethod(MethodDefinition left, MethodDefinition right)
        {
            return CommonFilterMatchedMethod(left, right) &&
                FilterMethods(left);
        }

        bool CommonFilterMatchedMethod(MethodDefinition left, MethodDefinition right)
        {
            return
                left.IsPublic &&
                !left.EditorBrowsableStateNever() &&
                !left.HasObsoleteAttribute() &&
                !right.IsPublic;
        }

        bool FilterMethods(MethodDefinition method)
        {
            return !method.IsConstructor // not constructors
                && (!method.IsVirtual || !method.IsReuseSlot) // not public overrides
                && !method.IsGetter && !method.IsSetter; // not property methods
        }

        bool IsAnonymousType(TypeDefinition type)
        {
            return type.Name.StartsWith("<>");
        }
    }
}