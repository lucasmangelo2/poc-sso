using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Model
{
    public class WebhookDefinition : BaseModel
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
