using Mono.Cecil;

namespace APIComparer.BreakingChanges
{
    public class PublicFieldRemoved:BreakingChange
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
}