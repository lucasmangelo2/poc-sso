using ApiClient.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IConfiguration configuration,
            UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync()
        {
            using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
            {
                var body = await reader.ReadToEndAsync();
                WebhookRequest request = JsonConvert.DeserializeObject<WebhookRequest>(body);
                var secret = _configuration.GetValue<string>("WebHookSecretKey");
                var isValid = await IsSignatureCompatibleAsync(secret, body);

                if (!isValid)
                {
                    throw new Exception("Chave inválida");
                }

                if (request.Event == "user.insert")
                {

                }
                else if (request.Event == "user.update")
                {

                }
            }

            return Ok();
        }

        private async Task<bool> IsSignatureCompatibleAsync(string secret, string body)
        {
            if (!HttpContext.Request.Headers.ContainsKey("sso-webhook-secret"))
            {
                return false;
            }

            var receivedSignature = HttpContext.Request.Headers["sso-webhook-secret"].ToString().Split("=");

            string computedSignature;
            switch (receivedSignature[0])
            {
                case "sha256":
                    var secretBytes = Encoding.UTF8.GetBytes(secret);
                    using (var hasher = new HMACSHA256(secretBytes))
                    {
                        var data = Encoding.UTF8.GetBytes(body);
                        computedSignature = BitConverter.ToString(hasher.ComputeHash(data));
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            return computedSignature == receivedSignature[1];
        }
    }
}
