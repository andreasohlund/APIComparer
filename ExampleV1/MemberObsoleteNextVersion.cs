namespace Example
{
    using System;

    public class MemberObsoleteNextVersion
    {
        [Obsolete]
        public string StringProperty { get; set; }

        [Obsolete]
        public void Method()
        {
        }

        [Obsolete] public string StringField;
    }
}