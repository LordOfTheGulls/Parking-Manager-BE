using PM_Common.DTO;
using PM_DAL.Entity;
using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interface
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        UserDto FindByUsername(string username);
        UserDto FindByEmail(string email);
        UserDto FindByName(string name);
        UserDto FindByFirstName(string firstName);
        UserDto FindByLastName(string lastName);
    }
}
