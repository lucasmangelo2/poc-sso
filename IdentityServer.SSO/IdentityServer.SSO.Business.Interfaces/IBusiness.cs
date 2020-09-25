using IdentityServer.SSO.Model;
using System;

namespace IdentityServer.SSO.Business.Interfaces
{
    public interface IBusiness<TModel> 
        where TModel : BaseModel
    {
    }
}
