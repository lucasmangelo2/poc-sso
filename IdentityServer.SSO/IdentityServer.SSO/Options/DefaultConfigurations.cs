using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.SSO.Options
{
    public class DefaultConfigurations
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "API para troca de informações", new []{ 
                    "fullacess",
                    "readonly"
                })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client_application",
                    ClientName = "Angular Client Application",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = { "http://localhost:4200"  },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    ClientSecrets = {new Secret("super-secret".ToSha256(),"mvc-secret") },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "api"
                    }
                }
            };
        }

        public static List<Claim> GetPrimaryClaims()
        {
            return new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, "Administrador"),
                new Claim(JwtClaimTypes.GivenName, "Administrador"),
                new Claim(JwtClaimTypes.Email, "admin-sso@gmail.com"),
                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                new Claim("createuser", "true")
            };
        }
    }
}