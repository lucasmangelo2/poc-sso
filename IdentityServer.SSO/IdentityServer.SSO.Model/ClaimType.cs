using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.SSO.Model
{
    [Table("tb_tipopermissao")]
    public class ClaimType : BaseModel
    {
        [Column("nome"), MaxLength(50), Required]
        public string Name { get; set; }
    }
}
