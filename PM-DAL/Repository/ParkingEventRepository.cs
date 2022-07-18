using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Repository
{
    public class ParkingEventRepository : RepositoryBase<ParkingEvent>, IParkingEventRepository
    {
        private readonly PMDBContext context;

        public ParkingEventRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
