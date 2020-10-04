using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required, Display(Name = "Login")]
        public string Username { get; set; }

        [Required, Display(Name = "Senha")]
        public string Password { get; set; }

        [Required, Display(Name = "Confirmar Senha"), Compare("Password", ErrorMessage = "Por favor, verifique a senha de confirmação")]
        public string ConfirmPassword { get; set; }

        [Required, Display(Name = "Nome")]
        public string Name { get; set; }

        [Required, Display(Name = "E-mail")]
        public string Email { get; set; }

        public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();
    }
}
