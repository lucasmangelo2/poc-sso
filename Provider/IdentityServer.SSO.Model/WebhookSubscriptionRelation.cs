using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Model
{
    public class WebhookSubscriptionRelation : BaseModel
    {
        [Required]
        public long WebhookDefinitionId { get; set; }

        [Required]
        public long WebhookSubscriptionId { get; set; }
    }
}
