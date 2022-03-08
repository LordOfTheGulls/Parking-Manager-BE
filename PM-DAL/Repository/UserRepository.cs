using Microsoft.EntityFrameworkCore;
using PM_Common.DTO;
using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext): base(dbContext)
        {

        }

        public UserDto FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public UserDto FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public UserDto FindByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public UserDto FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public UserDto FindByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
