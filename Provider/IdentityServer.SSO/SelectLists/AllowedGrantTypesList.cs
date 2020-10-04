using IdentityServer.SSO.Infra.Extensions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IdentityServer.SSO.SelectLists
{
    public static class AllowedGrantTypesList
    {
        public static List<SelectListItem> GetItems()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = string.Empty, Value = string.Empty},
                new SelectListItem { Text = "Code", Value = GrantTypes.Code.JoinListToString()},
                new SelectListItem { Text = "Code And Client Credentials", Value = GrantTypes.CodeAndClientCredentials.JoinListToString()},
                new SelectListItem { Text = "Implicit And Client Hybrid", Value = GrantTypes.Hybrid.JoinListToString()},
                new SelectListItem { Text = "Hybrid And Client Credentials", Value = GrantTypes.HybridAndClientCredentials.JoinListToString()},
                new SelectListItem { Text = "Client Credentials", Value = GrantTypes.ClientCredentials.JoinListToString()},
                new SelectListItem { Text = "Resource Owner Password", Value = GrantTypes.ResourceOwnerPassword.JoinListToString()},
                new SelectListItem { Text = "Resource Owner Password And Client Credentials", Value = GrantTypes.ResourceOwnerPasswordAndClientCredentials.JoinListToString()},
                new SelectListItem { Text = "Device Flow", Value = GrantTypes.DeviceFlow.JoinListToString()},
            };
        }
    }
}
