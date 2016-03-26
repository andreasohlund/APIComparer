namespace Example
{
    using System;

    public class ClassWithMembersToBeObsoletedWithErrorInNextVersion
    {
        [Obsolete("Use xyz instead. Will be removed in version 2.0.0.", true)]
        public string StringField;

        [Obsolete("Can safely be removed. Will be removed in version 2.0.0.", true)]
        public string StringProperty { get; set; }

        [Obsolete("This is how to do it. Will be removed in version 2.0.0.", true)]
        public void Method()
        {
        }
        
        internal void InternalMethodWithNoChangeShouldNotBeIncluded()
        {

        }

#pragma warning disable 169
        internal string InternalFieldWithNoChangeShouldNotBeIncluded;
#pragma warning restore 169
    }
}