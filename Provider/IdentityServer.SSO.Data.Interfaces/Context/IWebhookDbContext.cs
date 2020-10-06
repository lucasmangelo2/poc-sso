using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.SSO.Data.Interfaces.Context
{
    public interface IWebhookDbContext : IDbContext
    {
        DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }

        DbSet<WebhookDefinition> WebhookDefinitions { get; set; }
        
        DbSet<WebhookEvent> WebhookEvents { get; set; }
    }
}
