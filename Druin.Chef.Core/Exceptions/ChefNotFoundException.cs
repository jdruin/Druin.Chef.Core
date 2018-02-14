using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
