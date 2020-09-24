using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.SSO.Models
{
    [Table("tb_permissao_usuario")]
    public class UserClaim : BaseModel
    {
        [Column("id_usuario"), ForeignKey("User"), Required]
        public int UserId { get; set; }

        [Column("id_permissaoaplicacao"), ForeignKey("ApplicationClaim"), Required]
        public int ApplicationClaimId { get; set; }
    }
}
