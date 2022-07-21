using PM_Common.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class UpdateUserCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public UserDTO User { get; set; }
    }
}
