using Mono.Cecil;

namespace APIComparer.BreakingChanges
{
    public class PublicMethodRemoved : BreakingChange
    {
        readonly TypeDefinition typeDefinition;
        readonly MethodDefinition method;

        public PublicMethodRemoved(TypeDefinition typeDefinition, MethodDefinition method)
        {
            this.typeDefinition = typeDefinition;
            this.method = method;
        }

        public override string Reason
        {
            get { return string.Format("Method {0} removed from type {1}", method.Name, typeDefinition); }
        }
    }
}