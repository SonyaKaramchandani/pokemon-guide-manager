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
    /// This API call gives a list of events that if I live in that city, I would receive a notification about.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEventRiskController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Find a list of events that if I live in that city, I would receive a notification about.
        /// </summary>
        /// <param name="GeonameId">The geoname identifier.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public List<usp_GetZebraEventInfoByGeonameId_Result> Get(int GeonameId)
        {
            ZebraEntities zebraDbContext = new ZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            ////get all articles related to the event
            //List<usp_GetZebraEventArticlesByEventId_Result> eventArticles = zebraDbContext.usp_GetZebraEventArticlesByEventId(EventId).ToList();
            //get all users emails with their event information
            return zebraDbContext.usp_GetZebraEventInfoByGeonameId(GeonameId).ToList();
        }
    }
}