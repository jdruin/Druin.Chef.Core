using Druin.Chef.Core.Authentication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Druin.Chef.Core.Requests
{
    internal class RequestAgent
    {
        private readonly Uri server;

        public RequestAgent(string server)
            : this(new Uri(server))
        {
        }

        public RequestAgent(Uri server)
        {
            this.server = server;
        }

        public HttpResponseMessage SendRequest(AuthenticatedRequest request)
        {
            return SendRequestAsync(request).Result;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(AuthenticatedRequest request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = server;

                var message = request.Create();
                var result = await client.SendAsync(message);

                result.EnsureSuccessStatusCode();

                return result;
            }
        }
    }
}