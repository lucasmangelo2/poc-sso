using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.SSO.Data.Context
{
    public class IdentityConfigurationDbContext : ConfigurationDbContext, IIdentityConfigurationDbContext
    {
        public IdentityConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
    }
}
