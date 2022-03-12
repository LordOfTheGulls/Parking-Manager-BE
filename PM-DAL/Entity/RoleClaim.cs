using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("role_claim")]
    public class RoleClaim: IdentityRoleClaim<Int64>
    {
        public virtual Role Role { get; set; }
    }
}
