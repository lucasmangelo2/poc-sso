using IdentityServer.SSO.Models;
using System;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IBusiness<TModel> 
        where TModel : BaseModel
    {
    }
}
