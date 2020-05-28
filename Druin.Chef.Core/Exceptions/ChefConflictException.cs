using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Core.Exceptions
{
    public class ChefConflictException : Exception
    {
        public ChefConflictException() : base()
        { }

        public ChefConflictException(string message) : base(message)
        { }

        public ChefConflictException(string message, Exception inner) : base(message, inner)
        { }
    }
}