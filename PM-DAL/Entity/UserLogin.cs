using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    public class UserLogin : IdentityUserLogin<Int64>
    {
        public virtual User User { get; set; }
    }
}
