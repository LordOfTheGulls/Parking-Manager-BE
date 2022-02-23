
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace PM_AUTH
{
    public static class Config
    {
        public static List<TestUser> GetUsers() => new List<TestUser>
        {
           new TestUser()
           {
               Username = "alex",
               Password = "password"
           }
        };

        public static IEnumerable<IdentityResource> GetResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> GetApis() => new List<ApiScope>()
        {
            new ApiScope("pmapi", "Parking Manager - API"),
        };

        public static IEnumerable<Client> GetClients() => new List<Client>()
        {
            new Client()
            {
                ClientId            = "pmspa",
                ClientName          = "Parking Manager - Web Client",
                ClientSecrets       = { new Secret("secret".ToSha256()) },
                AllowedGrantTypes   = GrantTypes.Code,
                RequirePkce         = true,
                RequireConsent      = false,
                RequireClientSecret = false,
                AccessTokenType     = AccessTokenType.Jwt,
                AccessTokenLifetime = 600,
                AllowedScopes       = { 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "pmapi"
                },
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { "http://localhost:4200/auth-callback" },
            }
        };
    }
}
