using Microsoft.EntityFrameworkCore;
using PM_Common.DTO;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.User;
using PM_Common.Exceptions;
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

        public async Task UpdatePermission(Int64 userId, UserRightDto userRight, CancellationToken cancellationToken)
        {
            var claims = await context.UserClaims.Where(e => e.UserId == userId).ToListAsync();

            claims.FirstOrDefault(c => c.ClaimType == "CANEDITBLACKLIST").ClaimValue   = userRight.CanEditBlacklist.ToString();
            claims.FirstOrDefault(c => c.ClaimType == "CANEDITWORKHOURS").ClaimValue   = userRight.CanEditWorkhours.ToString();
            claims.FirstOrDefault(c => c.ClaimType == "CANEDITPARKINGRATE").ClaimValue = userRight.CanEditParkingRate.ToString();
        }

        public async Task DeleteUser(Int64 userId, CancellationToken cancellationToken)
        {
            var query = context.Users;

            User? userRes = await query.FirstOrDefaultAsync(e => e.Id == userId, cancellationToken);

            if (userRes == null)
                throw new EntityDoesNotExistException(userId, typeof(User));

            context.Users.Remove(userRes);
        }

        public async Task AddUser(CancellationToken cancellationToken)
        {
            var query = await context.Users.AddAsync(new User()
            {
                FirstName = "N/A",
                LastName = "N/A",
                UserName = "N/A",
                PasswordHash = "N/A",
                Email = "N/A",
                PhoneNumber = "N/A"
            });
        }

        public async Task UpdateUser(Int64 userId, UserDTO user, CancellationToken cancellationToken)
        {
            var query = context.Users;

            User? userRes = await query.FirstOrDefaultAsync(e => e.Id == userId, cancellationToken);

            if (userRes == null)
                throw new EntityDoesNotExistException(userId, typeof(User));

            userRes.Email = user.Email;
            userRes.FirstName = user.FirstName;
            userRes.LastName = user.LastName;
            userRes.Email = user.Email;
            userRes.PhoneNumber = user.Phone;
            userRes.UserName = user.Username;
        }

        public async Task<UserRightDto> GetUserPermissions(Int64 userId, CancellationToken cancellationToken)
        {
            var claims = await context.UserClaims.AsNoTracking().Where(e => e.UserId == userId).ToListAsync();

            if(claims.Count() > 0)
            {
                return new UserRightDto()
                {
                    CanEditBlacklist = bool.Parse(claims.FirstOrDefault(v => v.ClaimType == "CANEDITBLACKLIST").ClaimValue),
                    CanEditParkingRate = bool.Parse(claims.FirstOrDefault(v => v.ClaimType == "CANEDITPARKINGRATE").ClaimValue),
                    CanEditWorkhours = bool.Parse(claims.FirstOrDefault(v => v.ClaimType == "CANEDITWORKHOURS").ClaimValue),
                };
            }
          
            return new UserRightDto();
        }

        public async Task<UserDTO> GetUser(Int64 userId, CancellationToken cancellationToken)
        {
            var query = context.Users.AsNoTracking();

            var result = await query.FirstOrDefaultAsync(e => e.Id == userId, cancellationToken);

            var claims = await context.UserClaims.Where(e => e.UserId == userId).ToListAsync();

            var user = new UserDTO()
            {
                Username = result.UserName,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                UserId = result.Id,
                Phone = result.PhoneNumber,
                UserRights = new UserRightDto()
                {
                    CanEditBlacklist   = bool.Parse(claims.FirstOrDefault(v => v.ClaimType == "CANEDITBLACKLIST").ClaimValue),
                    CanEditParkingRate = bool.Parse(claims.FirstOrDefault(v => v.ClaimType == "CANEDITPARKINGRATE").ClaimValue),
                    CanEditWorkhours   = bool.Parse(claims.FirstOrDefault(v => v.ClaimType == "CANEDITWORKHOURS").ClaimValue),
                }
            };

            return user;
        }

        public async Task<PagingResult<UserDTO>> GetAllUsers(FilterDto filter, CancellationToken cancellationToken)
        {
            var result = new PagingResult<UserDTO>();

            result.TotalRecords = await context.Users.CountAsync(cancellationToken);

            var query = context.Users.AsNoTracking();

            if (filter.Sorting != null)
            {
                /* foreach (SortingDto sort in filter.Sorting)
                 {
                     if (sort.SortOrder != SortOrder.None)
                     {
                         switch (sort.ColumnId)
                         {
                             case "eventDate":
                                 {
                                     query = (sort.SortOrder == SortOrder.Ascending) ?
                                         query.OrderBy(v => v.EventDate) :
                                         query.OrderByDescending(v => v.EventDate);
                                     break;
                                 }
                             default: continue;
                         }
                     }
                 }*/
            }
            else
            {
                query = query.OrderBy(v => v.Id);
            }

            result.Records = await query
            .Skip(filter.Paging.Skip)
            .Take(filter.Paging.Take)
            .Select(e => new UserDTO()
            {
                Username  = e.UserName,
                FirstName = e.FirstName,
                LastName  = e.LastName,
                Email     = e.Email,
                UserId    = e.Id,
                Phone     = e.PhoneNumber,
            }).ToListAsync(cancellationToken);

            return result;
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
                UserId      = u.Id,
                Username    = u.UserName,
                Email       = u.Email,
                FirstName   = u.FirstName,
                LastName    = u.LastName,
            }).FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
        }

        public async Task<UserDto> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                UserId     = u.Id,
                Username  = u.UserName,
                Email     = u.Email,
                FirstName = u.FirstName,
                LastName  = u.LastName,
            }).FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<UserDto> FindByFirstNameAsync(string firstName, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                UserId     = u.Id,
                Username   = u.UserName,
                Email      = u.Email,
                FirstName  = u.FirstName,
                LastName   = u.LastName,
            }).FirstOrDefaultAsync(u => u.FirstName == firstName, cancellationToken);
        }

        public async Task<UserDto> FindByLastNameAsync(string lastName, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                UserId    = u.Id,
                Username  = u.UserName,
                Email     = u.Email,
                FirstName = u.FirstName,
                LastName  = u.LastName,
            }).FirstOrDefaultAsync(u => u.LastName == lastName, cancellationToken);
        }

        public async Task<UserDto> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await context.Users.Select(u => new UserDto()
            {
                UserId    = u.Id,
                Username  = u.UserName,
                Email     = u.Email,
                FirstName = u.FirstName,
                LastName  = u.LastName,
            }).FirstOrDefaultAsync(u => (u.FirstName + " " + u.LastName) == name, cancellationToken);
        }
    }
}
