using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembra credenciais")]
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}