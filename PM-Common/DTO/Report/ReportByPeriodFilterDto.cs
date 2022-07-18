using PM_Common.Enums.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Report
{
    public class ReportByPeriodFilterDto
    {
        public Int64 ParkingLotId { get; set; }
        public ReportType ReportType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
