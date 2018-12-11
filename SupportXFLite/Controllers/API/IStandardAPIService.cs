using System;
using System.Threading.Tasks;
using SupportXFLite.Models.API.Request;
using SupportXFLite.Models.API.Response;

namespace SupportXFLite.Controllers.API
{
    public enum RequestMethod
    {
        GET, POST, PUT
    }

    public interface IStandardAPIService
    {
        Task<APIStandardResponse<TResponse>> RequestPostAsync<TResponse>(string url, APIRequestBaseModel requestParameters);
        Task<APIStandardResponse<TResponse>> RequestGetAsync<TResponse>(string url, APIRequestBaseModel requestParameters);
        Task<APIStandardResponse<TResponse>> RequestPutAsync<TResponse>(string url, APIRequestBaseModel requestParameters);
        Task<APIStandardResponse<TResponse>> RequestAsync<TResponse>(RequestMethod requestMethod, string url, APIRequestBaseModel requestParameters);
    }
}