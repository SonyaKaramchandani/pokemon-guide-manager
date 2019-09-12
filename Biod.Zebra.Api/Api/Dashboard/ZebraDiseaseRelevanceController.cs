using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using Biod.Zebra.Api.Api;
using System.Net;
using Newtonsoft.Json;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraDiseaseRelevanceController : BaseApiController
    {
        /// <summary>
        /// Return view model for preset query's disease relevance.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="groupType">The disease group identifier.</param>
        /// <returns>DiseaseRelevanceViewModel</returns>
        public HttpResponseMessage Get(string userId = "", string roleId = "", int groupType = 1)
        {
            try
            {
                var diseaseRelevanceViewModel = new DiseaseRelevanceViewModel(
                    userId ?? "", 
                    roleId ?? "",
                    groupType);

                Logger.Info("Successfully returned data for disease relevance");
                return Request.CreateResponse(HttpStatusCode.OK, diseaseRelevanceViewModel);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to return data for disease relevance", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Insert new disease relevance for preset query.
        /// </summary>
        /// <param name="inputJson">The userId, roleId, and disease relevance data.</param>
        /// <returns>String</returns>
        public HttpResponseMessage PostAsync(
            [FromBody] string inputJson)
        {
            try
            {
                //inputJson = [{ "diseaseId": 26, "relevanceId": 1, "stateId": 1 },{ "diseaseId": 55, "relevanceId": 1, "stateId": 1 }]
                var userId = "";
                var roleId = "";
                var diseaseRelevanceList = new List<DiseaseRelevanceModel>();
                var inputJsonObj = JsonConvert.DeserializeObject<DiseaseRelevanceInputJsonModel>(inputJson);
                userId = inputJsonObj.userId;
                roleId = inputJsonObj.roleId;
                diseaseRelevanceList = inputJsonObj.diseaseRelevanceJson;

                using (BiodZebraEntities zebraDbContext = new BiodZebraEntities()) {
                    zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

                    if (diseaseRelevanceList.Any()) {
                        if (!String.IsNullOrEmpty(userId))
                        {
                            var queryResult = zebraDbContext.Xtbl_User_Disease_Relevance.Where(x => x.UserId == userId);
                            if (queryResult.Any()) {
                                zebraDbContext.Xtbl_User_Disease_Relevance.RemoveRange(queryResult);
                            }
                            foreach (var dr in diseaseRelevanceList) {
                                var row = new Xtbl_User_Disease_Relevance() {
                                    UserId = userId,
                                    DiseaseId = dr.DiseaseId,
                                    RelevanceId = dr.RelevanceId,
                                    StateId = dr.StateId
                                };
                                zebraDbContext.Xtbl_User_Disease_Relevance.Add(row);
                            }
                        }
                        else if (!String.IsNullOrEmpty(roleId))
                        {
                            var queryResult = zebraDbContext.Xtbl_Role_Disease_Relevance.Where(x => x.RoleId == roleId);
                            if (queryResult.Any())
                            {
                                zebraDbContext.Xtbl_Role_Disease_Relevance.RemoveRange(queryResult);
                            }
                            foreach (var dr in diseaseRelevanceList)
                            {
                                var row = new Xtbl_Role_Disease_Relevance()
                                {
                                    RoleId = roleId,
                                    DiseaseId = dr.DiseaseId,
                                    RelevanceId = dr.RelevanceId,
                                    StateId = dr.StateId
                                };
                                zebraDbContext.Xtbl_Role_Disease_Relevance.Add(row);
                            }
                        }
                        zebraDbContext.SaveChanges();
                    }
                }

                Logger.Info("Successfully saved data for disease relevance");
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to save data for disease relevance", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}