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
        private readonly IWebhookDefinitionRepository _webhookDefinitionRepository;

        public WebhookPublisherBusiness(
            IWebhookSenderBusiness webhookSender,
            IWebhookEventRepository webhookEventRepository,
            IWebhookSubscriptionBusiness webhookSubscriptionBusiness,
            IWebhookDefinitionRepository webhookDefinitionRepository)
        {
            _webhookSender = webhookSender;
            _webhookEventRepository = webhookEventRepository;
            _webhookSubscriptionBusiness = webhookSubscriptionBusiness;
            _webhookDefinitionRepository = webhookDefinitionRepository;
        }

        public async Task PublishAsync(string webhookName, object data)
        {
            var webhook = await _webhookDefinitionRepository.GetByNameAsync(webhookName);

            if (webhook != null)
            {
                await PublishAsync(webhook, data);
            }
        }

        public async Task PublishAsync(WebhookDefinition webhook, object data)
        {
            var webhookSubscriptions = await _webhookSubscriptionBusiness.GetByWebhookIdAsync(webhook.Id);

            if (webhookSubscriptions == null || (webhookSubscriptions != null && webhookSubscriptions.Count == 0))
            {
                return;
            }

            var webhookEvent = await InsertWebhookEventAsync(webhook.Id, data);

            foreach (var webhookSubscription in webhookSubscriptions)
            {
                await _webhookSender.SendAsync(new WebhookSenderArgs
                {
                    WebhookEventId = webhookEvent.Id,
                    Data = webhookEvent.Data,
                    WebhookName = webhook.Name,
                    WebhookSubscriptionId = webhookSubscription.Id,
                    Secret = webhookSubscription.Secret,
                    WebhookUri = webhookSubscription.WebhookUri
                });
            }
        }

        private async Task<WebhookEvent> InsertWebhookEventAsync(long webhookId, object data)
        {
            var webhookEvent = new WebhookEvent
            {
                WebhookDefinitionId = webhookId,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(data)
            };

            await _webhookEventRepository.InsertAsync(webhookEvent);
            return webhookEvent;
        }
    }
}
