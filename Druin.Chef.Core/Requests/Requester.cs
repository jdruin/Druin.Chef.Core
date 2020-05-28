using Druin.Chef.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Core.Requests
{
    public class Requester : IRequester
    {
        private readonly IChefConnection connectionInfo;

        public Requester(IChefConnection conn)
        {
            this.connectionInfo = conn;
        }

        public Task<HttpResponseMessage> PutRequestAsync(string endPoint, string body, string parameter = "")
        {
            return MakeRequestAsync(connectionInfo, endPoint, HttpMethod.Put, body, parameter);
        }

        public Task<HttpResponseMessage> PutRequestAsync(string endPoint, byte[] file, string checksum, string parameter = "")
        {
            return MakeRequestAsync(connectionInfo, endPoint, HttpMethod.Put, file, checksum, parameter);
        }

        public Task<HttpResponseMessage> PostRequestAsync(string endPoint, string body, string parameter = "")
        {
            return MakeRequestAsync(connectionInfo, endPoint, HttpMethod.Post, body, parameter);
        }

        public Task<HttpResponseMessage> PostRequestAsync(string endPoint, byte[] file, string checksum, string parameter = "")
        {
            return MakeRequestAsync(connectionInfo, endPoint, HttpMethod.Post, file, checksum, parameter);
        }

        public Task<HttpResponseMessage> DeleteRequestAsync(string endPoint, string parameter = "")
        {
            return MakeRequestAsync(connectionInfo, endPoint, HttpMethod.Delete, String.Empty, parameter);
        }

        public Task<HttpResponseMessage> GetRequestAsync(string endPoint, string parameter = "")
        {
            return MakeRequestAsync(connectionInfo, endPoint, HttpMethod.Get, String.Empty, parameter);
        }

        public Task<HttpResponseMessage> MakeRequestAsync(IChefConnection connectionInfo, string endPoint, HttpMethod method, string body, string parameter)
        {
            return MakeRequestAsync(connectionInfo.ChefServer, endPoint, connectionInfo.PrivateKey, connectionInfo.UserId, method, body, parameter);
        }

        public Task<HttpResponseMessage> MakeRequestAsync(IChefConnection connectionInfo, string endPoint, HttpMethod method, byte[] file, string checksum, string parameter)
        {
            return MakeRequestAsync(connectionInfo.ChefServer, endPoint, connectionInfo.PrivateKey, connectionInfo.UserId, method, file, checksum, parameter);
        }

        public Task<HttpResponseMessage> MakeRequestAsync(string chefServer, string endPoint, string privateKey, string userId, HttpMethod method, byte[] file, string checksum, string parameter)
        {
            ServicePointManager.DefaultConnectionLimit = 50;
            var baseUri = new Uri(chefServer);

            endPoint = endPoint + parameter;

            var requestUri = new Uri(baseUri, endPoint);

            var authenticatedRequest = new AuthenticatedRequest(userId, requestUri, method, file);

            authenticatedRequest.Sign(privateKey);

            var server = new RequestAgent(baseUri.ToString());

            var resultContent = server.SendRequestAsync(authenticatedRequest);

            return resultContent;
        }

        public Task<HttpResponseMessage> MakeRequestAsync(string chefServer, string endPoint, string privateKey, string userId, HttpMethod method, string body, string parameter)
        {
            ServicePointManager.DefaultConnectionLimit = 50;
            var baseUri = new Uri(chefServer);

            endPoint = endPoint + parameter;

            var requestUri = new Uri(baseUri, endPoint);

            var authenticatedRequest = new AuthenticatedRequest(userId, requestUri, method, body);

            authenticatedRequest.Sign(privateKey);

            var server = new RequestAgent(baseUri.ToString());

            var resultContent = server.SendRequestAsync(authenticatedRequest);

            return resultContent;
        }

        public IChefConnection GetChefConnection()
        {
            return connectionInfo;
        }
    }
}