using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IUserBusiness : IBusiness<IdentityUser>
    {
        Task<IdentityUser> InsertAsync(string username, string name, string email, string password);

        Task DeleteAsync(string id);

        Task<List<Claim>> GetClaimsAsync(IdentityUser user);

        Task<IdentityUser> GetUserByIdAsync(string id);

        Task<IdentityUser> GetUserByUserNameAsync(string userName);

        Task<IdentityUser> GetUserByEmailAsync(string email);
    }
}
