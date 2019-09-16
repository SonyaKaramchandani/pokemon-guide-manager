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

namespace Biod.Zebra.Api.Hcw
{
    /// <summary>
    /// This API call gives a list of diseases by incubation period.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HcwGetDiseaseByIncubationController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Find diseases by incubation period.
        /// </summary>
        /// <param name="DiseaseIds">Comma-separated the diseases identifiers.</param>
        /// <param name="UserReturnDate">Date patient returned from travel. Format: yyyy-mm-dd.</param>
        /// <param name="LengthOfStay">Days patient had stayed in travel location.</param>
        /// <param name="OnsetOfSymptomsDays">Days patient has been felling ill.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public List<usp_HcwGetDiseaseByIncubation_Result> Get(string DiseaseIds, DateTime UserReturnDate, int LengthOfStay, int OnsetOfSymptomsDays)
        {
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            //get diseases symtoms score
            var result = zebraDbContext.usp_HcwGetDiseaseByIncubation(DiseaseIds, UserReturnDate, LengthOfStay, OnsetOfSymptomsDays).ToList();
            return result;
        }
    }
}