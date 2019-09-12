using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Westwind.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;

namespace Biod.Zebra.Api.Hcw
{
    /// <summary>
    /// This API call gives next disease symptom and updated association scores.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HcwGetRiskWithTierController : ApiController
    {
        // GET api/values
        // Note: for testing, Toronto is at lat=43.7, long=-79.4
        /// <summary>
        /// Get the disease risks for an array of places given their latitudes and longitudes and the tier of disease.
        /// </summary>
        /// <param name="latitude">One or more latitudes between -90 and 90 degrees.</param>
        /// <param name="longitude">One or more longitudes between -360 and 360 degrees.</param>
        /// <param name="tier">Disease tier: 1-High, 2-Intermediate, 3-Low</param>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IHttpActionResult Get([FromUri] double[] latitude, [FromUri] double[] longitude, [FromUri] int tier, string context = "web")
        {
            if (latitude.Length != longitude.Length)
                return BadRequest("Number of latitude and longitude parameters must match.");

            var querystring = string.Empty;
            for (int i = 0; i < latitude.Length; i++)
            {
                querystring += $"latitude={latitude[i]}&longitude={longitude[i]}" + ((latitude.Length >= i) ? $"&tier={tier}&context={context}" : "&");
            }

            var jsonStringResultAsyncString = JsonStringResultClass.GetJsonStringResultAsync(
                                                ConfigurationManager.AppSettings.Get("GeorgeApiBaseUrl"),
                                                "/location/riskswithtier?" + querystring,
                                                ConfigurationManager.AppSettings.Get(@"GeorgeApiUserName"),
                                                ConfigurationManager.AppSettings.Get("GeorgeApiPassword")).Result;
            return Ok(jsonStringResultAsyncString);
        }
    }
}