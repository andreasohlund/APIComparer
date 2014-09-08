using Mono.Cecil;

namespace APIComparer
{
    public class MatchingMember<T> where T : IMemberDefinition
    {
        public T Left;
        public T Right;
    }
}