using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.Extensions.Datetime
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;

            return dt.AddDays(-1 * diff).Date;
        }
    }
}
