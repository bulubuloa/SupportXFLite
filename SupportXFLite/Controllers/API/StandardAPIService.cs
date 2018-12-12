using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SupportXFLite.Models.API.Request;
using SupportXFLite.Models.API.Response;

namespace SupportXFLite.Controllers.API
{
    public abstract class StandardAPIService : BaseController, IStandardAPIService
    {
        protected readonly JsonSerializerSettings jsonSerializerSettings;

        public StandardAPIService()
        {
            jsonSerializerSettings = new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
        }

        public virtual async Task<APIStandardResponse<TResponse>> RequestPostAsync<TResponse>(string url, APIRequestBaseModel requestParameters)
        {
            return await RequestAsync<TResponse>(RequestMethod.POST, url, requestParameters);
        }

        public virtual async Task<APIStandardResponse<TResponse>> RequestGetAsync<TResponse>(string url, APIRequestBaseModel requestParameters)
        {
            return await RequestAsync<TResponse>(RequestMethod.GET, url, requestParameters);
        }

        public virtual async Task<APIStandardResponse<TResponse>> RequestPutAsync<TResponse>(string url, APIRequestBaseModel requestParameters)
        {
            return await RequestAsync<TResponse>(RequestMethod.PUT, url, requestParameters);
        }

        public virtual async Task<APIStandardResponse<TResponse>> RequestAsync<TResponse>(RequestMethod requestMethod, string url, APIRequestBaseModel requestParameters)
        {
            return await BasicConsumeAPI<TResponse>(requestMethod,url,requestParameters,GetHttpClient());
        }

        protected virtual System.Net.Http.HttpClient GetHttpClient()
        {
            return new System.Net.Http.HttpClient();
        }

        protected virtual System.Net.Http.HttpClient GetHttpClient(DelegatingHandler delegatingHandler)
        {
            return new System.Net.Http.HttpClient(delegatingHandler);
        }

        protected virtual async Task<APIStandardResponse<TResponse>> BasicConsumeAPI<TResponse>(RequestMethod requestMethod, string requestURL, APIRequestBaseModel requestParams, System.Net.Http.HttpClient httpClientAsync)
        {
            Exception exception = null;
            APIStandardResponse<TResponse> aPIStandardResponse = (APIStandardResponse<TResponse>)Activator.CreateInstance(typeof(APIStandardResponse<TResponse>));

            try
            {
                using (var cts = new CancellationTokenSource())
                {
                    using (var httpClient = httpClientAsync)
                    {
                        httpClient.Timeout = TimeSpan.FromSeconds(Utils.API_REQUEST_TIMEOUT);

                        HttpResponseMessage httpResponse = null;

                        /*
                         * Process with GET Method
                         */
                        if (requestMethod == RequestMethod.GET)
                        {
                            var paramete = requestParams.Get_GetParamsRequest();
                            httpResponse = await httpClient.GetAsync(requestURL + paramete, cts.Token);
                        }
                        /*
                         * Process with POST Method
                         */
                        else if (requestMethod == RequestMethod.POST)
                        {
                            var paramete = requestParams.Get_PostParamsRequest();
                            httpResponse = await httpClient.PostAsync(requestURL, new StringContent(paramete, Encoding.UTF8, "application/json"), cts.Token);
                        }
                        /*
                         * Process with PUT Method
                         */
                        else if (requestMethod == RequestMethod.PUT)
                        {
                            var paramete = requestParams.Get_PutParamsRequest();
                            httpResponse = await httpClient.PutAsync(requestURL, new StringContent(paramete, Encoding.UTF8, "application/json"), cts.Token);
                        }

                        /*
                         * Process reponse after request
                         */


                        if(httpResponse != null)
                        {
                            aPIStandardResponse.ObjectRaw = httpResponse.Content.ReadAsStringAsync().Result;

                            if (httpResponse.IsSuccessStatusCode)
                            {
                                aPIStandardResponse.RequestOK = true;

                                aPIStandardResponse.ObjectSuccess = await Task.Run(() =>
                                {
                                    var result = JsonConvert.DeserializeObject<TResponse>(aPIStandardResponse.ObjectRaw, jsonSerializerSettings);
                                    aPIStandardResponse.JsonConvertSuccess = true;
                                    return result;
                                });
                            }
                            else
                            {
                                aPIStandardResponse.StatusCode = httpResponse.StatusCode;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                DebugMessage(ex.StackTrace, "ParseJsonError");
            }

            if (exception == null)
            {
                /*
                 * Request successfully and return data
                 */
                return aPIStandardResponse;
            }
            else
            {
                /*
                 * Request error and return default object of reponse model
                 */
                return aPIStandardResponse;
            }
        }
    }
}