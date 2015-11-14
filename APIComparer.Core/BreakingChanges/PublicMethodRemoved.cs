namespace APIComparer.BreakingChanges
{
    using Mono.Cecil;

    public class PublicMethodRemoved : BreakingChange
    {
        public PublicMethodRemoved(TypeDefinition typeDefinition, MethodDefinition method)
        {
            this.typeDefinition = typeDefinition;
            this.method = method;
        }

        public override string Reason => $"Method {method.Name} removed from type {typeDefinition}";

        MethodDefinition method;
        TypeDefinition typeDefinition;
    }
}