namespace Example
{
    public class ClassWithPrivateMembersRemovedShouldNotBeIncluded
    {
#pragma warning disable 169
        string StringField;
#pragma warning restore 169

        string StringProperty { get; set; }

        void Method()
        {
        }
    }
}