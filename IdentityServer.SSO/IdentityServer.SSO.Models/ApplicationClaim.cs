using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.SSO.Models
{
    [Table("tb_permissao_aplicacao")]
    public class ApplicationClaim : BaseModel
    {
        [Column("nome"), MaxLength(50), Required]
        public string Name { get; set; }
    }
}
