﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public Int64 UserId { get; set; }
    }
}
