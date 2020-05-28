using System;


namespace Druin.Chef.Core.Exceptions
{
    public class ChefUnauthorizedException : Exception
    {
        public ChefUnauthorizedException() : base()
        { }

        public ChefUnauthorizedException(string message) : base(message)
        { }

        public ChefUnauthorizedException(string message, Exception inner) : base(message, inner)
        { }
    }
}