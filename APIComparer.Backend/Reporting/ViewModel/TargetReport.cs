namespace APIComparer.Backend.Reporting
{
    using System.Collections.Generic;

    public class TargetReport
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ComparedTo { get; set; }
        public bool noLongerSupported { get; set; }
        public bool hasRemovedPublicTypes { get; set; }
        public IEnumerable<RemovedItem> removedPublicTypes { get; set; }
        public bool hasTypesMadeInternal { get; set; }
        public IEnumerable<RemovedItem> typesMadeInternal { get; set; }
        public bool hasTypeDifferences { get; set; }
        public IEnumerable<TypeDifferencesReport> typeDifferences { get; set; }
        public bool hasChanges { get; set; }
    }
}
