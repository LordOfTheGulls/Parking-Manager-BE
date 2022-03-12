using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("user_role")]
    public class UserRole : IdentityUserRole<Int64>
    {

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
