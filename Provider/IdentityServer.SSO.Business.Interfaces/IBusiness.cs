using IdentityServer.SSO.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IBusiness<TModel> 
        where TModel : BaseModel
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(int id);

        Task<TModel> InsertAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);

        Task DeleteAsync(int id);
    }
}
