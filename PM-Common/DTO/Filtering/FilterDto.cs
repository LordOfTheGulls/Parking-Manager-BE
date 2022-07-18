using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Sorting;

namespace PM_Common.DTO.Filtering
{
    public class FilterDto
    {
        public PagingDto? Paging { get; set; }
        public List<SortingDto>? Sorting { get; set; }
    }
}
