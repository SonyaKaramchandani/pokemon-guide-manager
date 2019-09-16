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
using System.Net.Http;
using Biod.Zebra.Api.Api;
using System.Net;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraInitDashboardController : BaseApiController
    {
        /// <summary>
        /// Initialize the global dashboard.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="EventId">The event identifier.</param>
        /// <param name="geonameIds">The geoname ids CSV.</param>
        /// <param name="diseasesIds">The diseases Ids CSV.</param>
        /// <param name="transmissionModesIds">The transmission modes Ids CSV.</param>
        /// <param name="interventionMethods">The intervention methods InterventionDisplayName's CSV.</param>
        /// <param name="severityRisks">The severity risks SeverityLevelDisplayName's CSV low, medium, high.</param>
        /// <param name="biosecurityRisks">The biosecurity risks BiosecurityRiskCode's A, B, C, D, No CSV.</param>
        /// <returns>EventsInfoViewModel</returns>
        public HttpResponseMessage Get(
            string userId, 
            int EventId = -1, 
            string geonameIds = "", 
            string diseasesIds = "", 
            string transmissionModesIds = "",
            string interventionMethods = "", 
            bool locationOnly = false,
            string severityRisks = "", 
            string biosecurityRisks = "")
        {
            try
            {
                var eventsInfoViewModel = new EventsInfoViewModel(
                    userId, 
                    EventId, 
                    geonameIds ?? "",
                    diseasesIds ?? "", 
                    transmissionModesIds ?? "", 
                    interventionMethods ?? "",
                    locationOnly,
                    severityRisks ?? "", 
                    biosecurityRisks ?? ""); ;

                Logger.Info("Successfully returned data for the dashboard");
                return Request.CreateResponse(HttpStatusCode.OK, eventsInfoViewModel);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to return data for the dashboard", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}