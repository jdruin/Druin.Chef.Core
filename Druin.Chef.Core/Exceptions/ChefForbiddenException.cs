using System;


namespace Druin.Chef.Core.Exceptions
{
    public class ChefForbiddenException : Exception
    {
        public ChefForbiddenException() : base()
        { }

        public ChefForbiddenException(string message) : base(message)
        { }

        public ChefForbiddenException(string message, Exception inner) : base(message, inner)
        { }
    }
}
