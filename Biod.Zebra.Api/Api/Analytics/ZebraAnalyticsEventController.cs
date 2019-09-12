using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Biod.Zebra.Api.Analytics;

namespace Biod.Zebra.Api.Api.Analytics
{
    [RoutePrefix("api/ZebraAnalytics")]
    public class ZebraAnalyticsEventController : ZebraAnalyticsApiController
    {
        // GET api/ZebraAnalytics/Event
        /// <summary>
        /// Gets the latest event information given an event id
        /// </summary>
        /// <param name="eventId">the event id</param>
        /// <returns>the event information</returns>
        [Route("Event")]
        [HttpGet]
        public HttpResponseMessage ZebraAnalyticsGetEventDetailInfoById(int eventId)
        {
            try
            {
                var eventResult = DbContext.usp_ZebraAnalyticsGetEventByEventId(eventId).FirstOrDefault();

                if (eventResult == null)
                {
                    Logger.Warning($"Event details for event id {eventId} not found");
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                Logger.Info($"Successfully returned event details for event id {eventId}");
                return Request.CreateResponse(HttpStatusCode.OK, eventResult);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return event details for event id {eventId}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
