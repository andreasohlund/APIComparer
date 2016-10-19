using Mono.Cecil;

namespace APIComparer.BreakingChanges
{
    public class EnumFieldValueChanged : BreakingChange
    {
        public EnumFieldValueChanged(TypeDefinition typeDefinition, FieldDefinition leftField, FieldDefinition rightField)
        {
            this.typeDefinition = typeDefinition;
            this.leftField = leftField;
            this.rightField = rightField;
        }

        public override string Reason => $"Member {leftField.Name} of type {typeDefinition} value has been changed from {leftField.Constant} to {rightField.Constant}";

        FieldDefinition leftField;
        FieldDefinition rightField;
        TypeDefinition typeDefinition;
    }
}
