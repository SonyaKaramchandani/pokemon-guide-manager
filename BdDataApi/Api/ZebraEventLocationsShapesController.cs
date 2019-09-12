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
    /// This API is to get Zebra events shapes as text by event identifier.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEventLocationsShapesController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Get all published events.
        /// </summary>
        /// <param name="EventId">The event identifier.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IEnumerable<usp_GetZebraEventLocationsShapesByEventId_Result> Get(int EventId)
        {
            ZebraEntities zebraDbContext = new ZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            return zebraDbContext.usp_GetZebraEventLocationsShapesByEventId(EventId);
        }
    }
}