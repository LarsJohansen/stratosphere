using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Tools
{
    public class RestApiException : Exception
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }



    }
}
