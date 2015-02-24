namespace APIComparer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class AssemblyGroup
    {
        public AssemblyGroup(IEnumerable<string> assemblies)
        {
            Assemblies = assemblies.ToList();

            ReadSymbols = Assemblies.All(a => File.Exists(Path.Combine(Path.GetDirectoryName(a),Path.GetFileNameWithoutExtension(a) +".pdb")));
        }

        public IEnumerable<string> Assemblies { get; private set; }
        public bool ReadSymbols { get; private set; }
    }
}