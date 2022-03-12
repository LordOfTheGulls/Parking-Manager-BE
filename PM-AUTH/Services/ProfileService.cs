using IdentityServer4.Models;
using IdentityServer4.Services;

namespace PM_AUTH.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using IdentityServer4.Extensions;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using PM_DAL.Interface;

    namespace CustomIdentityServer4.UserServices
    {
        public class CustomProfileService : IProfileService
        {
            protected readonly ILogger Logger;

            protected readonly IUserRepository _userRepository;

            public CustomProfileService(IUserRepository userRepository, ILogger<CustomProfileService> logger)
            {
                _userRepository = userRepository ?? throw new ArgumentNullException();
                Logger          = logger         ?? throw new ArgumentNullException();
            }

            public async Task GetProfileDataAsync(ProfileDataRequestContext context)
            {
                var sub = context.Subject.GetSubjectId();

                Logger.LogDebug("Get profile called for subject {subject} from client {client} with claim types {claimTypes} via {caller}",
                    context.Subject.GetSubjectId(),
                    context.Client.ClientName ?? context.Client.ClientId,
                    context.RequestedClaimTypes,
                    context.Caller);

                var user = _userRepository.FindById(Int64.Parse(context.Subject.GetSubjectId()));

             /*   var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
                }*/

                var claims = new List<Claim>
                {
                    new Claim("role", "dataEventRecords.admin"),
                    new Claim("role", "dataEventRecords.user"),
                    /*new Claim("username", user.UserName),
                    new Claim("email",    user.Email)*/
                };

                context.IssuedClaims = claims;
            }

            public async Task IsActiveAsync(IsActiveContext context)
            {
                var sub = context.Subject.GetSubjectId();

                var user = _userRepository.FindById(Int64.Parse(context.Subject.GetSubjectId()));

                context.IsActive = (user != null);
            }
        }
    }
}
