using IdentityServer4.EntityFramework.Interfaces;

namespace IdentityServer.SSO.Data.Interfaces.Context
{
    public interface IIdentityConfigurationDbContext : IConfigurationDbContext, IDbContext
    {
    }
}
