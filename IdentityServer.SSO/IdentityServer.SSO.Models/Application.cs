using IdentityServer.SSO.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.SSO.Models
{
    [Table("tb_aplicacao")]
    public class Application : BaseModel
    {
        [Column("nome"), MaxLength(50), Required]
        public string Name { get; set; }

        [Column("descricao"), MaxLength(150)]
        public string Description { get; set; }

        [Column("tipoaplicacao"), Required]
        public ApplicationType Type { get; set; }

        #region Navigations

        public List<ApplicationClaim> ApplicationClaims { get; set; }

        #endregion
    }
}
