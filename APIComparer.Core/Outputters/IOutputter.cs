namespace APIComparer.Outputters
{
    public interface IOutputter
    {
        void WriteOut(Diff diff);
    }
}