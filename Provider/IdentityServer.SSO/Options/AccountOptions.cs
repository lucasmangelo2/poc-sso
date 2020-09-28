
using System;

namespace IdentityServer.SSO.Options
{
    public class AccountOptions
    {
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool AutomaticRedirectAfterSignOut = false;

        public static string InvalidCredentialsErrorMessage = "login ou senha inválidos";
    }
}
