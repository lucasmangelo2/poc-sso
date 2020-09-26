using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;

namespace IdentityServer.SSO.Data.Repository
{
    public class UserRepository : BaseRepository<User, IMainDbContext>, IUserRepository
    {
        public UserRepository(IMainDbContext context) : base(context)
        {
        }
    }
}
