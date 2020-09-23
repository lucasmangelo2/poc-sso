using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.SSO
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "client_application",
                    ClientName = "Client Application With Angular",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = { "http://localhost:4200"  },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    ClientSecrets = {new Secret("super-secret".ToSha256(),"client-secret") },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "website"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "lucas",
                    Password = "123",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "Lucas"),
                        new Claim(JwtClaimTypes.GivenName, "Lucas Mangelo"),
                        new Claim(JwtClaimTypes.FamilyName, "Mangelo"),
                        new Claim(JwtClaimTypes.Email, "lucas.mangelo@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                    }
                }
            };
        }
    }
}
