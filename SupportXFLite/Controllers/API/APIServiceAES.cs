﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SupportXFLite.Models.API.Request;
using SupportXFLite.Models.API.Response;

namespace SupportXFLite.Controllers.API
{
    public class APIServiceAES : IAPIService
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public APIServiceAES()
        {
            jsonSerializerSettings = new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResponse> RequesGetAsync<TResponse>(string url, AESRequestBaseModel param, string token = "")
        {
            return await ConfigurationHttpClientRequest<TResponse>(RequestMethod.GET, url, param, token);
        }

        public async Task<TResponse> RequesPutAsync<TResponse>(string url, AESRequestBaseModel param, string token = "")
        {
            return await ConfigurationHttpClientRequest<TResponse>(RequestMethod.PUT, url, param, token);
        }

        public async Task<TResponse> RequestAsync<TResponse>(RequestMethod requestMethod, string url, AESRequestBaseModel param, string token = "")
        {
            return await ConfigurationHttpClientRequest<TResponse>(requestMethod, url, param, token);
        }

        public async Task<TResponse> RequestPostAsync<TResponse>(string url, AESRequestBaseModel param, string token = "")
        {
            return await ConfigurationHttpClientRequest<TResponse>(RequestMethod.POST, url, param, token);
        }

        protected virtual void LogicTokenExpired()
        {

        }

        protected virtual DelegatingHandler GetSupportHandler()
        {
            return new TimeoutHandler
            {
                DefaultTimeout = TimeSpan.FromSeconds(Utils.API_REQUEST_TIMEOUT),
                InnerHandler = new HttpClientHandler()
            };
        }

        protected async Task<TResponse> ConfigurationHttpClientRequest<TResponse>(RequestMethod requestMethod, string requestURL, AESRequestBaseModel requestParams, string token = "")
        {
            return await ExcuteMe<TResponse>(requestMethod, requestURL, requestParams, GetSupportHandler(), token);
        }

        public async Task<TResponse> ExcuteMe<TResponse>(RequestMethod requestMethod, string requestURL, AESRequestBaseModel requestParams, DelegatingHandler delegatingHandler, string token = "")
        {
            Exception exception = null;
            TResponse response = default(TResponse);

            bool IsSuccess = false;

            try
            {
                using (var cts = new CancellationTokenSource())
                using (var httpClient = new System.Net.Http.HttpClient(delegatingHandler))
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(Utils.API_REQUEST_TIMEOUT);
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    HttpResponseMessage httpResponse = null;
                    if (requestMethod == RequestMethod.GET)
                    {
                        var paramete = requestParams.Get_GetParamsRequest();
                        Debug.WriteLine(requestURL + "" + paramete);

                        httpResponse = await httpClient.GetAsync(requestURL + paramete, cts.Token);
                    }
                    else if (requestMethod == RequestMethod.POST)
                    {
                        var paramete = requestParams.Get_PostParamsRequest();
                        Debug.WriteLine(requestURL);
                        Debug.WriteLine("param {0} ", paramete);

                        httpResponse = await httpClient.PostAsync(requestURL, new StringContent(paramete, Encoding.UTF8, "application/json"), cts.Token);
                    }
                    else if (requestMethod == RequestMethod.PUT)
                    {
                        var paramete = requestParams.Get_PutParamsRequest();
                        Debug.WriteLine(requestURL);
                        Debug.WriteLine("param {0} ", paramete);

                        httpResponse = await httpClient.PostAsync(requestURL, new StringContent(paramete, Encoding.UTF8, "application/json"), cts.Token);
                    }

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var responseRaw = httpResponse.Content.ReadAsStringAsync().Result;
                        if (responseRaw.Length < 1000)
                            Debug.WriteLine("Response raw {0}", responseRaw);
                        else
                            Debug.WriteLine("Response raw {0}", responseRaw.Substring(0, 999));

                        response = await Task.Run(() => JsonConvert.DeserializeObject<TResponse>(responseRaw, jsonSerializerSettings));

                        try
                        {
                            var fack = response as AESResponseBaseModel;
                            if (fack.StatusCode == Utils.STATUSCODE_UNAUTHORIZE)
                            {
                                LogicTokenExpired();
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.StackTrace);
                        }

                        IsSuccess = true;
                    }
                    else
                    {
                        Debug.WriteLine("Response code {0}", httpResponse.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Debug.WriteLine("Parse to json error {0}", ex.StackTrace);
            }

            if (exception == null && IsSuccess)
            {
                return response;
            }
            else
            {
                //throw exception;
                return response;
                //return Task.FromResult<TResponse>(null);
            }
        }

    }
}
