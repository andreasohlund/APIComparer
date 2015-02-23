namespace APIComparer.BreakingChanges
{
    public abstract class BreakingChange
    {
        public abstract string Reason { get; }

        public override string ToString()
        {
            return Reason;
        }

    }
}