using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Model
{
    public class BaseModel
    {
        /// <summary>
        /// Unique Identifier
        /// </summary>
        [Key]
        public long Id { get; set; }
    }
}
