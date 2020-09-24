using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Models
{
    public class UserAccountModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
