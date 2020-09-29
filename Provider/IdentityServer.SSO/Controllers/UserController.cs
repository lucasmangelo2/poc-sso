using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using IdentityServer.SSO.Infra.Data;
using IdentityServer.SSO.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.SSO.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Index()
        {
            var users = _dbContext.Users.ToList();

            var list = new List<UserViewModel>();

            foreach (var user in users)
            {
                var claims = await _userManager.GetClaimsAsync(user);

                list.Add(new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value,
                    Username = user.UserName
                });
            }

            BaseListViewModel<UserViewModel> vm = new BaseListViewModel<UserViewModel>()
            {
                Data = list
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
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
        public async Task<IActionResult> Update(int id)
        {
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserViewModel model)
        {
            return RedirectToAction("Index", "User");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return RedirectToAction("Index", "User");
        }
    }
}