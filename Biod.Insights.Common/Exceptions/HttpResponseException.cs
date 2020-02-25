using System;
using System.Net;

namespace Biod.Insights.Common.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(HttpStatusCode statusCode, string message) : base(message)
        {
            Status = statusCode;
            Message = message;
        }

        public HttpStatusCode Status { get; }

        public override string Message { get; }
    }
}