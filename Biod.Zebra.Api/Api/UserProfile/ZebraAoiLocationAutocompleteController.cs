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
    public class ZebraAoiLocationAutocompleteController : BaseApiController
    {
        /// <summary>Gets local feed locations.</summary>
        /// <returns>List of LocationKeyValueAndTypePairModel.</returns>
        public HttpResponseMessage Get(string aoiGeonameIds)
        {    
            try
            {
                var items = DbContext.usp_SearchGeonamesByGeonameIds(aoiGeonameIds)
                .Select(x => new LocationKeyValueAndTypePairModel
                {
                    key = x.GeonameId,
                    value = x.DisplayName,
                    type = x.LocationType
                })
                .ToList();

                Logger.Info($"Successfully returned auto-complete location list for query {aoiGeonameIds}");
                return Request.CreateResponse(HttpStatusCode.OK, items);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return auto-complete location list for query {aoiGeonameIds}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}