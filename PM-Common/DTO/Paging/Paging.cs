using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Paging
{
    public class PagingDto
    {
        public int Page { set; get; }
        public int PageSize { set; get; }

        public int Skip => (Page > 1 ? PageSize * (Page - 1) : 0);
        public int Take => (PageSize > 1 ? PageSize : 0);
    }
}
