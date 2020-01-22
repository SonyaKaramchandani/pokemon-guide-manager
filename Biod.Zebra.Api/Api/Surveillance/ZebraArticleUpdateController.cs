using Biod.Zebra.Library.EntityModels;
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
using Biod.Zebra.Library.Infrastructures.Geoname;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models.Surveillance;

namespace Biod.Zebra.Api.Api.Surveillance
{
    [AllowAnonymous]
    public class ZebraArticleUpdateController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody] ArticleUpdateForZebra modifiedArticle)
        {
            IHttpActionResult result;

            try
            {
                if (!string.IsNullOrEmpty(modifiedArticle.ArticleId))
                {
                    var article = DbContext.ProcessedArticles
                        .Include(pa => pa.Events)
                        .SingleOrDefault(s => s.ArticleId == modifiedArticle.ArticleId);

                    if (article == null)//for a new article
                    {
                        //insert article
                        article = new ProcessedArticle();
                        DbContext.ProcessedArticles.Add(article);
                    }
                    else // for an existing article
                    {
                        //Clear article events
                        article.Events.Clear();
                    }

                    UpdateArticle(article, modifiedArticle);
                    DbContext.SaveChanges();

                    Logger.Info($"Successfully updated existing article with id { article.ArticleId }");
                    result = Ok("Success! Article " + article.ArticleId + " has been updated");
                }
                else
                {
                    Logger.Warning("Failed to update article: the ArticleId was null");
                    result = BadRequest("Error: ArticleId cannot be null");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to update article: " + ex.Message, ex);
                result = BadRequest("Error: " + ex.Message);
            }

            return result;
        }

        private void UpdateArticle(ProcessedArticle article, ArticleUpdateForZebra modifiedArticle)
        {
            article.ArticleId = modifiedArticle.ArticleId;
            article.ArticleTitle = modifiedArticle.ArticleTitle;
            article.ArticleFeedId = modifiedArticle.ArticleFeedId;
            article.FeedURL = modifiedArticle.FeedURL;
            article.FeedSourceId = modifiedArticle.FeedSourceId;
            article.FeedPublishedDate = modifiedArticle.FeedPublishedDate;
            article.HamTypeId = modifiedArticle.HamTypeId;
            article.OriginalSourceURL = modifiedArticle.OriginalSourceURL;
            article.IsCompleted = modifiedArticle.IsCompleted;
            article.SimilarClusterId = modifiedArticle.SimilarClusterId;
            article.OriginalLanguage = modifiedArticle.OriginalLanguage;
            article.UserLastModifiedDate = modifiedArticle.UserLastModifiedDate;
            article.LastUpdatedByUserName = modifiedArticle.LastUpdatedByUserName;
            article.Notes = modifiedArticle.Notes;
            article.ArticleBody = modifiedArticle.ArticleBody;
            article.IsRead = modifiedArticle.IsRead;
            article.SystemLastModifiedDate = modifiedArticle.SystemLastModifiedDate;
            article.ArticleFeedType = GetDisplayName(article.ArticleFeedId, article.OriginalSourceURL);
            article.SequenceId = GetSequenceId(article.ArticleFeedId, article.OriginalSourceURL);

            //insert or update the association with event
            if (modifiedArticle.SelectedPublishedEventIds != null)
            {
                var eventIds = new HashSet<int>(modifiedArticle.SelectedPublishedEventIds);
                DbContext.Events.Where(e => eventIds.Contains(e.EventId)).ForEach(e =>
                {
                    article.Events.Add(e);
                });
            }
        }

        private string GetDisplayName(int? feedId, string sourceUrl)
        {
            switch (feedId)
            {
                case 3 when sourceUrl.Contains("cdc.gov"):
                case 9 when sourceUrl.Contains("wwwnc.cdc.gov"):
                    return "CDC";
                case 9 when sourceUrl.Contains("ecdc.europa.eu"):
                    return "ECDC";
                case 9 when sourceUrl.Contains("chp.gov.hk"):
                    return "Other Official";
                case 3:
                case 9:
                    return "News Media";
                default:
                    return DbContext.ArticleFeeds.SingleOrDefault(f => f.ArticleFeedId == feedId)?.DisplayName ?? "";
            }
        }

        private int GetSequenceId(int? feedId, string sourceUrl)
        {
            switch (feedId)
            {
                case 3 when sourceUrl.Contains("cdc.gov"):
                case 9 when sourceUrl.Contains("wwwnc.cdc.gov"):
                    return 2;
                case 9:
                    return 6;
                case 3:
                    return 7;
                default:
                    return DbContext.ArticleFeeds.SingleOrDefault(f => f.ArticleFeedId == feedId)?.SeqId ?? 0;
            }
        }
    }
}
