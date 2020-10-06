using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClient.Requests
{
    public class WebhookRequest
    {
        public string Event { get; set; }

        public object Data { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
