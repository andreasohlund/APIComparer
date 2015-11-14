namespace APIComparer.BreakingChanges
{
    using Mono.Cecil;

    public class TypeRemoved : BreakingChange
    {
        public TypeRemoved(TypeDefinition removedType)
        {
            this.removedType = removedType;
        }

        public override string Reason => $"Type {removedType} removed";

        TypeDefinition removedType;
    }
}