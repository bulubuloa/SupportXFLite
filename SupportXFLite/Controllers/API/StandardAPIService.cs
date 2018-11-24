using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SupportXFLite.Models.API.Request;

namespace SupportXFLite.Controllers.API
{
    public class StandardAPIService : BaseController, IStandardAPIService
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

        public virtual async Task<TResponse> RequestGetAsync<TResponse>(string url, APIRequestBaseModel requestParameters)
        {
            return await RequestAsync<TResponse>(RequestMethod.GET,url,requestParameters);
        }

        public virtual async Task<TResponse> RequestPutAsync<TResponse>(string url, APIRequestBaseModel requestParameters)
        {
            return await RequestAsync<TResponse>(RequestMethod.PUT, url, requestParameters);
        }

        public virtual async Task<TResponse> RequestPostAsync<TResponse>(string url, APIRequestBaseModel requestParameters)
        {
            return await RequestAsync<TResponse>(RequestMethod.POST, url, requestParameters);
        }

        public virtual async Task<TResponse> RequestAsync<TResponse>(RequestMethod requestMethod, string url, APIRequestBaseModel requestParameters)
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

        protected async Task<TResponse> BasicConsumeAPI<TResponse>(RequestMethod requestMethod, string requestURL, APIRequestBaseModel requestParams, System.Net.Http.HttpClient httpClientAsync)
        {
            Exception exception = null;
            TResponse response = default(TResponse);
            bool IsSuccess = false;
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
                            DebugMessage(requestURL + paramete);
                            httpResponse = await httpClient.GetAsync(requestURL + paramete, cts.Token);
                        }
                        /*
                         * Process with POST Method
                         */
                        else if (requestMethod == RequestMethod.POST)
                        {
                            var paramete = requestParams.Get_PostParamsRequest();
                            DebugMessage(requestURL);
                            DebugMessage(paramete, "Parameters");
                            httpResponse = await httpClient.PostAsync(requestURL, new StringContent(paramete, Encoding.UTF8, "application/json"), cts.Token);
                        }
                        /*
                         * Process with PUT Method
                         */
                        else if (requestMethod == RequestMethod.PUT)
                        {
                            var paramete = requestParams.Get_PutParamsRequest();
                            DebugMessage(requestURL);
                            DebugMessage(paramete, "Parameters");
                            httpResponse = await httpClient.PutAsync(requestURL, new StringContent(paramete, Encoding.UTF8, "application/json"), cts.Token);
                        }

                        /*
                         * Process reponse after request
                         */
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            var responseRaw = httpResponse.Content.ReadAsStringAsync().Result;
                            if (responseRaw.Length < 1000)
                                DebugMessage(responseRaw, "Response");
                            else
                                DebugMessage(responseRaw.Substring(0, 999), "Response");

                            response = await Task.Run(() => JsonConvert.DeserializeObject<TResponse>(responseRaw, jsonSerializerSettings));

                            IsSuccess = true;
                        }
                        else
                        {
                            DebugMessage(httpResponse.StatusCode.ToString(), "Response Code");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                DebugMessage(ex.StackTrace, "ParseJsonError");
            }

            if (exception == null && IsSuccess)
            {
                /*
                 * Request successfully and return data
                 */
                return response;
            }
            else
            {
                /*
                 * Request error and return default object of reponse model
                 */
                return response;
            }
        }
    }
}