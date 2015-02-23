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
        get { return string.Format("Type removed: {0}", removedType); }
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
        get { return string.Format("Type made non public: {0}", typeDiff.RightType); }
    }
}