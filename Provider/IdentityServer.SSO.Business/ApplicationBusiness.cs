using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;

namespace IdentityServer.SSO.Business
{
    public class ApplicationBusiness : BaseBusiness<IApplicationRepository, Application>, IApplicationBusiness
    {
        public ApplicationBusiness(IApplicationRepository repository) : base(repository)
        {
        }
    }
}
