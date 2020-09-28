using IdentityServer.SSO.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.SSO.Model
{
    [Table("tb_permissao")]
    public class Claim : BaseModel
    {
        [Column("nome"), MaxLength(50), Required]
        public string Name { get; set; }

        [Column("tipo"), Required]
        public ClaimType Type { get; set; }
    }
}
