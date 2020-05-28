using System;


namespace Druin.Chef.Core.Exceptions
{
    public class ChefNotFoundException : Exception
    {
        public ChefNotFoundException() : base()
        { }

        public ChefNotFoundException(string message) : base(message)
        { }

        public ChefNotFoundException(string message, Exception inner) : base(message, inner)
        { }
    }
}