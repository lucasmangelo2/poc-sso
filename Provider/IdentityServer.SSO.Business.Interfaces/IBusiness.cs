using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IBusiness<TModel> where TModel : class
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> InsertAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);
        Task DeleteAsync(TModel model);
    }
}
