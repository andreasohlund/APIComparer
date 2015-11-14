namespace APIComparer.BreakingChanges
{
    using Mono.Cecil;

    public class FieldChangedToNonPublic : BreakingChange
    {
        public FieldChangedToNonPublic(TypeDefinition typeDefinition, FieldDefinition field)
        {
            this.typeDefinition = typeDefinition;
            this.field = field;
        }

        public override string Reason => $"Field {field.Name} of type {typeDefinition} has been made non public";

        FieldDefinition field;
        TypeDefinition typeDefinition;
    }
}