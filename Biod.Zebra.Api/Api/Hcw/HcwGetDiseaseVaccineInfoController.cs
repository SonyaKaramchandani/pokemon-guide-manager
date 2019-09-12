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
    /// This API call gives a list of disease with vaccine info.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HcwGetDiseaseVaccineInfoController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Find diseases vaccination information.
        /// </summary>
        /// <param name="DiseaseIds">Comma-separated the diseases identifiers.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public List<usp_HcwGetDiseaseVaccineInfo_Result> Get(string DiseaseIds)
        {
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            //get diseases symtoms score
            var result = zebraDbContext.usp_HcwGetDiseaseVaccineInfo(DiseaseIds).ToList();
            return result;
        }
    }
}
