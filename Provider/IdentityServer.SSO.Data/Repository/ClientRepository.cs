using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServer.SSO.Data.Repository
{
    public class ClientRepository : BaseRepository<Client, IIdentityConfigurationDbContext>, IClientRepository
    {
        public ClientRepository(IIdentityConfigurationDbContext context) : base(context)
        {
        }
    }
}
