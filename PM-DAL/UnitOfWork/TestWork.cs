using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.UOW
{

    public class TestWork
    {
        PMDBContext context;

        public async Task<IUnitOfWork> OpenAsync()
        {
            var uof = new UnitOfWork(context);

            try
            {
                await uof.OpenConnectionAsync();
            }
            catch
            {
                uof.Dispose();
                throw;
            }

            return uof;
        }
    }
}
