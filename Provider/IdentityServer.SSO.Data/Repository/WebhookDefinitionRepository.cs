using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Repository
{
    public class WebhookDefinitionRepository : BaseRepository<WebhookDefinition, IWebhookDbContext>, IWebhookDefinitionRepository
    {
        public WebhookDefinitionRepository(IWebhookDbContext context) : base(context)
        {
        }

        public Task<WebhookDefinition> GetByNameAsync(string name)
        {
            return Context.Set<WebhookDefinition>().FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
