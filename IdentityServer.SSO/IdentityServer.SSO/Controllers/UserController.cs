using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.SSO.Options;
using IdentityServer.SSO.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.SSO.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            BaseListViewModel<UserViewModel> vm = new BaseListViewModel<UserViewModel>()
            {
                Data = new List<IdentityUser>()
                .Select(x => new UserViewModel()
                {
                    //Email = x.Claims.First(x => x.Type == "email")?.Value,
                    Password = x.PasswordHash,
                    Username = x.UserName
                })
                .ToList()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(UserViewModel model)
        {
            return Index();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View();
        }

        [HttpPut]
        public IActionResult Update(UserViewModel model)
        {
            return Index();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Index();
        }
    }
}