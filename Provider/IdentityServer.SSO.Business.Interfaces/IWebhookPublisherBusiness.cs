using IdentityServer.SSO.Model;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IWebhookPublisherBusiness
    {
        Task PublishAsync(string webhookName, object data);
        Task PublishAsync(WebhookDefinition webhook, object data);
    }
}
