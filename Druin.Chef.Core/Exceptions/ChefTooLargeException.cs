using System;


namespace Druin.Chef.Core.Exceptions
{
    public class ChefTooLargeException : Exception
    {
        public ChefTooLargeException() : base()
        { }

        public ChefTooLargeException(string message) : base(message)
        { }

        public ChefTooLargeException(string message, Exception inner) : base(message, inner)
        { }
    }
}