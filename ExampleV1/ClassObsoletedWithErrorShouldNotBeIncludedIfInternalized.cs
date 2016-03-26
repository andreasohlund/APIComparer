using System;

namespace Example
{
    [Obsolete("Some message", true)]
    public class ClassObsoletedWithErrorShouldNotBeIncludedIfInternalized
    {
    }
}