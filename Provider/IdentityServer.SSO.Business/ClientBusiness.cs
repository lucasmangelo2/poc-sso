using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServer.SSO.Business
{
    public class ClientBusiness : BaseBusiness<IClientRepository, Client>, IClientBusiness
    {
        public ClientBusiness(IClientRepository repository) : base(repository)
        {
        }
    }
}
