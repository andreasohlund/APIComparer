namespace APIComparer.BreakingChanges
{
    using Mono.Cecil;

    public class PublicFieldRemoved : BreakingChange
    {
        public PublicFieldRemoved(TypeDefinition typeDefinition, FieldDefinition fieldDefinition)
        {
            this.typeDefinition = typeDefinition;
            this.fieldDefinition = fieldDefinition;
        }


        public override string Reason => $"Public field {fieldDefinition.Name} removed from {typeDefinition}";

        private FieldDefinition fieldDefinition;
        private TypeDefinition typeDefinition;
    }
}