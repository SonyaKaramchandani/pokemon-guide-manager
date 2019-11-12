using Biod.Zebra.Library.EntityModels.George;
using Biod.Zebra.Library.EntityModels.Zebra;
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
    public class HcwGetDiseaseDetailInfoController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Find detailed disease information.
        /// </summary>
        /// <param name="DiseaseId">A disease identifier.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public HcwDiseaseDetailInfoModel Get(int DiseaseId)
        {
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            DiseasesAPIEntities georgeDiseaseContext = new DiseasesAPIEntities();
            georgeDiseaseContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            //get diseases intro
            string diseaseIntroduction = georgeDiseaseContext.usp_HcwGetDiseaseIntroduction(DiseaseId).FirstOrDefault();

            //get diseases info other than intro
            var diseaseDetailInfo = zebraDbContext.usp_HcwGetDiseaseDetailInfo(DiseaseId).ToList();
            //loop each group
            var result = (from d in diseaseDetailInfo
                          group d by new { d.DiseaseId, d.DiseaseName, d.Agents, d.AgentTypes, d.TransmissionMode, d.Incubation, d.Vaccination } into g
                          select new HcwDiseaseDetailInfoModel()
                          {
                              DiseaseId = g.Key.DiseaseId,
                              DiseaseName = g.Key.DiseaseName,
                              DiseaseIntroduction = diseaseIntroduction,
                              Agents = g.Key.Agents,
                              AgentTypes = g.Key.AgentTypes,
                              TransmissionMode = g.Key.TransmissionMode,
                              Incubation = g.Key.Incubation,
                              Vaccination = g.Key.Vaccination,
                              SymptomScore = (from dd in diseaseDetailInfo
                                              where dd.SymptomId != null
                                              select new HcwSymptomScoreModel()
                                              {
                                                  SymptomId = dd.SymptomId.Value,
                                                  Symptom = dd.Symptom,
                                                  AssociationScore = dd.AssociationScore.Value
                                              }
                                             ).ToList()
                          }
                         ).FirstOrDefault();
            return result;

        }
    }
}