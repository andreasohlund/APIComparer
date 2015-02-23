using APIComparer;

public class TypeMadeNonPublic: BreakingChange
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