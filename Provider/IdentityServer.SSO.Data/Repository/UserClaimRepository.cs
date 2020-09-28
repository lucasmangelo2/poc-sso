using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;

namespace IdentityServer.SSO.Data.Repository
{
    public class UserClaimRepository : BaseRepository<UserClaim, IMainDbContext>, IUserClaimRepository
    {
        public UserClaimRepository(IMainDbContext context) : base(context)
        {
        }
    }
}
