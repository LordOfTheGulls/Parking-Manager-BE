using PM_Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO
{
    public class SocketPayload
    {
        public SocketEmitType EmitType { get; set;} = SocketEmitType.None;
        public string PayloadType { get; set; }
        public object Payload { get; set; }
    }
}
