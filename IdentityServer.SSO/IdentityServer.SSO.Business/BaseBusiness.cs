using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Data.Interfaces;
using IdentityServer.SSO.Models;

namespace IdentityServer.SSO.Business
{
    public class BaseBusiness<TModel> : IBusiness<TModel> 
        where TModel : BaseModel
    {
        private readonly IRepository<TModel> _repository;
        public BaseBusiness(IRepository<TModel> repository)
        {
            _repository = repository;
        }
    }
}
