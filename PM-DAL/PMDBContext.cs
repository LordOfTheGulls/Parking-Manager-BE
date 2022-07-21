using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM_DAL.Entity;

namespace PM_DAL
{
    public class PMDBContext : IdentityDbContext<User, Role, Int64, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<ParkingLot> ParkingLot { get; set; }
        public DbSet<ParkingEvent> ParkingEvent { get; set; }
        public DbSet<ParkingEventLog> ParkingEventLog { get; set; }
        public DbSet<ParkingInOutLog> ParkingInOutLog { get; set; }
        public DbSet<ParkingLotBlacklist> ParkingLotBlacklist { get; set; }
        public DbSet<ParkingLotType> ParkingLotType { get; set; }
        public DbSet<ParkingPayment> ParkingPayment { get; set; }
        public DbSet<ParkingPricing> ParkingPricing { get; set; }
        public DbSet<ParkingPricingPlan> ParkingPricingPlan { get; set; }
        public DbSet<ParkingPaymentMethod> ParkingPaymentMethod { get; set; }
        public DbSet<ParkingWorkhours> ParkingWorkhours { get; set; }
        public DbSet<ParkingWorkhoursPlan> ParkingWorkhoursPlans { get; set; }

        public PMDBContext(DbContextOptions<PMDBContext> options): base(options)
        {
            //((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += (sender, e) => DateTimeKindAttribute.Apply(e.Entity);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("user");

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
                b.ToTable("user_role");
            });

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.ToTable("user_claim");
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.ToTable("user_token");
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.ToTable("user_login");
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.ToTable("role");

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
                b.ToTable("role_claim");
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