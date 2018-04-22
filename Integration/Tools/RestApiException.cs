using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Integration.Tools
{
    public class RestApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReturnMessage { get; set; }



    }
}
