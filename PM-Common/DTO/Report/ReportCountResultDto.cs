using PM_Common.Enums.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Report
{
    public class ReportCountResultDto
    {
        public int TotalRecords { get; set; }
        public ReportType ReportType { get; set; }
        public DateTime Date { get; set; }
    }
}
