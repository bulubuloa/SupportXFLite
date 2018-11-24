using System;
using System.Threading.Tasks;
using SupportXFLite.Models.API.Request;
using SupportXFLite.Models.API.Response;

namespace SupportXFLite.Controllers.API.Restful
{
    public interface IRestfulAPIService : IStandardAPIService
    {
        Task<TResponse> RequestAsyncToken<TResponse, TRequest>(RequestMethod requestMethod, string url, TRequest requestParameters,string token = "") where TRequest : StandardRequestRestfulBaseModel;
        Task<TResponse> RequestAsyncCredential<TResponse,TRequest>(RequestMethod requestMethod, string url, TRequest requestParameters, string apiUsername, string apiPassword) where TRequest : StandardRequestRestfulBaseModel;
    }
}