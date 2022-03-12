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
        private readonly PMDBContext context;

        public UserRepository(PMDBContext dbContext): base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); 
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username), cancellationToken); 

            if (user != null)
            {
                return user.Equals(password);
            }

            return false;
        }

        public async Task<UserDto> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                Id          = u.Id,
                UserName    = u.UserName,
                Email       = u.Email,
                FirstName   = u.FirstName,
                LastName    = u.LastName,
            }).FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);
        }

        public async Task<UserDto> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                Id        = u.Id,
                UserName  = u.UserName,
                Email     = u.Email,
                FirstName = u.FirstName,
                LastName  = u.LastName,
            }).FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<UserDto> FindByFirstNameAsync(string firstName, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                Id         = u.Id,
                UserName   = u.UserName,
                Email      = u.Email,
                FirstName  = u.FirstName,
                LastName   = u.LastName,
            }).FirstOrDefaultAsync(u => u.FirstName == firstName, cancellationToken);
        }

        public async Task<UserDto> FindByLastNameAsync(string lastName, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                Id        = u.Id,
                UserName  = u.UserName,
                Email     = u.Email,
                FirstName = u.FirstName,
                LastName  = u.LastName,
            }).FirstOrDefaultAsync(u => u.LastName == lastName, cancellationToken);
        }

        public async Task<UserDto> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                Id        = u.Id,
                UserName  = u.UserName,
                Email     = u.Email,
                FirstName = u.FirstName,
                LastName  = u.LastName,
            }).FirstOrDefaultAsync(u => (u.FirstName + " " + u.LastName) == name, cancellationToken);
        }
    }
}
