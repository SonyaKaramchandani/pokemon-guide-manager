using System;
using System.Net;

namespace Biod.Insights.Api.Exceptions
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