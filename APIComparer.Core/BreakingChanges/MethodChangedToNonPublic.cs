using Mono.Cecil;

namespace APIComparer.BreakingChanges
{
    public class MethodChangedToNonPublic : BreakingChange
    {
        readonly TypeDefinition typeDefinition;
        readonly MethodDefinition method;

        public MethodChangedToNonPublic(TypeDefinition typeDefinition, MethodDefinition method)
        {
            this.typeDefinition = typeDefinition;
            this.method = method;
        }


        public override string Reason
        {
            get { return string.Format("Method {0} of type {1} has been made non public", method.Name, typeDefinition); }
        }
    }
}