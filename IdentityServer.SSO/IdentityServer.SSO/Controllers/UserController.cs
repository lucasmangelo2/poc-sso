using System.Threading.Tasks;
using IdentityServer.SSO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.SSO.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(UserAccountModel model)
        {
            return View("Indext");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View();
        }

        [HttpPut]
        public IActionResult Update(UserAccountModel model)
        {
            return View("Indext");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return View("Indext");
        }
    }
}