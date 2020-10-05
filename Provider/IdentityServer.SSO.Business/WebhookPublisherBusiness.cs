using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business
{
    public class WebhookPublisherBusiness : IWebhookPublisherBusiness
    {
        private readonly IWebhookSenderBusiness _webhookSender;
        private readonly IWebhookEventRepository _webhookEventRepository;
        private readonly IWebhookSubscriptionBusiness _webhookSubscriptionBusiness;

        public WebhookPublisherBusiness(
            IWebhookSenderBusiness webhookSender,
            IWebhookEventRepository webhookEventRepository,
            IWebhookSubscriptionBusiness webhookSubscriptionBusiness)
        {
            _webhookSender = webhookSender;
            _webhookEventRepository = webhookEventRepository;
            _webhookSubscriptionBusiness = webhookSubscriptionBusiness;
        }

        public async Task PublishAsync(string webhookName, object data)
        {
            var webhookSubscriptions = await _webhookSubscriptionBusiness.GetByWebhookNameAsync(webhookName);

            if (webhookSubscriptions != null || (webhookSubscriptions != null && webhookSubscriptions.Count == 0))
            {
                return;
            }

            var webhookEvent = await SaveAndGetWebhookEventAsync(webhookName, data);

            foreach (var webhookSubscription in webhookSubscriptions)
            {
                await _webhookSender.SendAsync(new WebhookSenderArgs
                {
                    WebhookEventId = webhookEvent.Id,
                    Data = webhookEvent.Data,
                    WebhookName = webhookEvent.WebhookName,
                    WebhookSubscriptionId = webhookSubscription.Id,
                    Headers = webhookSubscription.Headers,
                    Secret = webhookSubscription.Secret,
                    WebhookUri = webhookSubscription.WebhookUri
                });
            }
        }

        protected virtual async Task<WebhookEvent> SaveAndGetWebhookEventAsync(string webhookName, object data)
        {
            var webhookEvent = new WebhookEvent
            {
                WebhookName = webhookName,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(data)
            };

            await _webhookEventRepository.InsertAsync(webhookEvent);
            return webhookEvent;
        }
    }
}
