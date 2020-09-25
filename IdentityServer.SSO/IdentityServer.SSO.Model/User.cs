using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.SSO.Model
{
    [Table("tb_usuario")]
    public class User : BaseModel
    {
        [Column("login"), MaxLength(50), Required]
        public string Username { get; set; }

        [Column("senha"), MaxLength(30), Required]
        public string Password { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        #region Navigations

        public List<UserClaim> UserClaims { get; set; }

        #endregion
    }
}
