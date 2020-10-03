using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Infra.Extensions
{
    public static class CollectionExtension
    {
        public static string JoinListToString(this ICollection<string> list, string separator = ",")
        {
            return string.Join(separator, list);
        }
    }
}
