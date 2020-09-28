using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Model
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
