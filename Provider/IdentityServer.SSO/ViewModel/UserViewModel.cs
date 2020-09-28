using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
