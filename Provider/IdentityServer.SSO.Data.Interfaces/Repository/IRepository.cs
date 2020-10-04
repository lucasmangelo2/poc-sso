using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Interfaces.Repository
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<IQueryable<TModel>> GetAllAsync();

        Task<TModel> InsertAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);

        Task DeleteAsync(TModel model);
    }
}
