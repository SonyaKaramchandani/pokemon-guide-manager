using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BdDataApi.Infrastructures
{
    public static class CustomResponseHandler
    {
        public static HttpResponseMessage GetHttpResponse(bool isSuccess, string message)
        {
            var response = new HttpResponseMessage();
            if (isSuccess)
                response.StatusCode = HttpStatusCode.OK;
            else
                response.StatusCode = HttpStatusCode.BadRequest;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("Debug")))
                response.Content = new StringContent(message);

            return response;
        }
    }
}