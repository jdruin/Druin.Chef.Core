using System;


namespace Druin.Chef.Core.Exceptions
{
    public class ChefException : Exception
    {
        public ChefException() : base()
        { }

        public ChefException(string message) : base(message)
        { }

        public ChefException(string message, Exception inner) : base(message, inner)
        { }
    }
}