using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Repository
{
    public class WebhookSubscriptionRepository : BaseRepository<WebhookSubscription, IWebhookContext>, IWebhookSubscriptionRepository
    {
        public WebhookSubscriptionRepository(IWebhookContext context) : base(context)
        {
        }

        public async Task<List<WebhookSubscription>> GetByWebhookNameAsync(string webhookName)
        {
            return Context.Set<WebhookSubscription>().Where(x => x.Webhooks.Contains(webhookName)).AsNoTracking().ToList();
        }
    }
}
