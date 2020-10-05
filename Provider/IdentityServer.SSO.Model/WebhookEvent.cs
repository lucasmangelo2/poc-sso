using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Model
{
    public class WebhookEvent : BaseModel
    {
        /// <summary>
        /// Webhook unique name
        /// </summary>
        [Required]
        public string WebhookName { get; set; }

        /// <summary>
        /// Webhook data as JSON string.
        /// </summary>
        public string Data { get; set; }
    }
}
