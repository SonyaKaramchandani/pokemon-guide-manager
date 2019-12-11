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
    public class ZebraEventDetailController : BaseApiController
    {
        /// <summary>Initialize the global dashboard.</summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="geonames"></param>
        /// <returns>EventsInfoViewModel</returns>
        public HttpResponseMessage Get(string userId, int eventId, string geonames, string diseasesIds = "", string transmissionModesIds = "", string interventionMethods = "", bool locationOnly = false, string severityRisks = "", string biosecurityRisks = "")
        {
            try
            {
                var gmObj = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<GeonameModel>>(geonames);
                var eventDetailViewModel = new EventDetailViewModel(userId, eventId, gmObj, new FilterParamsModel(
                    string.Join(",", gmObj.Select(x => x.GeonameId)),
                    diseasesIds,
                    transmissionModesIds,
                    interventionMethods,
                    locationOnly,
                    severityRisks,
                    biosecurityRisks)
                );

                Logger.Info($"Successfully returned event detail for user ID {userId}, event ID {eventId}, and geonames {geonames}");
                return Request.CreateResponse(HttpStatusCode.OK, eventDetailViewModel);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get event detail for user ID {userId}, event ID {eventId}, and geonames {geonames}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}