using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Chart
{
    public class ChartDto
    {
    }

    public class WeeklyChartDto<T> : ChartDto
    {
        public Dictionary<int, T> WeeklyData { get; set; }
    }
}
