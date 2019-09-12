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
using System.Data.Entity.Core.Objects;
using System.Net.Http;
using Biod.Zebra.Api.Api;
using System.Net;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraEventsFilterGroupSortController : BaseApiController
    {
        /// <summary>Get sorted and grouped filtered events.</summary>
        /// <param name="userId"></param>
        /// <param name="geonameIds"></param>
        /// <param name="diseasesIds"></param>
        /// <param name="transmissionModesIds"></param>
        /// <param name="prevensionMethods"></param>
        /// <param name="severityRisks"></param>
        /// <param name="biosecurityRisks"></param>
        /// <param name="groupType"></param>
        /// <param name="sortType"></param>
        /// <returns>string</returns>
        public HttpResponseMessage Get(
            string userId,
            string geonameIds = "",
            string diseasesIds = "",
            string transmissionModesIds = "",
            string prevensionMethods = "",
            string severityRisks = "",
            string biosecurityRisks = "",
            bool locationOnly = false,
            int groupType = 1,
            string sortType = "LastUpdatedDate")
        {
            try
            {
                var result = new EventsInfoViewModel().FilterGroupSort(
                    userId,
                    geonameIds ?? "",
                    diseasesIds ?? "",
                    transmissionModesIds ?? "",
                    prevensionMethods ?? "",
                    severityRisks ?? "",
                    biosecurityRisks ?? "",
                    locationOnly,
                    groupType,
                    sortType);

                Logger.Info("Successfully returned zebra events for the given query");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to return zebra events for the given query", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}