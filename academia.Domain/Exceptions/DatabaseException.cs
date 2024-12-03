using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace academia.Domain.Exceptions
{
    public class DatabaseException : Exception
    {
        //classe de exception personalizada
        public DatabaseException()
        {
        }

        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception exception) : base(message, exception) { }
    }
}
