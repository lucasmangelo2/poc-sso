using IdentityServer.SSO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IWebhookSubscriptionBusiness : IBusiness<WebhookSubscription>
    {
        Task<List<WebhookSubscription>> GetByWebhookIdAsync(long id);
    }
}
