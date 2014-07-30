using System;

namespace Example
{
    public class MemberObsoleteNextVersion
    {
        [Obsolete]
        public string StringField;

        [Obsolete]
        public string StringProperty { get; set; }

        [Obsolete]
        public void Method()
        {
        }
    }
}