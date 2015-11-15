namespace Example
{
    using System;

    public class ClassWithMembersObsoletedInBothVersionsShouldNotBeIncluded
    {
        [Obsolete("",true)]
        public string StringField;

        [Obsolete("", true)]
        public string StringProperty { get; set; }

        [Obsolete("", true)]
        public void Method()
        {
        }
    }
}