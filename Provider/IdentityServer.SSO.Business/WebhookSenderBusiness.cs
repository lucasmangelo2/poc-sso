using IdentityServer.SSO.Business.Interfaces;
using IdentityServer.SSO.Model;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Business
{
    public class WebhookSenderBusiness : IWebhookSenderBusiness
    {
        public async Task SendAsync(WebhookSenderArgs webhookSenderArgs)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, webhookSenderArgs.WebhookUri);

            var serializedBody = await GetSerializedBodyAsync(webhookSenderArgs);

            request.Content = new StringContent(serializedBody, Encoding.UTF8, "application/json");

            SignWebhookRequest(request, serializedBody, webhookSenderArgs.Secret);

            try
            {
                var response = await SendHttpRequest(request);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<string> GetSerializedBodyAsync(WebhookSenderArgs webhookSenderArgs)
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(webhookSenderArgs.Data);

            var payload = new WebhookPayload()
            {
                Event = webhookSenderArgs.WebhookEventId.ToString(),
                Data = data,
                CreationTimeUtc = DateTime.Now
            };

            return JsonConvert.SerializeObject(payload);
        }

        protected virtual void SignWebhookRequest(HttpRequestMessage request, string serializedBody, string secret)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            using (var hasher = new HMACSHA256(secretBytes))
            {
                var data = Encoding.UTF8.GetBytes(serializedBody);

                var signatureBytes = hasher.ComputeHash(data);

                var headerValue = string.Format(CultureInfo.InvariantCulture, "secret", BitConverter.ToString(signatureBytes));

                request.Headers.Add("secret", headerValue);
            }
        }

        protected virtual async Task<(bool isSucceed, HttpStatusCode statusCode, string content)> SendHttpRequest(HttpRequestMessage request)
        {
            using (var client = new HttpClient { Timeout = DateTime.Now.AddMinutes(2).TimeOfDay })
            {
                var response = await client.SendAsync(request);

                var isSucceed = response.IsSuccessStatusCode;
                var statusCode = response.StatusCode;
                var content = await response.Content.ReadAsStringAsync();

                return (isSucceed, statusCode, content);
            }
        }
    }
}
