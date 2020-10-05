using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.SSO.Model
{
    public class WebhookPayload : BaseModel
    {
        public string Event { get; set; }

        public dynamic Data { get; set; }

        public DateTime CreationTimeUtc { get; set; }
    }
}
