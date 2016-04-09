using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bikeground.API.Exceptions
{
    public class BusinessException : Exception
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public BusinessException(string statusMessage)
        {
            StatusMessage = statusMessage;
        }
    }

    public class CriticalException : Exception
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }

    public class AuthorizationException : Exception
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }

    public class DatabaseException : Exception
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}