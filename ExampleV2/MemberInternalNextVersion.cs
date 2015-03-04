namespace Example
{
    public class MemberInternalNextVersion
    {
#pragma warning disable 169
        internal string StringField;
#pragma warning restore 169

        internal string StringProperty { get; set; }

        internal void Method()
        {
        }
    }
}