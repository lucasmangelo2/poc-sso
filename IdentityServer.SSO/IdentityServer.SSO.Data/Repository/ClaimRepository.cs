using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;

namespace IdentityServer.SSO.Data.Repository
{
    public class ClaimRepository : BaseRepository<Claim, IMainDbContext>, IClaimRepository
    {
        public ClaimRepository(IMainDbContext context) : base(context)
        {
        }
    }
}
