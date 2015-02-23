using Mono.Cecil;

public class FieldChangedToNonPublic : BreakingChange
{
    readonly TypeDefinition typeDefinition;
    readonly FieldDefinition field;

    public FieldChangedToNonPublic(TypeDefinition typeDefinition, FieldDefinition field)
    {
        this.typeDefinition = typeDefinition;
        this.field = field;
    }

    public override string Reason
    {
        get { return string.Format("Field {0} of type {1} has been made non public", field.Name, typeDefinition); }
    }
}