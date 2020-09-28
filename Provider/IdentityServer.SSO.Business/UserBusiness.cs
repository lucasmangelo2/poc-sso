using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;

namespace IdentityServer.SSO.Business
{
    public class UserBusiness : BaseBusiness<IUserRepository, User>, IUserBusiness
    {
        public UserBusiness(IUserRepository repository) : base(repository)
        {
        }
    }
}
