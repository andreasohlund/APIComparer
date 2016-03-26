namespace Example
{
    using System;

    public class ClassWithMembersToBeObsoletedWithWarnInNextVersion
    {
        [Obsolete("Use xyz instead. Will be treated as an error from version 2.0.0. Will be removed in version 3.0.0.", false)]
        public string StringField;

        [Obsolete("Can safely be removed. Will be treated as an error from version 2.0.0. Will be removed in version 3.0.0.", false)]
        public string StringProperty { get; set; }

        [Obsolete("This is how to do it. Will be treated as an error from version 2.0.0. Will be removed in version 3.0.0.", false)]
        public void Method()
        {
        }
    }
}