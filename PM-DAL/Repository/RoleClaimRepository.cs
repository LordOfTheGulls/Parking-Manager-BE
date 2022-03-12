using PM_DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Repository
{
    public class RoleClaimRepository : RepositoryBase<RoleClaim>, IRoleClaimRepository
    {
        private readonly PMDBContext context;

        public RoleClaimRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
