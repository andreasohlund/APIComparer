using APIComparer;
using EqualityComparers;
using Mono.Cecil;

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
               && (!method.IsVirtual || !method.IsReuseSlot);
    }

    bool IsAnonymousType(TypeDefinition type)
    {
        return type.Name.StartsWith("<>");
    }
}