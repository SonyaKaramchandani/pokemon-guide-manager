using Biod.Zebra.Library.EntityModels;
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

namespace Biod.Zebra.Api.Hcw
{
    /// <summary>
    /// This API call gives a list of disease symptom association scores.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HcwGetDiseaseSymptomScoreController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Find symptoms association scores.
        /// </summary>
        /// <param name="DiseaseIds">Comma-separated diseases identifiers.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IEnumerable<HcwDiseaseSymptomScoreModel> Get(string DiseaseIds)
        {
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            //get diseases symtoms score
            var diseaseSymptomScore = zebraDbContext.usp_HcwGetDiseaseSymptomScore(DiseaseIds).ToList();
            //loop each group
            var result = (from r in diseaseSymptomScore
                          group r by r.DiseaseId into g
                          select new HcwDiseaseSymptomScoreModel()
                          {
                              DiseaseId = g.Key,
                              SymptomScore = (from d in diseaseSymptomScore
                                              where d.DiseaseId == g.Key
                                              select new HcwSymptomScoreModel()
                                              {
                                                  SymptomId = d.SymptomId,
                                                  Symptom = d.Symptom,
                                                  AssociationScore = d.AssociationScore.Value
                                              }
                                             ).ToList()
                          }
                         );
            return result;

        }
    }
}