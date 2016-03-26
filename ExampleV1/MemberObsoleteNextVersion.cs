using System;

namespace Example
{
    public class MemberObsoleteNextVersion
    {
        public string NotObsoletedFieldShouldBeIncluded;
        public void NotObsoletedMethodShouldBeIncluded() { }

        [Obsolete("", true)]
        public string StringFieldShouldNotBeIncluded;

        [Obsolete("", true)]
        public string StringPropertyShouldNotBeIncluded { get; set; }

        [Obsolete("", true)]
        public void MethodShouldNotBeIncluded()
        {
        }

        [Obsolete("", true)]
        public void MethodObsoleteInBothVersionsShouldNotBeIncluded()
        {
        }


        [Obsolete("", true)]
        public string PropertyObsoleteInBothVersionsShouldNotBeIncluded { get; set; }

        [Obsolete("", true)]
        public string FieldObsoleteInBothVersionsShouldNotBeIncluded;
    }
}