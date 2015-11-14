namespace APIComparer
{
    using Mono.Cecil;

    public class MatchingMember<T> where T : IMemberDefinition
    {
        public T Left;
        public T Right;
    }
}