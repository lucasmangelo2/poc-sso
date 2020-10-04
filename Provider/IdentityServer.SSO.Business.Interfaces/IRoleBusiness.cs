using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IRoleBusiness
    {
        Task<string> GetRoleByUserAsync(IdentityUser user);

        Task InsertAsync(IdentityUser user, string role);

        Task UpdateAsync(IdentityUser user, string role);
    }
}
