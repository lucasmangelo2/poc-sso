using IdentityServer.SSO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Interfaces.Repository
{
    public interface IWebhookSubscriptionRepository : IRepository<WebhookSubscription>
    {
        Task<List<WebhookSubscription>> GetByWebhookNameAsync(long webhookId);
    }
}
