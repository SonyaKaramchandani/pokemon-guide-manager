using Biod.Zebra.Library.EntityModels;
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

namespace Biod.Zebra.Api.Generic
{
    /// <summary>
    /// This API is to get Zebra event by geoname city Id.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEventInfoController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Get specific event infromation by geoname city Id.
        /// </summary>
        /// <param name="GeonameId">The geoname identifier.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IEnumerable<usp_ZebraApiGetEventByGeonameId_Result> Get(int GeonameId)
        {
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            return zebraDbContext.usp_ZebraApiGetEventByGeonameId(GeonameId, Convert.ToInt32(ConfigurationManager.AppSettings.Get("EventDistanceBuffer")));
        }
    }
}