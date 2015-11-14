namespace Example
{
    public class MemberObsoleteNextVersion
    {
        internal string StringProperty { get; set; }

        internal void Method()
        {
        }
#pragma warning disable 169
        internal string StringField;
#pragma warning restore 169
    }
}