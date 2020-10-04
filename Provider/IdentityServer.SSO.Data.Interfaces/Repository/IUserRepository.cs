using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Interfaces.Repository
{
    public interface IUserRepository : IRepository<IdentityUser>
    {
        Task<IdentityUser> GetUserByIdAsync(string id);

        Task<IdentityUser> GetUserByUserNameAsync(string userName);

        Task<IdentityUser> GetUserByEmailAsync(string email);
    }
}
