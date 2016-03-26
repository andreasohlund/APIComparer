namespace Example
{
    using System;

    public class MemberObsoleteNextVersion
    {
#pragma warning disable 169
        internal string StringField;
#pragma warning restore 169

        internal string StringProperty { get; set; }

        internal void Method()
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