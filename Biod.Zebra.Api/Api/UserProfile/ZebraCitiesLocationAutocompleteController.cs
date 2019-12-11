using System;
using Biod.Zebra.Api.Api;
using System.Net.Http;
using System.Net;
using Biod.Zebra.Library.Infrastructures.Geoname;

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
                var items = GeonameSearchHelper.SearchCityNames(DbContext, term);

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