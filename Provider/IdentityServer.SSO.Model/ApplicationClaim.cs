using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.SSO.Model
{
    public class ApplicationClaim : BaseModel
    {
        [Column("id_aplicacao"), ForeignKey("Application"), Required]
        public int ApplicationId { get; set; }

        [Column("id_permissao"), ForeignKey("Claim"), Required]
        public int ClaimId { get; set; }
    }
}
