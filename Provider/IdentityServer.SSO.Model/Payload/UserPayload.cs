using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.SSO.Model.Payload
{
    public class UserPayload
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
