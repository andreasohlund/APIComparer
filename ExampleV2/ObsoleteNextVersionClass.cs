namespace Example
{
    internal class ObsoleteNextVersionClass
    {
        public string StringProperty { get; set; }

        public void Method()
        {
        }
#pragma warning disable 169
        public string StringField;
#pragma warning restore 169
    }
}