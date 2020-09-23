using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
