using APIComparer.Filters;
using Mono.Cecil;

namespace APIComparer
{
    public class NServiceBusAPIFilter : BaseAPIFilter
    {
        public override bool FilterLeftType(TypeDefinition type)
        {
            return !type.Name.StartsWith("<>");
        }

        public override bool FilterRightType(TypeDefinition type)
        {
            return false;
        }

        public override bool FilterMatchedType(TypeDiff diff)
        {
            return !diff.LeftType.Name.StartsWith("<>");
        }
    }
}