using Biod.Zebra.Library.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Biod.Zebra.Api.Api.Surveillance
{
    public class GetPostTestController : BaseApiController
    {

        public HttpResponseMessage Get()
        {
            Logger.Debug("GetPostTestController GET: Step 1");
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Sucess message");
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Post(int eventId)
        {
            Logger.Debug("GetPostTestController POST: Step 1");
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Sucesfully Saved event = " + eventId);
            return response;
        }
    }
}
