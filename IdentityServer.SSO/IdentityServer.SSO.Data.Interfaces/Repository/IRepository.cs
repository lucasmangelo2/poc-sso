using IdentityServer.SSO.Model;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Interfaces.Repository
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        Task<IQueryable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(int id);

        Task<TModel> InsertAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);

        Task DeleteAsync(TModel model);
    }
}
