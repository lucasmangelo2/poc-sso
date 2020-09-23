using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Request
{
    public class LoginAuthenticationRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
