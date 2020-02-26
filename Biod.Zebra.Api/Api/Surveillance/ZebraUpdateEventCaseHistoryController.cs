using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Biod.Zebra.Api.Api.Surveillance
{
    public class ZebraUpdateEventCaseHistoryController : BaseApiController
    {
        public IZebraUpdateService RequestResponseService { get; set; }

        public ZebraUpdateEventCaseHistoryController()
        {
            RequestResponseService = new CustomRequestResponseService();
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] int eventId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                //1....Call this SP to update xtbl_event_location_History table
                var updated = DbContext.usp_ZebraEventSetEventCase(eventId).FirstOrDefault()?.Result ?? false;
                if (!updated)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Failed to update case count history for event {eventId}");
                }

                return Request.CreateResponse(HttpStatusCode.OK, $"Successfully updated the event location history data for event {eventId}");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }        
    }
}
