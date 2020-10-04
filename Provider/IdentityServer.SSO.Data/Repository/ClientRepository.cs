using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServer.SSO.Data.Repository
{
    public class ClientRepository : BaseRepository<Client, ConfigurationDbContext>, IClientRepository
    {
        public ClientRepository(ConfigurationDbContext context) : base(context)
        {
        }
    }
}
