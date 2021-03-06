﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.Model
{
    public class WebhookEvent : BaseModel
    {
        public string Data { get; set; }

        public long WebhookDefinitionId { get; set; }
    }
}
