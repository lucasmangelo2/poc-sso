using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;

namespace IdentityServer.SSO.Business
{
    public class ClaimBusiness : BaseBusiness<IClaimRepository, Claim>, IClaimBusiness
    {
        public ClaimBusiness(IClaimRepository repository) : base(repository)
        {
        }
    }
}
