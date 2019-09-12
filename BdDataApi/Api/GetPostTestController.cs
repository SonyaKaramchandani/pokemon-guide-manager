using BdDataApi.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BdDataApi.Api
{
    public class GetPostTestController : ApiController
    {

        public HttpResponseMessage Get()
        {
            Logging.Log("GetPostTestController GET: Step 1");
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Sucess message");
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Post(int eventId)
        {
            Logging.Log("GetPostTestController POST: Step 1");
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Sucesfully Saved event = " + eventId);
            return response;
        }
    }
}
