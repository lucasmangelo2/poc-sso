using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityServer.SSO
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

        public static List<TestUser> GetUsers()
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };

            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "lucas",
                    Password = "12345",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "Lucas"),
                        new Claim(JwtClaimTypes.GivenName, "Lucas Mangelo"),
                        new Claim(JwtClaimTypes.FamilyName, "Lucas"),
                        new Claim(JwtClaimTypes.Email, "lucas@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("createuser", "true")
                    }
                }
            };
        }
    }
}