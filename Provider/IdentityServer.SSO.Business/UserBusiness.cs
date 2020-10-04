using IdentityModel;
using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business
{
    public class UserBusiness : BaseBusiness<IUserRepository, IdentityUser>, IUserBusiness
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserBusiness(IUserRepository repository,
            UserManager<IdentityUser> userManager) : base(repository)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser> InsertAsync(string username, string name, string email, string password)
        {
            var user = new IdentityUser(username);
            user.Email = email;

            await _userManager.CreateAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, name),
                new Claim(JwtClaimTypes.Email, user.Email)
            };

            await _userManager.AddClaimsAsync(user, claims);

            await _userManager.AddPasswordAsync(user, password);

            return user;
        }

        public override async Task<IdentityUser> UpdateAsync(IdentityUser model)
        {
            var result = await _userManager.UpdateAsync(model);

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            var model = await GetUserByIdAsync(id);

            await DeleteAsync(model);
        }

        public override async Task DeleteAsync(IdentityUser model)
        {
            var result = await _userManager.DeleteAsync(model);
        }

        #region Get Methods

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            return await _repository.GetUserByEmailAsync(email);
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        public async Task<IdentityUser> GetUserByUserNameAsync(string userName)
        {
            return await _repository.GetUserByUserNameAsync(userName);
        }

        public async Task<List<Claim>> GetClaimsAsync(IdentityUser user)
        {
            return (await _userManager.GetClaimsAsync(user)).ToList();
        }

        #endregion
    }
}
