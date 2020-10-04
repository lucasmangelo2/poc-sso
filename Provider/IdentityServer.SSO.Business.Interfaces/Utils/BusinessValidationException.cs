using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.SSO.Business.Interfaces.Utils
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException(string message) : base(message)
        {
        }
    }
}
