using PM_Common.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Report
{
    public class GetReportForDate : IQuery<ReportResultDto>
    {
        public ReportByDateFilterDto ReportByDateFilter { get; set; }
    }
}
