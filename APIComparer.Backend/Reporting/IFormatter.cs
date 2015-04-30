namespace APIComparer.Backend.Reporting
{
    using System.IO;
    using System.Net.Mime;

    public interface IFormatter
    {
        ContentType ContentType { get; }

        void Render(TextWriter writer, PackageDescription description, DiffedCompareSet[] diffedCompareSets);
    }
}