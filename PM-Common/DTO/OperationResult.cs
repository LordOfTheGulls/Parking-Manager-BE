using PM_Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO
{
    public class OperationResult
    {
        public OperationResultStatus Status { get; set; }
        public string Message { get; set; }

        public OperationResult(OperationResultStatus status, string message)
        {
            Status = status;

            Message = message;
        }
    }
}
