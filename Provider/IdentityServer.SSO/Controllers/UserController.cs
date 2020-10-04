using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Business.Interfaces.Utils;
using IdentityServer.SSO.Infra.Atributtes;
using IdentityServer.SSO.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.SSO.Controllers
{
    [Authorize(Roles = "Admin")]
    [SecurityHeaders]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _business;
        private readonly IRoleBusiness _roleBusiness;

        public UserController(IUserBusiness business,
            IRoleBusiness roleBusiness)
        {
            _business = business;
            _roleBusiness = roleBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _business.GetAllAsync();
            BaseListViewModel<UserViewModel> vm = await GetListViewModelAsync(users);

            return View(vm);
        }


        [HttpGet]
        [Route("insert")]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _business.InsertAsync(model.Username, model.Name, model.Email, model.Password, model.Role);
            }
            catch (BusinessValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _business.GetUserByIdAsync(id);

            if (user != null)
            {
                var viewModel = await GetUserViewModelAsync(user);

                return View(viewModel);
            }

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(string id, UserViewModel model)
        {
            var user = await _business.GetUserByIdAsync(id);

            if (user != null)
            {
                user.UserName = model.Username;
                user.Email = model.Email;

                try
                {
                    await _business.UpdateAsync(user, model.Name, model.Role);
                }
                catch (BusinessValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }
            }

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _business.DeleteAsync(id);

            return RedirectToAction("Index", "User");
        }

        private async Task<UserViewModel> GetUserViewModelAsync(IdentityUser user)
        {
            List<Claim> claims = await _business.GetClaimsAsync(user);
            string role = await _roleBusiness.GetRoleByUserAsync(user);

            return new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Name = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value,
                Username = user.UserName,
                Role = role
            };
        }

        private async Task<BaseListViewModel<UserViewModel>> GetListViewModelAsync(List<IdentityUser> users)
        {
            var list = new List<UserViewModel>();

            foreach (var user in users)
            {
                list.Add(await GetUserViewModelAsync(user));
            }

            BaseListViewModel<UserViewModel> vm = new BaseListViewModel<UserViewModel>()
            {
                Data = list
            };
            return vm;
        }
    }
}