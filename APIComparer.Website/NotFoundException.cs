namespace APIComparer.Website
{
    using System;

    class NotFoundException : Exception
    {
        public NotFoundException(string message):base(message)
        {
        }
    }
}