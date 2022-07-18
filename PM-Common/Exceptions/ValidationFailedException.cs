using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException(string reason) : base(reason)
        {

        }
    }
}
