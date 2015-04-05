namespace APIComparer
{
    using System.Linq;

    public class EmptyAssemblyGroup : AssemblyGroup
    {
        public EmptyAssemblyGroup() : base(Enumerable.Empty<string>())
        {
        }
    }
}