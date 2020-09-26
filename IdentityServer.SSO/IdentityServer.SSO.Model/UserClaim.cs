using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.SSO.Model
{
    [Table("tb_permissao_usuario")]
    public class UserClaim : BaseModel
    {
        [Column("id_usuario"), ForeignKey("User"), Required]
        public int UserId { get; set; }

        [Column("id_permissao"), ForeignKey("Claim"), Required]
        public int ClaimId { get; set; }
    }
}
