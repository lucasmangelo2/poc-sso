using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.SSO.Data.Repository
{
    public class WebhookEventRepository : BaseRepository<WebhookEvent, IWebhookDbContext>, IWebhookEventRepository
    {
        public WebhookEventRepository(IWebhookDbContext context) : base(context)
        {
        }
    }
}
