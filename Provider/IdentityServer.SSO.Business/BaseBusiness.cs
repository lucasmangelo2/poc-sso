using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business
{
    public class BaseBusiness<TRepository, TModel> : IBusiness<TModel>
        where TRepository : IRepository<TModel>
        where TModel : class
    {
        protected readonly TRepository _repository;
        public BaseBusiness(TRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            await _repository.DeleteAsync(model);
        }

        public virtual async Task<List<TModel>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).ToList();
        }

        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            return await _repository.InsertAsync(model);
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            return await _repository.UpdateAsync(model);
        }
    }
}
