using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.Exceptions
{
    public class EntityDoesNotExistException : Exception
    {
        public EntityDoesNotExistException(Int64 id, Type entity) : base($"Entity {entity} with ID {id} does not exist!")
        {

        }
    }
}
