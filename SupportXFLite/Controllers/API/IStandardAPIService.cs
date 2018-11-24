using System;
using System.Threading.Tasks;
using SupportXFLite.Models.API.Request;

namespace SupportXFLite.Controllers.API
{
    public enum RequestMethod
    {
        GET, POST, PUT
    }

    public interface IStandardAPIService
    {
        Task<TResponse> RequestPostAsync<TResponse>(string url, APIRequestBaseModel requestParameters);
        Task<TResponse> RequestGetAsync<TResponse>(string url, APIRequestBaseModel requestParameters);
        Task<TResponse> RequestPutAsync<TResponse>(string url, APIRequestBaseModel requestParameters);
        Task<TResponse> RequestAsync<TResponse>(RequestMethod requestMethod, string url, APIRequestBaseModel requestParameters);
    }
}