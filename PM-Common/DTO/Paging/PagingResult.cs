using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Paging
{
    public class PagingResult<T> where T : class
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }
    }
}
