using IdentityServer.SSO.Data.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Repository
{
    public class UserRepository : BaseRepository<IdentityUser, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IdentityUser> GetUserByUserNameAsync(string userName)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
