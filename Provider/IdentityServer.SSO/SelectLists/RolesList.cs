using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IdentityServer.SSO.SelectLists
{
    public static class RolesList
    {
        public static List<SelectListItem> GetItems()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = string.Empty, Value = string.Empty},
                new SelectListItem { Text = "Administrador", Value = "Admin"},
                new SelectListItem { Text = "Convidado", Value = "Guest"},
                new SelectListItem { Text = "Operador", Value = "Operator"},
            };
        }
    }
}
