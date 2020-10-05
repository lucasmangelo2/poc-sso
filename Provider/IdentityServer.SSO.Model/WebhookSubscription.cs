using System;
using System.Collections.Generic;

namespace IdentityServer.SSO.Model
{
    public class WebhookSubscription : BaseModel
    {
        /// <summary>
        /// Subscription webhook endpoint
        /// </summary>
        public string WebhookUri { get; set; }

        /// <summary>
        /// Webhook secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Is subscription active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Subscribed webhook definitions unique names. <see cref="WebhookDefinition.Name"/>
        /// </summary>
        public List<string> Webhooks { get; set; } = new List<string>();

        /// <summary>
        /// Gets a set of additional HTTP headers. That headers will be sent with the webhook.
        /// </summary>
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}
