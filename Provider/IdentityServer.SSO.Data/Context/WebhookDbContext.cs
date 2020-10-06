using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.SSO.Data.Context
{
    public class WebhookDbContext : DbContext, IWebhookDbContext
    {
        public WebhookDbContext(DbContextOptions<WebhookDbContext> options) : base(options)
        {
        }

        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }

        public DbSet<WebhookDefinition> WebhookDefinitions { get; set; }

        public DbSet<WebhookEvent> WebhookEvents { get; set; }
    }
}
