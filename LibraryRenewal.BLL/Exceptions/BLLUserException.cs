using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Exceptions
{
    public class BLLUserException : LibraryException
    {
        public BLLUserException()
        {
        }

        public BLLUserException(string message)
            : base(message)
        {
        }

        public BLLUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
