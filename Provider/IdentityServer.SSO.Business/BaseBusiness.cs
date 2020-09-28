using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business
{
    public class BaseBusiness<TRepository, TModel> : IBusiness<TModel>
        where TRepository : IRepository<TModel>
        where TModel : BaseModel
    {
        private readonly TRepository _repository;
        public BaseBusiness(TRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);

            await _repository.DeleteAsync(model);
        }

        public async Task<List<TModel>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).ToList();
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TModel> InsertAsync(TModel model)
        {
            return await _repository.InsertAsync(model);
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            return await _repository.UpdateAsync(model);
        }
    }
}
