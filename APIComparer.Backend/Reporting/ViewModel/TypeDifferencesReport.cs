namespace APIComparer.Backend.Reporting
{
    using System.Collections.Generic;
    using System.Linq;

    public class TypeDifferencesReport
    {
        public string name { get; set; }
        public bool hasBeenObsoleted { get; set; }
        public string obsoleteMessage { get; set; }

        public bool hasFieldsChangedToNonPublic { get; set; }
        public IEnumerable<RemovedItem> fieldsChangedToNonPublic { get; set; }

        public bool hasFieldsObsoleted { get; set; }
        public IEnumerable<ObsoletedItem> fieldsObsoleted { get; set; }

        public bool hasFieldsRemoved { get; set; }
        public IEnumerable<RemovedItem> fieldsRemoved { get; set; }

        public bool hasMethodsChangedToNonPublic { get; set; }
        public IEnumerable<RemovedItem> methodsChangedToNonPublic { get; set; }

        public bool hasMethodsRemoved { get; set; }
        public IEnumerable<RemovedItem> methodsRemoved { get; set; }

        public bool hasMethodsObsoleted { get; set; }
        public IEnumerable<ObsoletedItem> methodsObsoleted { get; set; }

        public int BreakingChanges
        {
            get
            {
                return fieldsChangedToNonPublic.Count()
                       + fieldsRemoved.Count()
                       + methodsChangedToNonPublic.Count()
                       + methodsRemoved.Count();
            }
        }
    }
}
