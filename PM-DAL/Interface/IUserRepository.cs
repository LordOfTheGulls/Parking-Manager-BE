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
        public Task<bool> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByFirstNameAsync(string firstName, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByLastNameAsync(string lastName, CancellationToken cancellationToken = default);
    }
}
