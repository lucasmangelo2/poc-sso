using IdentityServer.SSO.Models;
using System;

namespace IdentityServer.SSO.Data.Interfaces
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
    }
}
