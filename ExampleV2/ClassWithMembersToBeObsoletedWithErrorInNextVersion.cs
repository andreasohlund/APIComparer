namespace Example
{
    using System;

    public class ClassWithMembersToBeObsoletedWithErrorInNextVersion
    {
        [Obsolete("StringProperty. Will be removed in version 2.0.0.", true)]
        public string StringProperty { get; set; }

        [Obsolete("Method. Will be removed in version 2.0.0.", true)]
        public void Method()
        {
        }

        [Obsolete("StringField. Will be removed in version 2.0.0.", true)] public string StringField;
    }
}