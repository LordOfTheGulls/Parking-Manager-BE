using PM_AUTH.Services;
using PM_AUTH.Services.CustomIdentityServer4.UserServices;
using PM_DAL.Interface;
using PM_DAL.Repository;

namespace PM_AUTH.Extensions
{
    public static class IdentityServerExtensions
    {
        public static IIdentityServerBuilder AddUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.AddProfileService<CustomProfileService>();

            builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
