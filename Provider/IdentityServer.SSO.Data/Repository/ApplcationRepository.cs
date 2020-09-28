using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;

namespace IdentityServer.SSO.Data.Repository
{
    public class ApplcationRepository : BaseRepository<Application, IMainDbContext>, IApplicationRepository
    {
        public ApplcationRepository(IMainDbContext context) : base(context)
        {
        }
    }
}
