using APIComparer;
using Mono.Cecil;

public class BreakingChange
{
    public static BreakingChange TypeRemoved(TypeDefinition typeDefinition)
    {
        return new BreakingChange("Type removed", typeDefinition.ToString());
    }
    public static BreakingChange TypeMadeNonPublic(TypeDiff typeDiff)
    {
        return new BreakingChange("Type made non public", typeDiff.RightType.ToString());
    }
    

    BreakingChange(string reason,string changedThing)
    {
        this.reason = reason;
        this.changedThing = changedThing;
    }


     


    public override string ToString()
    {
        return string.Format("{0}: {1}", reason, changedThing);
    }

 readonly string reason;
    readonly string changedThing;

}