using PM_Common.DTO;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Report;
using PM_Common.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.User
{
    public class GetUsersQuery : IQuery<PagingResult<UserDTO>>
    {
        public FilterDto Filter { get; set; }
    }
}
