using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;

namespace IdentityServer.SSO.Data.Repository
{
    public class ApplicationClaimRepository : BaseRepository<ApplicationClaim, IMainDbContext>, IApplicationClaimRepository
    {
        public ApplicationClaimRepository(IMainDbContext context) : base(context)
        {
        }
    }
}
