using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM_DAL.Entity;

namespace PM_DAL
{
    public class PMDBContext : IdentityDbContext<User, Role, Int64, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public PMDBContext(DbContextOptions<PMDBContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("User");

                b.Ignore(c => c.AccessFailedCount)
                 .Ignore(c => c.LockoutEnabled)
                 .Ignore(c => c.LockoutEnd)
                 .Ignore(c => c.TwoFactorEnabled);

                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.ToTable("UserRole");
            });

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.ToTable("UserClaim");
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.ToTable("UserToken");
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.ToTable("UserLogin");
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.ToTable("Role");

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<RoleClaim>(b =>
            {
                b.ToTable("RoleClaim");
            });

            SeedUsers(modelBuilder);

            SeedRoles(modelBuilder);

            SeedRoleClaims(modelBuilder);

            SeedUserRoles(modelBuilder);
        }

        public void SeedUsers(ModelBuilder builder)
        {
            User[] users = new User[]
            {
                new()
                {
                    Id        = 1,
                    FirstName = "Aleksandar",
                    LastName  = "Gospodinov",
                    UserName  = "test123",
                    Email     = "test123@gmail.com",
                    EmailConfirmed = true
                }
            };

            //TO-DO: 
            var hasher = new PasswordHasher<User>();

            users[0].PasswordHash = hasher.HashPassword(users[0], "test97");

            builder.Entity<User>().HasData(users);
        }

        public void SeedRoles(ModelBuilder builder)
        {
            Role[] roles = new Role[]
            {
                new()
                {
                    Id               = 1,
                    Name             = "Owner",
                    NormalizedName   = "owner",
                    ConcurrencyStamp = "1",
                },
                new()
                {
                    Id               = 2,
                    Name             = "Admin",
                    NormalizedName   = "admin",
                    ConcurrencyStamp = "1",
                }
            };
           
            builder.Entity<Role>().HasData(roles);
        }

        public void SeedRoleClaims(ModelBuilder builder)
        {
            RoleClaim[] roleClaims = new RoleClaim[]
            {
                new()
                {
                    Id     = 1,
                    RoleId = 1,
                    ClaimType = "parking-lot",
                    ClaimValue = "add",
                }
            };

            builder.Entity<RoleClaim>().HasData(roleClaims);
        }

        public void SeedUserRoles(ModelBuilder builder)
        {
            UserRole[] userRoles = new UserRole[]
            {
                new(){ UserId = 1, RoleId = 1 }
            };

            builder.Entity<UserRole>().HasData(userRoles);
        }
    }
}