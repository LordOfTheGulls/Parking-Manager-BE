using Microsoft.AspNetCore.Authorization;

namespace PM_AUTH.AuthorizationRequirements
{
    public class CustomeRequireClaim: IAuthorizationRequirement
    {
        public CustomeRequireClaim(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; }
    }

    public class CustomRequireClaimHandler: AuthorizationHandler<CustomeRequireClaim>
    {
        public CustomRequireClaimHandler(){}

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomeRequireClaim requirement)
        {
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);

            if (hasClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder,
            string claimType
        )
        {
            builder.AddRequirements(new CustomeRequireClaim(claimType));

            return builder;
        }
    }
}
