using APIComparer;
using Mono.Cecil;

public abstract class BreakingChange
{
    public abstract string Reason { get; }

    public override string ToString()
    {
        return Reason;
    }

}

class TypeRemoved : BreakingChange
{
    readonly TypeDefinition removedType;

    public TypeRemoved(TypeDefinition removedType)
    {
        this.removedType = removedType;
    }

    public override string Reason
    {
        get { return string.Format("Type {0} removed", removedType); }
    }
}

class TypeMadeNonPublic: BreakingChange
{
    readonly TypeDiff typeDiff;
   
    public TypeMadeNonPublic(TypeDiff typeDiff)
    {
        this.typeDiff = typeDiff;
    }

    public override string Reason
    {
        get { return string.Format("Type {0} made non public", typeDiff.RightType); }
    }
}

class PublicFieldRemoved:BreakingChange
{
    readonly TypeDefinition typeDefinition;
    readonly FieldDefinition fieldDefinition;

    public PublicFieldRemoved(TypeDefinition typeDefinition, FieldDefinition fieldDefinition)
    {
        this.typeDefinition = typeDefinition;
        this.fieldDefinition = fieldDefinition;
    }


    public override string Reason
    {
        get
        {
             return string.Format("Public field {0} removed from {1}", fieldDefinition.Name,typeDefinition);
        }
    }
}