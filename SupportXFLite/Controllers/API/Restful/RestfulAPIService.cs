using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SupportXFLite.Controllers.HttpClient;
using SupportXFLite.Models.API.Request;

namespace SupportXFLite.Controllers.API.Restful
{
    public class RestfulAPIService : StandardAPIService, IRestfulAPIService
    {
        public RestfulAPIService()
        {
        }

        public virtual async Task<TResponse> RequestAsyncToken<TResponse, TRequest>(RequestMethod requestMethod, string url, TRequest requestParameters, string token = "") where TRequest : StandardRequestRestfulBaseModel
        {
            var httpClient = GetHttpClient();

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await BasicConsumeAPI<TResponse>(requestMethod, url, requestParameters, httpClient);
            /*
             *Logic here 
             */

            return response.ObjectSuccess;
        }

        public virtual async Task<TResponse> RequestAsyncCredential<TResponse, TRequest>(RequestMethod requestMethod, string url, TRequest requestParameters, string apiUsername, string apiPassword) where TRequest : StandardRequestRestfulBaseModel
        {
            var handler = new TimeoutHandler
            {
                DefaultTimeout = TimeSpan.FromSeconds(Utils.API_REQUEST_TIMEOUT),
                InnerHandler = new HttpClientHandler()
                {
                    Credentials = new System.Net.NetworkCredential(apiUsername, apiPassword)
                }
            };
            var httpClient = GetHttpClient(handler);
            var response = await BasicConsumeAPI<TResponse>(requestMethod, url, requestParameters, httpClient);
            /*
             *Logic here 
             */
            return response.ObjectSuccess;
        }

        public virtual async Task<TResponse> RequestAsyncBasic<TResponse, TRequest>(RequestMethod requestMethod, string url, TRequest requestParameters) where TRequest : StandardRequestRestfulBaseModel
        {
            var response = await BasicConsumeAPI<TResponse>(requestMethod, url, requestParameters, GetHttpClient());
            /*
             *Logic here 
             */
            return response.ObjectSuccess;
        }
    }
}