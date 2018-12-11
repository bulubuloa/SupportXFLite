using System;
using System.Net;

namespace SupportXFLite.Models.API.Response
{
    public class APIStandardResponse<TResponse>
    {
        public TResponse ObjectSuccess { set; get; }
        public string ObjectRaw { set; get; }
        public HttpStatusCode StatusCode { set; get; }
        public bool RequestOK { set; get; }

        public APIStandardResponse()
        {
            RequestOK = false;
        }
    }
}