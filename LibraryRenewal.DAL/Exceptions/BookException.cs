using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL.Exceptions
{
    public class BookException : DALException
    {
        public BookException()
        {
        }

        public BookException(string message)
            : base(message)
        {
        }

        public BookException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
