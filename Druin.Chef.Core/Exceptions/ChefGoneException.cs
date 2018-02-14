using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Core.Exceptions
{
    public class ChefGoneException : Exception
    {
        public ChefGoneException() : base()
        { }

        public ChefGoneException(string message) : base(message)
        { }

        public ChefGoneException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
