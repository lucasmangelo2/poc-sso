using IdentityServer.SSO.Model;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IWebhookSenderBusiness
    {
        Task SendAsync(WebhookSenderArgs args); 
    }
}
