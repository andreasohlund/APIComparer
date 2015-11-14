namespace Example
{
    using System;

    public class ClassWithReadOnlyPropToBeRemovedInNextMajor
    {
        [Obsolete("Xyz. Will be removed in version 2.0.0.", true)]
        public string TheProperty
        {
            get { throw new InvalidOperationException(); }
            set { throw new InvalidOperationException(); }
        }
    }
}