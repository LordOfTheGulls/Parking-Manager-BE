using PM_DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Repository
{
    public class UserTokenRepository : RepositoryBase<UserToken>, IUserTokenRepository
    {
        private readonly PMDBContext context;

        public UserTokenRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
