namespace APIComparer.BreakingChanges
{
    using Mono.Cecil;

    public class MethodChangedToNonPublic : BreakingChange
    {
        public MethodChangedToNonPublic(TypeDefinition typeDefinition, MethodDefinition method)
        {
            this.typeDefinition = typeDefinition;
            this.method = method;
        }


        public override string Reason => $"Method {method.Name} of type {typeDefinition} has been made non public";

        private MethodDefinition method;
        private TypeDefinition typeDefinition;
    }
}