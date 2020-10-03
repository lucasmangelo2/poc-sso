using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using IdentityServer.SSO.Infra.Atributtes;
using IdentityServer.SSO.Infra.Data;
using IdentityServer.SSO.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.SSO.Controllers
{
    [Authorize]
    [SecurityHeaders]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public UserController(
            IMapper mapper,
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _dbContext.Users.ToList();
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
            bool loginExists = GetUserByUserName(model.Username) != null,
                emailExists = GetUserByEmail(model.Email) != null;

            if (!ModelState.IsValid || loginExists || emailExists)
            {
                if (loginExists)
                {
                    ModelState.AddModelError("Username", "Login existente, por favor informe um novo login");
                }

                if (emailExists)
                {
                    ModelState.AddModelError("Email", "Email existente, por favor informe um novo e-mail");
                }

                return View(model);
            }

            var user = new IdentityUser(model.Username);
            user.Email = model.Email;

            await _userManager.CreateAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, model.Name),
                new Claim(JwtClaimTypes.Email, user.Email)
            };

            await _userManager.AddClaimsAsync(user, claims);

            await _userManager.AddPasswordAsync(user, model.Password);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(string id)
        {
            var user = GetUserById(id);

            if (user != null)
            {
                var viewModel = await GetUserViewModelAsync(user);

                return View(viewModel);
            }

            return RedirectToAction("Index", "User");
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(UserViewModel model)
        {
            var user = GetUserById(model.Id);

            if (user != null)
            {
                user.UserName = model.Username;
                user.Email = model.Email;

                await _userManager.UpdateAsync(user);
            }


            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = GetUserById(id);
            
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index", "User");
        }

        private async Task<UserViewModel> GetUserViewModelAsync(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            return new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Name = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value,
                Username = user.UserName
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

        private IdentityUser GetUserById(string id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        private IdentityUser GetUserByUserName(string userName)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName == userName);
        }

        private IdentityUser GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}