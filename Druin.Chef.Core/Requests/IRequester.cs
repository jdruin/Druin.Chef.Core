using System.Net.Http;
using System.Threading.Tasks;
using Druin.Chef.Core.Authentication;

namespace Druin.Chef.Core.Requests
{
    public interface IRequester
    {
        IChefConnection GetChefConnection();
        Task<HttpResponseMessage> DeleteRequestAsync(string endPoint, string parameter = "");
        Task<HttpResponseMessage> GetRequestAsync(string endPoint, string parameter = "");
        Task<HttpResponseMessage> MakeRequestAsync(IChefConnection connectionInfo, string endPoint, HttpMethod method, byte[] file, string checksum, string parameter);
        Task<HttpResponseMessage> MakeRequestAsync(IChefConnection connectionInfo, string endPoint, HttpMethod method, string body, string parameter);
        Task<HttpResponseMessage> MakeRequestAsync(string chefServer, string endPoint, string privateKey, string userId, HttpMethod method, byte[] file, string checksum, string parameter);
        Task<HttpResponseMessage> MakeRequestAsync(string chefServer, string endPoint, string privateKey, string userId, HttpMethod method, string body, string parameter);
        Task<HttpResponseMessage> PostRequestAsync(string endPoint, byte[] file, string checksum, string parameter = "");
        Task<HttpResponseMessage> PostRequestAsync(string endPoint, string body, string parameter = "");
        Task<HttpResponseMessage> PutRequestAsync(string endPoint, byte[] file, string checksum, string parameter = "");
        Task<HttpResponseMessage> PutRequestAsync(string endPoint, string body, string parameter = "");
    }
}