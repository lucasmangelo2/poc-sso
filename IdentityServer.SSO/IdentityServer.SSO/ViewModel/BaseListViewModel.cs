using System.Collections.Generic;

namespace IdentityServer.SSO.ViewModel
{
    public class BaseListViewModel<T> where T : class
    {
        public List<T> Data { get; set; } = new List<T>();
    }
}
