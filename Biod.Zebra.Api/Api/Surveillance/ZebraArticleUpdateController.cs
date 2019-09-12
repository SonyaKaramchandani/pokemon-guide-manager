﻿using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;

namespace Biod.Zebra.Api.Api.Surveillance
{
    [AllowAnonymous]
    public class ZebraArticleUpdateController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody] ArticleUpdateForZebra input)
        {
            IHttpActionResult toReturnAction;

            try
            {
                if (!string.IsNullOrEmpty(input.ArticleId))
                {
                    var curId = input.ArticleId;
                    var curArticle = DbContext.ProcessedArticles
                        .Include(pa => pa.Events)
                        .Include(pa => pa.Xtbl_Article_Location_Disease)
                        .SingleOrDefault(s => s.ArticleId == curId);

                    if (curArticle == null)//for a new article
                    {
                        //insert article
                        var r = new ProcessedArticle();

                        r = AssignArticle(r, input, true);

                        DbContext.ProcessedArticles.Add(r);
                        DbContext.SaveChanges();

                        //response = "success";
                        Logger.Info($"Successfully created article with id { curId }");
                        toReturnAction = Ok("Success! Article " + r.ArticleId + " has been inserted");
                    }
                    else // for an existing article
                    {
                        //Clear article items
                        curArticle.Xtbl_Article_Location_Disease.Clear();
                        curArticle.Events.Clear();

                        curArticle = AssignArticle(curArticle, input, false);

                        DbContext.SaveChanges();
                        //response = "success";
                        Logger.Info($"Successfully updated existing article with id { curId }");
                        toReturnAction = Ok("Success! Article " + curArticle.ArticleId + " has been updated");
                    }
                }
                else
                {
                    Logger.Warning("Failed to update article: the ArticleId was null");
                    toReturnAction = BadRequest("Error: ArticleId cannot be null");
                    //response = "Error: ArticleId cannot be null";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to update article: " + ex.Message);
                toReturnAction = BadRequest("Error: " + ex.Message);
                //response = "failed";
            }

            return toReturnAction;
        }

        private ProcessedArticle AssignArticle(ProcessedArticle artObj, ArticleUpdateForZebra artm, Boolean isInsert)
        {
            artObj.ArticleId = artm.ArticleId;
            artObj.ArticleTitle = artm.ArticleTitle;
            artObj.ArticleFeedId = artm.ArticleFeedId;
            artObj.FeedURL = artm.FeedURL;
            artObj.FeedSourceId = artm.FeedSourceId;
            artObj.FeedPublishedDate = artm.FeedPublishedDate;
            artObj.HamTypeId = artm.HamTypeId;
            artObj.OriginalSourceURL = artm.OriginalSourceURL;
            artObj.IsCompleted = artm.IsCompleted;
            artObj.SimilarClusterId = artm.SimilarClusterId;
            artObj.OriginalLanguage = artm.OriginalLanguage;
            artObj.UserLastModifiedDate = artm.UserLastModifiedDate;
            artObj.LastUpdatedByUserName = artm.LastUpdatedByUserName;
            artObj.Notes = artm.Notes;
            artObj.ArticleBody = artm.ArticleBody;
            artObj.IsRead = artm.IsRead;
            artObj.SystemLastModifiedDate = artm.SystemLastModifiedDate;

            //insert or update the association with event
            if (artm.SelectedPublishedEventIds != null)
            {
                var eventIds = new HashSet<int>(artm.SelectedPublishedEventIds);
                DbContext.Events.Where(e => eventIds.Contains(e.EventId)).ForEach(e =>
                {
                    artObj.Events.Add(e);
                });
            }

            if (!String.IsNullOrEmpty(artm.DiseaseObject))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var disArr = js.Deserialize<ArticleLocationDisease[]>(artm.DiseaseObject);

                foreach (var disItem in disArr)
                {
                    var ald = new Xtbl_Article_Location_Disease();
                    ald.ArticleId = artm.ArticleId;
                    ald.DiseaseId = disItem.DiseaseId;
                    ald.LocationGeoNameId = disItem.LocationId;
                    ald.NewSuspectedCount = disItem.NewSuspectedCount;
                    ald.NewConfirmedCount = disItem.NewConfirmedCount;
                    ald.NewReportedCount = disItem.NewReportedCount;
                    ald.NewDeathCount = disItem.NewDeathCount;
                    ald.TotalSuspectedCount = disItem.TotalSuspectedCount;
                    ald.TotalConfirmedCount = disItem.TotalConfirmedCount;
                    ald.TotalReportedCount = disItem.TotalReportedCount;
                    ald.TotalDeathCount = disItem.TotalDeathCount;

                    artObj.Xtbl_Article_Location_Disease.Add(ald);
                }
            }

            return artObj;
        }

    }
}