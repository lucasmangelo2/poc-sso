using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business
{
    public class WebhookSubscriptionBusiness : BaseBusiness<IWebhookSubscriptionRepository, WebhookSubscription>, IWebhookSubscriptionBusiness
    {
        public WebhookSubscriptionBusiness(IWebhookSubscriptionRepository repository) : base(repository)
        {
        }

        public async Task<List<WebhookSubscription>> GetByWebhookIdAsync(long id)
        {
            return await _repository.GetByWebhookNameAsync(id);
        }
    }
}
