namespace APIComparer.BreakingChanges
{
    public class TypeMadeNonPublic : BreakingChange
    {
        public TypeMadeNonPublic(TypeDiff typeDiff)
        {
            this.typeDiff = typeDiff;
        }

        public override string Reason => $"Type {typeDiff.RightType} made non public";

        TypeDiff typeDiff;
    }
}