using PM_Common.DTO;
using PM_Common.Enums;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class DeleteParkingBlacklistCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public Int64 ParkingBlackListId { get; set; }
    }
}