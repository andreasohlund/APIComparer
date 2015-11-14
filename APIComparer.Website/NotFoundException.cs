namespace APIComparer.Website
{
    using System;

    internal class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}