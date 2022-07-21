using PM_Common.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.User
{
    public class GetUserQuery : IQuery<UserDTO>
    {
        public Int64 UserId { get; set; }
    }
}
