using IdentityServer.SSO.Request;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public LoginController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            TestUserStore users = null)
        {
            _users = users ?? new TestUserStore(Config.GetUsers());

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(LoginAuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthorizationRequest context = await _interaction.GetAuthorizationContextAsync(request.ReturnUrl);

            if (_users.ValidateCredentials(request.Username, request.Password))
            {
                TestUser user = _users.FindByUsername(request.Username);
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username));

                AuthenticationProperties props = null;

                // issue authentication cookie with subject ID and username
                var isuser = new IdentityServerUser(user.SubjectId)
                {
                    DisplayName = user.Username
                };

                await HttpContext.SignInAsync(isuser, props);

                if (context != null)
                {
                    return Redirect(request.ReturnUrl);
                }

                return Ok();
            }
            else
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(request.Username, "Credenciais inválidas"));
                throw new Exception("Credenciais inválidas");
            }
        }

        private static string GetAccessToken(HttpContext httpContext)
        {
            var accessToken = httpContext.Request.Headers["Authorization"];
            string token = string.Empty;
            
            if (accessToken.Count > 0)
                token = accessToken.First().Remove(0, "Bearer ".Length);
            return token;
        }
    }
}