using Mono.Cecil;

namespace APIComparer.BreakingChanges
{
    public class EnumFieldValueChanged : BreakingChange
    {
        public EnumFieldValueChanged(TypeDefinition typeDefinition, FieldDefinition field)
        {
            this.typeDefinition = typeDefinition;
            this.field = field;
        }

        public override string Reason => $"Member {field.Name} of Enum type {typeDefinition} value has been changed";

        FieldDefinition field;
        TypeDefinition typeDefinition;
    }
}
