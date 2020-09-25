using IdentityServer.SSO.Model;
using System;

namespace IdentityServer.SSO.Data.Interfaces
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
    }
}
