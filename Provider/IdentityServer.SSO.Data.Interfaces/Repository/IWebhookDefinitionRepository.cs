using IdentityServer.SSO.Model;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Interfaces.Repository
{
    public interface IWebhookDefinitionRepository : IRepository<WebhookDefinition>
    {
        public Task<WebhookDefinition> GetByNameAsync(string name);
    }
}
