using IdentityServer.SSO.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business
{
    public class RoleBusiness : IRoleBusiness
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RoleBusiness(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetRoleByUserAsync(IdentityUser user)
        {
            return (await _userManager.GetRolesAsync(user))?.FirstOrDefault();
        }

        public async Task InsertAsync(IdentityUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task UpdateAsync(IdentityUser user, string role)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (roles != null && roles.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(user, roles);
            }

            await InsertAsync(user, role);
        }
    }
}
