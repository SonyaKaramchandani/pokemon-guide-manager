using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.EntityModels.Zebra;
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
    /// This API is to get Insights event destination grids.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEventDestinationGridV3Controller : ApiController
    {
        // GET api/values
        /// <summary>Get event destination grids.</summary>
        /// <param name="EventId"></param>
        /// <param name="GridId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IEnumerable<usp_ZebraDataServicesGetEventDestinationGrid_Result> Get(int EventId = 0, string GridId = "")
        {
                BiodZebraEntities zebraDbContext = new BiodZebraEntities();
                zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

                return zebraDbContext.usp_ZebraDataServicesGetEventDestinationGrid(EventId, GridId);
        }
    }
}