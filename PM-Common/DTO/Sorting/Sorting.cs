using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Sorting
{
    public enum SortOrder {
        None       = 0,
        Ascending  = 1,
        Descending = 2,
    }

    public class SortingDto{
        public string ColumnId { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
