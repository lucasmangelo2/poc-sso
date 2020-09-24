using IdentityServer.SSO.Infra.Atributtes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.SSO.Controllers
{
    [Authorize]
    [SecurityHeaders]
    public class BaseCRUDController : Controller
    {

    }
}
