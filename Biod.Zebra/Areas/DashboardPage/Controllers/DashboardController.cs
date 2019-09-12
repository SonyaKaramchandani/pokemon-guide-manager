﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels;
using System.Configuration;
using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Biod.Zebra.Library.Infrastructures;
using Newtonsoft.Json;
using static AuthorizeByConfig;
using Biod.Zebra.Library.Infrastructures.Log;
using Biod.Zebra.Controllers;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        // GET: Dashboard/Index

        /// <summary>
        /// Index the specified event identifier.
        /// </summary>
        /// <param name="EventId">The event identifier.</param>
        /// <param name="geonameIds">The geoname ids CSV.</param>
        /// <param name="diseasesIds">The diseases Ids CSV.</param>
        /// <param name="transmissionModesIds">The transmission modes Ids CSV.</param>
        /// <param name="prevensionMethods">The prevension methods PreventionDisplayName's CSV.</param>
        /// <param name="severityRisks">The severity risks SeverityLevelDisplayName's CSV low, medium, high.</param>
        /// <param name="biosecurityRisks">The biosecurity risks BiosecurityRiskCode's A, B, C, D, No CSV.</param>
        /// <returns></returns>
        public ActionResult Index(int EventId = 0, string geonameIds = "", string diseasesIds = "", string transmissionModesIds = "",
            string prevensionMethods = "", string severityRisks = "", string biosecurityRisks = "")
        {
            ViewBag.Message = "Zebra dashboard page.";
            var userId = User.Identity.GetUserId();
            var queryString = "userId=" + userId + "&EventId=" + EventId + "&geonameIds=" + geonameIds + "&diseasesIds=" + diseasesIds +
                    "&transmissionModesIds=" + transmissionModesIds + "&prevensionMethods=" + prevensionMethods + "&severityRisks=" + severityRisks +
                    "&biosecurityRisks=" + biosecurityRisks;

            var result = JsonStringResultClass.GetJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraInitDashboard?" + queryString,
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            EventsInfoViewModel eventsInfoViewModel = JsonConvert.DeserializeObject<EventsInfoViewModel>(result);

            Logger.Info("User navigated to dashboard");
            return View(eventsInfoViewModel);
        }

        /// <summary>Gets the event detail partial view.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="geonames">The geonames.</param>
        /// <returns></returns>
        //[AuthorizeByConfig(RolesAppSettingKey = "PaidUsersRole")]
        //[DoNotAuthorize(Roles = "PaidUsers")]
        //[Authorize(Roles = "PaidUsers")]
        public ActionResult GetEventDetailPartialView(int eventId, string geonames)
        {
            var queryString = "userId=" + User.Identity.GetUserId() + "&eventId=" + eventId + "&geonames=" + geonames;

            var result = JsonStringResultClass.GetJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraEventDetail?" + queryString,
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            EventDetailViewModel eventDetailViewModel = JsonConvert.DeserializeObject<EventDetailViewModel>(result);

            Logger.Info($"Loaded event details partial view for event ID {eventId}");
            return PartialView("_EventDetailPanel", eventDetailViewModel);
        }
        
        //[AuthorizeByConfig(RolesAppSettingKey = "PaidUsersRole")]
        //[DoNotAuthorize(Roles = "PaidUsers")]
        //[Authorize(Roles = "PaidUsers")]
        public ActionResult GetEventCasePartialView(string geonameIds = "", string diseasesIds = "", string transmissionModesIds = "",
            string prevensionMethods = "", string severityRisks = "", string biosecurityRisks = "", bool locationOnly = false, bool customEvents = false, int groupType = 1, string sortType = "LastUpdatedDate")
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraUserProfile?userId=" + User.Identity.GetUserId(),
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;
            UserProfileDto userProfile = JsonConvert.DeserializeObject<UserProfileDto>(result);

            // Restrict Filter privileges if the user is not in the Paid User Role
            if (!User.IsInRole(ConfigurationManager.AppSettings.Get("PaidUsersRole"))
                && !customEvents
                && (!userProfile.UserNotification.AoiGeonameIds.Equals(geonameIds)
                    || !userProfile.UserNotification.DiseaseIds.Equals(diseasesIds)
                    || !string.IsNullOrEmpty(transmissionModesIds)
                    || !string.IsNullOrEmpty(prevensionMethods)
                    || !string.IsNullOrEmpty(severityRisks)
                    || !string.IsNullOrEmpty(biosecurityRisks)))
            {
                Logger.Warning($"Unpaid user requested event list with non-default filters, redirecting to error page");
                return PartialView("~/Views/Shared/Error.cshtml");
            }

            if (customEvents)
            {
                var queryString = "userId=" + User.Identity.GetUserId() + "&groupType=" + groupType + "&sortType=" + sortType;
                result = JsonStringResultClass.GetJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraEventsCustomEvents?" + queryString,
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;
            }
            else
            {
                var queryString = "userId=" + User.Identity.GetUserId() + "&geonameIds=" + geonameIds + "&diseasesIds=" + diseasesIds + "&transmissionModesIds=" + transmissionModesIds +
                                  "&prevensionMethods=" + prevensionMethods + "&severityRisks=" + severityRisks + "&biosecurityRisks=" + biosecurityRisks +
                                  "&groupType=" + groupType + "&sortType=" + sortType + "&locationOnly=" + locationOnly;
                result = JsonStringResultClass.GetJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraEventsFilterGroupSort?" + queryString,
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;
            }

            EventsInfoViewModel eventsInfoViewModel = JsonConvert.DeserializeObject<EventsInfoViewModel>(result);

            Logger.Info($"Loaded event list partial view");
            return PartialView("_EventCasePanel", eventsInfoViewModel);
        }

        public ActionResult GetCountryShapeAsText(int GeonameId)
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraCountryShapeAsText?GeonameId=" + GeonameId,
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            if (result[0] == '"' && result[result.Length - 1] == '"') {
                result = result.Substring(1, result.Length - 2);
            }
            
            return Content(result);
        }

        public ActionResult GetEventLocations(int EventId, string LocationType)
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraGetEventLocations?EventId=" + EventId + "&LocationType=" + LocationType,
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            var toReturn = JsonConvert.DeserializeObject<List<usp_ZebraApiGetEventLocationShapesByEventid_Result>>(result);

            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDestinationAirports(int EventId, string GeonameIds = "")
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraDestinationAirports?EventId=" + EventId + "&GeonameIds=" + GeonameIds ?? "",
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            var toReturn = JsonConvert.DeserializeObject<List<usp_ZebraEventGetDestinationAirportsByEventId_Result>>(result);
            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocalEventIds()
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraLocalEventIds?userId=" + User.Identity.GetUserId(),
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            var zebraEventsInfo = JsonConvert.DeserializeObject<List<int>>(result);

            return Json(zebraEventsInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocationAutocomplete(string term)
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraLocalFeedLocationAutocomplete?term=" + term,
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;
            List<LocationKeyValueAndTypePairModel> filteredItems = JsonConvert.DeserializeObject<List<LocationKeyValueAndTypePairModel>>(result);
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGeonamesByGeonameIds(string geonameIds)
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraGeonamesByGeonameIds?geonameIds=" + geonameIds ?? "",
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            var geonameItems = JsonConvert.DeserializeObject<List<usp_SearchGeonamesByGeonameIds_Result>>(result);

            return Json(geonameItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveAoiGeonameIds(string geonames)
        {
            var gmObj = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<GeonameModel>>(geonames);
            var geonameIds = string.Join(",", gmObj.Select(g => g.GeonameId));
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraSaveAoiGeonameIds?userId=" + User.Identity.GetUserId() + "&geonameIds=" + geonameIds,
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            var customIdentityResult = JsonConvert.DeserializeObject<CustomIdentityResult>(result);
            if (customIdentityResult.Succeeded)
            {
                return Json(new
                {
                    Success = true,
                    AoiGeonameIds = geonameIds,
                    AoiGeonames = gmObj
                }, JsonRequestBehavior.AllowGet);
            }

            var errMsg = customIdentityResult.Errors.First();
            return Json(new { Success = false, Message = errMsg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateOnboardingStatus()
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraOnboarding?userId=" + User.Identity.GetUserId() + "&isComplete=true",
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

            var customIdentityResult = JsonConvert.DeserializeObject<CustomIdentityResult>(result);
            if (!customIdentityResult.Succeeded)
            {
                var errMsg = customIdentityResult.Errors.First();
                return Json(new { Success = false, Message = errMsg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DownloadPDF(string fileName)
        {
            string path = Server.MapPath(String.Format("~/App_Data/{0}" + ".PDF", fileName));
            if (System.IO.File.Exists(path))
            {
                return File(path, "application/pdf", fileName);
            }
            return HttpNotFound();
        }

    }
}