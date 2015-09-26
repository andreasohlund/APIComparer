namespace Example
{
    using System;

    public class ClassWithMembersToBeObsoletedWithErrorInNextVersion
    {
        [Obsolete("FieldToBeObsoleted. Will be removed in version 2.0.0.", true)]
        public string FieldToBeObsoleted;
    }
}