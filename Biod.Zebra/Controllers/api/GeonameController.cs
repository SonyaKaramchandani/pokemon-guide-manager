using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using Biod.Zebra.Library.Infrastructures.Authentication;
using Biod.Zebra.Library.Infrastructures.Geoname;

namespace Biod.Zebra.Controllers.api
{
    [TokenAuthentication]
    [RoutePrefix("api/geoname")]
    public class GeonameController : BaseApiController
    {
        [Route("city")]
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage SearchCityName([QueryString] string term)
        {
            try
            {
                var results = GeonameSearchHelper.SearchCityNames(DbContext, term);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to search city name with term '{term}': {ex.Message}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}