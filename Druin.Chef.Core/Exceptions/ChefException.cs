using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Core.Exceptions
{
    public class ChefException : Exception
    {
        public ChefException()
        {}

        public ChefException(string message):base(message)
        {}

        public ChefException(string message, Exception inner): base(message, inner)
        { }
    }
}
