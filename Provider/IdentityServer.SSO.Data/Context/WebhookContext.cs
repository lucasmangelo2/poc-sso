using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.SSO.Data.Context
{
    public class WebhookContext : DbContext, IWebhookContext
    {
        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
        public DbSet<WebhookSenderArgs> WebhookSenderArgss { get; set; }
        public DbSet<WebhookEvent> WebhookEvents { get; set; }
    }
}
