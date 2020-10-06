using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Repository
{
    public class WebhookSubscriptionRepository : BaseRepository<WebhookSubscription, IWebhookDbContext>, IWebhookSubscriptionRepository
    {
        public WebhookSubscriptionRepository(IWebhookDbContext context) : base(context)
        {
        }

        public async Task<List<WebhookSubscription>> GetByWebhookNameAsync(long webhookId)
        {
            return Context.Set<WebhookSubscription>().Where(x => x.WebhooksRelated.Any(y => y.WebhookDefinitionId == webhookId)).AsNoTracking().ToList();
        }
    }
}
