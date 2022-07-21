using PM_Common.DTO;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.User;
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
        public Task AddUser(CancellationToken cancellationToken);
        public Task UpdateUser(Int64 userId, UserDTO user, CancellationToken cancellationToken);
        public Task DeleteUser(Int64 userId, CancellationToken cancellationToken);
        public Task UpdatePermission(Int64 userId, UserRightDto userRight, CancellationToken cancellationToken);
        public Task<UserRightDto> GetUserPermissions(Int64 userId, CancellationToken cancellationToken);
        public Task<UserDTO> GetUser(Int64 userId, CancellationToken cancellationToken);
        public Task<PagingResult<UserDTO>> GetAllUsers(FilterDto filter, CancellationToken cancellationToken);
        public Task<bool> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByFirstNameAsync(string firstName, CancellationToken cancellationToken = default);
        public Task<UserDto> FindByLastNameAsync(string lastName, CancellationToken cancellationToken = default);
    }
}
