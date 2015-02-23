using Mono.Cecil;

public class BreakingChange
{
    public TypeDefinition TypeDefinition { get; set; }

    public string Reason { get; set; }

    internal BreakingChange(TypeDefinition typeDefinition)
    {
        TypeDefinition = typeDefinition;
        Reason = "Type removed";
    }

    public override string ToString()
    {
        return string.Format("{0}: {1}", Reason, TypeDefinition);
    }
}