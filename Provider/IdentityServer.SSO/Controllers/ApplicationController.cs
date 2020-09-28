using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.SSO.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}