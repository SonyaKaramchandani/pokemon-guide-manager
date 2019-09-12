using BdDataApi.EntityModels;
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

namespace Biod.Zebra.Api
{
    /// <summary>
    /// This API is to get Zebra geonames cities for events.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraGeonameCitiesController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Get the cities start with combination of letters.
        /// </summary>
        /// <param name="CityName">Name of the city.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IEnumerable<usp_GetGeonameCities_Result> Get(string CityName)
        {
            ZebraEntities zebraDbContext = new ZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            return zebraDbContext.usp_GetGeonameCities(CityName);
        }
    }
}