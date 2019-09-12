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
    public class ZebraGeonamesByGeonameIdsController : BaseApiController
    {
        /// <summary>Get Geonames by Geoname identifiers.</summary>
        /// <param name="geonameIds"></param>
        /// <returns>EventsInfoViewModel</returns>
        public HttpResponseMessage Get(string geonameIds = "")
        {
            try
            {
                var result = DbContext.usp_SearchGeonamesByGeonameIds(geonameIds ?? "").ToList();

                Logger.Info($"Successfully returned geonames for geoname IDs {geonameIds}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return geonames by geoname IDs {geonameIds}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}