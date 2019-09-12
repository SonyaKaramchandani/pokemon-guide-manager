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
    public class ZebraCitiesLocationAutocompleteController : BaseApiController
    {
        /// <summary>Gets local feed locations.</summary>
        /// <returns>List of LocationKeyValuePairModel.</returns>
        public HttpResponseMessage Get(string term)
        {
            try
            {
                var items = DbContext.usp_GetGeonameCities(term)
                    .Where(item => item.DisplayName.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    .Select(x => new LocationKeyValueAndTypePairModel
                    {
                        key = x.GeonameId,
                        value = x.DisplayName,
                        type = "City"
                    })
                    .ToList();

                Logger.Info($"Successfully returned auto-complete location list for query term {term}");
                return Request.CreateResponse(HttpStatusCode.OK, items);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return auto-complete location list for query term {term}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
}