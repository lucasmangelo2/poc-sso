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
        /// Subscribed webhook definitions unique names. <see cref="Definition.Name"/>
        /// </summary>
        public List<WebhookSubscriptionRelation> WebhooksRelated { get; set; } = new List<WebhookSubscriptionRelation>();
    }
}
