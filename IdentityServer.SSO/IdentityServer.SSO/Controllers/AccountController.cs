using IdentityServer.SSO.Infra.Atributtes;
using IdentityServer.SSO.ViewModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityServer.SSO.Options;

namespace IdentityServer.SSO.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(
            IIdentityServerInteractionService interaction,
            SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _interaction = interaction;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string button)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (button != "login")
                return await RedirectOnCancel(model, context);

            if (ModelState.IsValid)
            {
                IdentityUser user = await _signInManager.UserManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberLogin, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToHome();
                    }
                }

                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            LoginViewModel vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        private async Task<IActionResult> RedirectOnCancel(LoginViewModel model, AuthorizationRequest context)
        {
            if (context != null)
            {
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                return Redirect(model.ReturnUrl);
            }
            
            return RedirectToHome();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();
                await _signInManager.SignOutAsync();
            }

            var logoutRequest = await _interaction.GetLogoutContextAsync(logoutId);

            if (logoutRequest == null || (logoutRequest != null && string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri)))
            {
                return RedirectToHome();
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        private IActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Utils

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Username = context?.LoginHint
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginViewModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        #endregion
    }
}
