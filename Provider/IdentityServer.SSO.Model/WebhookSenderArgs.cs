using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.SSO.Model
{
    public class WebhookSenderArgs
    {
        public long WebhookEventId { get; set; }

        public string Data { get; set; }

        public string WebhookName { get; set; }

        public long WebhookSubscriptionId { get; set; }

        public string Secret { get; set; }

        public string WebhookUri { get; set; }
    }
}
