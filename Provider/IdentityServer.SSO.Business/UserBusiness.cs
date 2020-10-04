using IdentityModel;
using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Business.Interfaces.Utils;
using IdentityServer.SSO.Data.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business
{
    public class UserBusiness : BaseBusiness<IUserRepository, IdentityUser>, IUserBusiness
    {
        private readonly IRoleBusiness _roleBusiness;
        private readonly UserManager<IdentityUser> _userManager;

        public UserBusiness(
            IRoleBusiness roleBusiness,
            IUserRepository repository,
            UserManager<IdentityUser> userManager) : base(repository)
        {
            _roleBusiness = roleBusiness;
            _userManager = userManager;
        }

        public async Task<IdentityUser> InsertAsync(string username, string name, string email, string password, string role)
        {
            await ValidBeforeInsertAsync(username, email);

            var user = new IdentityUser(username);
            user.Email = email;

            var result = await _userManager.CreateAsync(user);

            ValidIdentityResult(result);

            await SaveClaimAsync(user, JwtClaimTypes.Name, name);
            await SaveClaimAsync(user, JwtClaimTypes.Email, user.Email);

            await _roleBusiness.InsertAsync(user, role);
            await _userManager.AddPasswordAsync(user, password);

            return user;
        }

        public async Task<IdentityUser> UpdateAsync(IdentityUser model, string name, string role)
        {
            var result = await _userManager.UpdateAsync(model);

            ValidIdentityResult(result);

            await SaveClaimAsync(model, JwtClaimTypes.Name, name);
            await SaveClaimAsync(model, JwtClaimTypes.Email, model.Email);

            await _roleBusiness.UpdateAsync(model, role);

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

        #region Claims Management

        private async Task SaveClaimAsync(IdentityUser model, string type, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var claims = await _userManager.GetClaimsAsync(model);
                Claim oldClaim = claims.FirstOrDefault(x => x.Type == type);
                var newClaim = new Claim(type, value);

                if (oldClaim != null)
                {
                    if (newClaim.Value != oldClaim.Value)
                    {
                        var result = await _userManager.ReplaceClaimAsync(model, oldClaim, newClaim);
                    }
                }
                else
                {
                    await _userManager.AddClaimAsync(model, newClaim);
                }
            }
        }

        #endregion

        #region Validation

        private async Task ValidBeforeInsertAsync(string username, string email)
        {
            bool loginExists = await GetUserByUserNameAsync(username) != null, emailExists = await GetUserByEmailAsync(email) != null;

            if (loginExists)
            {
                throw new BusinessValidationException("Login existente, por favor informe um novo login");
            }

            if (emailExists)
            {
                throw new BusinessValidationException("Email existente, por favor informe um novo e-mail");
            }
        }

        private void ValidIdentityResult(IdentityResult result)
        {
            if (result != null && !result.Succeeded && result.Errors.Count() > 0)
            {
                throw new BusinessValidationException($"Erro ao inserir um novo usuário: {result.Errors.First().Description}");
            }
        }

        #endregion

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
