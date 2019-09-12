using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Biod.Zebra.Api.Api;
using System.Net.Http;
using System.Net;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraDestinationAirportsController : BaseApiController
    {
        /// <summary>Get destination airports.</summary>
        /// <param name="EventId"></param>
        /// <param name="GeonameIds"></param>
        /// <returns>EventsInfoViewModel</returns>
        public HttpResponseMessage Get(int EventId, string GeonameIds = "")
        {
            try
            {
                var result = DbContext.usp_ZebraEventGetDestinationAirportsByEventId(EventId, GeonameIds ?? "")
                    .Where(x =>
                        x.CityDisplayName != "-" &&
                        x.StationName != "-" &&
                        x.StationCode != "-" &&
                        x.Latitude != null &&
                        x.Longitude != null)
                    .ToList();

                Logger.Info($"Successfully returned event destination airports for event ID {EventId} with geoname IDs {GeonameIds}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return event destination airports for event ID {EventId} with geoname IDs {GeonameIds}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}