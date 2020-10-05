using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.SSO.Data.Interfaces.Context
{
    public interface IWebhookContext : IDbContext
    {
        DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
        DbSet<WebhookSenderArgs> WebhookSenderArgss { get; set; }
        DbSet<WebhookEvent> WebhookEvents { get; set; }
    }
}
