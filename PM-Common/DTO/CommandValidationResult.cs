using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO
{
    public class CommandValidationResult
    {
        public bool Succeeded { get; private set; }

        public bool Failed => !Succeeded;

        public string Reason { get; private set; }

        public CommandValidationResult(string reason, bool success)
        {
            Reason    = reason;
            Succeeded = success;
        }
    }

    public static class CommandValidation
    {
        public static CommandValidationResult Succeeded(string reason)
        {
            return new CommandValidationResult(reason, true);
        }

        public static CommandValidationResult Failed(string reason)
        {
            return new CommandValidationResult(reason, false);
        }
    }
}
