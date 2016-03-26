namespace Example
{
    using System;

    [Obsolete("Some message", true)]
    public class ClassObsoletedWithErrorShouldNotBeIncludedIfRemoved
    {
    }
}