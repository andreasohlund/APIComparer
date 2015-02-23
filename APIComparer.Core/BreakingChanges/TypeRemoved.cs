using Mono.Cecil;

namespace APIComparer.BreakingChanges
{
    public class TypeRemoved : BreakingChange
    {
        readonly TypeDefinition removedType;

        public TypeRemoved(TypeDefinition removedType)
        {
            this.removedType = removedType;
        }

        public override string Reason
        {
            get { return string.Format("Type {0} removed", removedType); }
        }
    }
}