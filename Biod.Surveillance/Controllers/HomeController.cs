using Biod.Surveillance.Infrastructures;
using Biod.Surveillance.ViewModels;
using Biod.Zebra.Library.EntityModels.Surveillance;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Surveillance;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SignalRSurveillance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Biod.Surveillance.Controllers
{
    [System.Web.Mvc.Authorize]
    public class HomeController : Controller
    {
        protected BiodSurveillanceDataEntities dbContext;

        public HomeController(BiodSurveillanceDataEntities context)
        {
            dbContext = context;
            dbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
        }

        public HomeController() : this(new BiodSurveillanceDataEntities())
        {
        }

        public ActionResult Index()
        {
            SurveillanceViewModel viewModel = new SurveillanceViewModel
            {
                ArticleCounts = ArticleCount.GetAllArticleCount(),
                EventList = EventViewModelHelper.GetAllEvents(dbContext),
                SuggestEventList = EventViewModelHelper.GetSuggestedEvents(dbContext)
            };

            return View(viewModel);
        }

        public ActionResult Chat()
        {
            ViewBag.Message = "Surveillance tool chatting page.";
            return View();
        }

        #region Processed Articles
        public ActionResult GetInitialArticleList(string id, string viewName)
        {
            var model = new ArticleByFilterView
            {
                HamTypes = ArticleByFilterView.GetHamType(dbContext),
                LocationList = ArticleByFilterView.GetLocationLists(null),
                ArticleFeeds = ArticleByFilterView.GetArticleFeeds(null),
                ArticleDiseases = ArticleByFilterView.GetDiseases(null)
            };

            return PartialView(viewName, model);
        }

        /// <summary>
        /// Serializes recent processed parent articles that match the specified filter 
        /// 
        /// An article is said to be recent if the publish date is not older than:
        /// - Spam: 1 month
        /// - Disease information: 1 year
        /// - Disease activity: 1 year
        /// </summary>
        /// <param name="ID">string filter for articles that can either be <c>all</c> or <c>unprocessed</c>. Otherwise, the parent articles are obtained from spam articles.</param>
        /// <returns>Serialized list of parent articles (<c>ArticleGridWithSimilarCluster</c>)</returns>
        public JsonResult GetAllProcessedArticles(string ID)
        {
            var old = DateTime.Now.AddMonths(-12); // 1 year old

            List<ArticleGridWithSimilarCluster> finalArticleList = new List<ArticleGridWithSimilarCluster>();
            List<IGrouping<decimal?, SurveillanceProcessedArticle>> articleGroupResult = new List<IGrouping<decimal?, SurveillanceProcessedArticle>>();

            if (ID == "all")
            {
                articleGroupResult = (from r in dbContext.SurveillanceProcessedArticles
                                      where r.HamTypeId != 1 &&
                                               (r.FeedPublishedDate >= old)
                                      group r by r.SimilarClusterId).ToList();
            }
            else if (ID == "unprocessed")
            {
                articleGroupResult = (from r in dbContext.SurveillanceProcessedArticles
                                      where r.HamTypeId != 1 &&
                                            (r.IsCompleted == null || r.IsCompleted == false) &&
                                            (r.FeedPublishedDate >= old)
                                      group r by r.SimilarClusterId).ToList();
            }
            else  // spam
            { 
                var spamArticles = ArticleCount.SpamArticleList();
                articleGroupResult = (from r in spamArticles
                                      group r by r.SimilarClusterId).ToList();
            }
            finalArticleList = GetParentArticles(articleGroupResult);

            return Json(finalArticleList);
        }

        /// <summary>
        /// Handles POST request for retrieving list of recent processed parent articles, filtered by the specified form. 
        /// Possible filters include:
        /// - disease ID
        /// - geoname ID
        /// - article feed source
        /// - publish date
        /// - ham type (defaults to non-spam if unspecified)
        /// - article type: all, unprocessed, spam
        /// 
        /// An article is said to be recent if the publish date is not older than:
        /// - Spam: 1 month
        /// - Disease information: 1 year
        /// - Disease activity: 1 year
        /// </summary>
        /// <param name="form">form for filtering articles to be returned</param>
        /// <returns>HTTP response containing the list of parent articles</returns>
        [HttpPost]
        public JsonResult GetFilteredArticle(FormCollection form)
        {
            List<SurveillanceProcessedArticle> filteredArticleList = new List<SurveillanceProcessedArticle>();
            var result = new JsonResult();

            var articleTypeId = form["SelectedArticleTypeID"];
            var startDt = form["txtStartDateField"] != "" ? form["txtStartDateField"] : null;
            var startDate = (startDt != null) ? DateTime.Parse(startDt) : (DateTime?)null;

            var endDt = form["txtEndDateField"] != "" ? form["txtEndDateField"] : null;
            var endDate = (endDt != null) ? DateTime.Parse(endDt) : (DateTime?)null;

            var hamIdsCSV = (form["ddlHamType"] == "0") ? "2,3" : form["ddlHamType"];
            var sourceFeedIdsCSV = string.IsNullOrEmpty(form["llbSourceList"]) ? "" : form["llbSourceList"]; //multiselect sends null when empty
            var diseaseIdsCSV = string.IsNullOrEmpty(form["llbDiseaseList"]) ? "" : form["llbDiseaseList"]; //multiselect sends null when empty
            var locationIdsCSV = form["SelectedLocationIds"]; //autocomplete sends "" empty string when there is no data

            IEnumerable<int> diseaseIds = diseaseIdsCSV.Split(',').Select(str => int.Parse(str));
            IEnumerable<int> locationIds = locationIdsCSV.Split(',').Select(str => int.Parse(str));

            if (diseaseIdsCSV != "" || locationIdsCSV != "")
            {
                IEnumerable<SurveillanceProcessedArticle> articleProcessed = null;

                if (diseaseIdsCSV != "" && locationIdsCSV != "")
                {
                    articleProcessed = (from r in dbContext.SurveillanceXtbl_Article_Location_Disease
                                        where diseaseIds.Contains(r.DiseaseId) &&
                                           locationIds.Contains(r.LocationGeoNameId)
                                        select r.ProcessedArticle).Distinct();
                }
                else if (diseaseIdsCSV != "" && locationIdsCSV == "")
                {

                    articleProcessed = (from r in dbContext.SurveillanceXtbl_Article_Location_Disease
                                        where diseaseIds.Contains(r.DiseaseId)
                                        select r.ProcessedArticle).Distinct();
                }
                else if (diseaseIdsCSV == "" && locationIdsCSV != "")
                {
                    articleProcessed = (from r in dbContext.SurveillanceXtbl_Article_Location_Disease
                                        where locationIds.Contains(r.LocationGeoNameId)
                                        select r.ProcessedArticle).Distinct();
                }
                else
                {
                    articleProcessed = (from r in dbContext.SurveillanceXtbl_Article_Location_Disease
                                        select r.ProcessedArticle).Distinct();
                }

                IEnumerable<SurveillanceProcessedArticle> query1 = articleProcessed;

                if (startDate != null && endDate != null && sourceFeedIdsCSV != "")
                {
                    query1 = query1.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && sourceFeedIdsCSV.Contains(r.ArticleFeedId.ToString()) &&
                                     (r.FeedPublishedDate >= startDate && r.FeedPublishedDate <= endDate));
                }
                else if (startDate != null && endDate == null && sourceFeedIdsCSV != "")
                {
                    query1 = query1.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && sourceFeedIdsCSV.Contains(r.ArticleFeedId.ToString()) &&
                                     r.FeedPublishedDate >= startDate);
                }
                else if (startDate == null && endDate != null && sourceFeedIdsCSV != "")
                {
                    query1 = query1.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && sourceFeedIdsCSV.Contains(r.ArticleFeedId.ToString()) &&
                                     r.FeedPublishedDate <= endDate);
                }
                else if (startDate != null && endDate == null && sourceFeedIdsCSV == "")
                {
                    query1 = query1.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) &&
                                    r.FeedPublishedDate >= startDate);
                }
                else if (startDate == null && endDate != null && sourceFeedIdsCSV == "")
                {
                    query1 = query1.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) &&
                                    r.FeedPublishedDate <= endDate);
                }
                else if (startDate == null && endDate == null && sourceFeedIdsCSV != "")
                {
                    query1 = query1.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && sourceFeedIdsCSV.Contains(r.ArticleFeedId.ToString()));
                }
                else if (startDate != null && endDate != null && sourceFeedIdsCSV == "")
                {
                    query1 = query1.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && (r.FeedPublishedDate >= startDate && r.FeedPublishedDate <= endDate));
                }
                else
                {
                    query1 = query1.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()));
                }
                filteredArticleList = (query1).ToList();
            }
            else
            {
                //only query ProcessedArticle table since there is no input for Disease and Location
                // Creating dynamic Where clause for quering db.ProcessedArticles
                var query = from r in dbContext.SurveillanceProcessedArticles select r;

                if (startDate != null && endDate != null && sourceFeedIdsCSV != "")
                {

                    query = query.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && sourceFeedIdsCSV.Contains(r.ArticleFeedId.ToString()) &&
                                     (r.FeedPublishedDate >= startDate && r.FeedPublishedDate <= endDate));
                }
                else if (startDate != null && endDate == null && sourceFeedIdsCSV != "")
                {
                    query = query.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && sourceFeedIdsCSV.Contains(r.ArticleFeedId.ToString()) &&
                                     r.FeedPublishedDate >= startDate);
                }
                else if (startDate == null && endDate != null && sourceFeedIdsCSV != "")
                {
                    query = query.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && sourceFeedIdsCSV.Contains(r.ArticleFeedId.ToString()) &&
                                     r.FeedPublishedDate <= endDate);
                }
                else if (startDate != null && endDate == null && sourceFeedIdsCSV == "")
                {
                    query = query.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) &&
                                    r.FeedPublishedDate >= startDate);
                }
                else if (startDate == null && endDate != null && sourceFeedIdsCSV == "")
                {
                    query = query.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) &&
                                    r.FeedPublishedDate <= endDate);
                }
                else if (startDate == null && endDate == null && sourceFeedIdsCSV != "")
                {
                    query = query.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && sourceFeedIdsCSV.Contains(r.ArticleFeedId.ToString()));
                }
                else if (startDate != null && endDate != null && sourceFeedIdsCSV == "")
                {
                    query = query.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()) && (r.FeedPublishedDate >= startDate && r.FeedPublishedDate <= endDate));
                }
                else
                {
                    query = query.Where(r => hamIdsCSV.Contains(r.HamTypeId.ToString()));
                }
                filteredArticleList = (query).ToList();
            }

            /*...........Get filtered data for 1 year (All or Unprocessed articles) or for 1 month (Spam articles) with Similar/Duplicate articles.........*/

            var old = DateTime.Now.AddMonths(-12); // 1 year old

            List<IGrouping<decimal?, SurveillanceProcessedArticle>> filteredArticleGroupResult = new List<IGrouping<decimal?, SurveillanceProcessedArticle>>();

            if (articleTypeId == "all")
            {

                filteredArticleGroupResult = (from r in filteredArticleList
                                              where r.HamTypeId != 1 &&
                                            (r.FeedPublishedDate >= old)
                                              orderby r.FeedPublishedDate ascending
                                              group r by r.SimilarClusterId).ToList();

            }
            else if (articleTypeId == "unprocessed")
            {

                filteredArticleGroupResult = (from r in filteredArticleList
                                              where r.HamTypeId != 1 && (r.IsCompleted == null || r.IsCompleted == false) &&
                                            (r.FeedPublishedDate >= old)
                                              orderby r.FeedPublishedDate ascending
                                              group r by r.SimilarClusterId).ToList();
            }
            else  // spam
            {
                var prev = DateTime.Now.AddMonths(-1); // 1 month old

                filteredArticleGroupResult = (from r in filteredArticleList
                                              where r.HamTypeId == 1 &&
                                                   (r.FeedPublishedDate >= prev)
                                              orderby r.FeedPublishedDate ascending
                                              group r by r.SimilarClusterId).ToList();
            }

            List<ArticleGridWithSimilarCluster> finalResult = GetParentArticles(filteredArticleGroupResult);
            var resultJsonMax = Json(finalResult);

            return new JsonResult()
            {
                ContentEncoding = Encoding.Default,
                ContentType = "application/json",
                Data = finalResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }

        /// <summary>
        /// Creates a list of processed parent articles.
        /// For each valid cluster ID (positive non-null), the parent article is the article with the latest publish date.
        /// For each non-valid cluster ID (null or negative), each article is marked as a parent article.
        /// </summary>
        /// <param name="articlesByGroupList">list of processed articles grouped by cluster ID</param>
        /// <returns>list of parent articles ordered by descending publish date</returns>
        public List<ArticleGridWithSimilarCluster> GetParentArticles(List<IGrouping<decimal?, SurveillanceProcessedArticle>> articlesByGroupList)
        {
            if (articlesByGroupList == null)
            {
                return new List<ArticleGridWithSimilarCluster>();
            }

            var articleFeedDict = dbContext.SurveillanceArticleFeeds
                .ToDictionary(feed => feed.ArticleFeedId, feed => feed.ArticleFeedName);

            List<ArticleGridWithSimilarCluster> parentArticles = new List<ArticleGridWithSimilarCluster>();

            foreach (var articleGroup in articlesByGroupList)
            {
                var sortedGroups = articleGroup.OrderByDescending(group => group.FeedPublishedDate);

                // cluster ID can be negative when UNLINK has been manually triggered
                var isValidClusterId = articleGroup.Key != null && articleGroup.Key > 0;
                if (isValidClusterId)
                {
                    var parentArticle = sortedGroups.First();

                    parentArticles.Add(new ArticleGridWithSimilarCluster(parentArticle)
                    {
                        ArticleFeedName = parentArticle.ArticleFeedId == null ? "" : articleFeedDict[(int)parentArticle.ArticleFeedId],
                        HasChildArticle = (articleGroup.Count() > 1) ? true : false
                });
                }
                else
                {
                    parentArticles.AddRange(sortedGroups.Select(item => new ArticleGridWithSimilarCluster(item)
                    {
                        ArticleFeedName = item.ArticleFeedId == null ? "" : articleFeedDict[(int)item.ArticleFeedId],
                        HasChildArticle = false
                    }).ToList());
                }
            }

            // Sort parent articles by publish date
            return parentArticles.OrderByDescending(d => d.FeedPublishedDate).ToList();
        }

        public ActionResult EditArticleMetaData(string id, bool hasSimilarArticle, bool isChild, string parentId, string viewName)
        {
            var model = new ArticleDetailsById
            {
                ArticleDetails = ArticleDetailsById.GetArticleById(dbContext, id, hasSimilarArticle, isChild, parentId)
            };
            return PartialView(viewName, model);
        }

        public ActionResult ViewArticleById(string id, string viewName)
        {
            var model = new ArticleDetailsById();
            model.ArticleDetails = ArticleDetailsById.GetArticleViewModelById(id);
            return PartialView(viewName, model);
        }

        [HttpPost]
        public int UpdateArticleDetails(ArticleUpdateModel articleUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return 0;
            }

            var currEventIds = articleUpdateModel.SelectedEventIds;
            var currSelectedEventIDs = new HashSet<int>(currEventIds?.Split(',').Select(int.Parse).ToArray() ?? new int[0]);

            var articleId = articleUpdateModel.ArticleID;
            var currentArticle = dbContext.SurveillanceProcessedArticles
                .Include(a => a.Events)
                .Include(a => a.Xtbl_Article_Location_Disease)
                .Single(s => s.ArticleId == articleId);

            currentArticle.Notes = articleUpdateModel.Notes;
            currentArticle.IsCompleted = bool.Parse(articleUpdateModel.IsCompleted);
            currentArticle.IsImportant = bool.Parse(articleUpdateModel.IsImportant);
            currentArticle.HamTypeId = int.Parse(articleUpdateModel.HamTypeId);
            currentArticle.UserLastModifiedDate = DateTime.Now;
            currentArticle.LastUpdatedByUserName = User.Identity.Name;

            var diseaseObject = JsonConvert.DeserializeObject<List<ArticleLocationDisease>>(articleUpdateModel.DiseaseObject);

            currentArticle.Events.Clear();
            currentArticle.Xtbl_Article_Location_Disease.Clear();

            dbContext.SurveillanceEvents.Where(e => currSelectedEventIDs.Contains(e.EventId)).ForEach(e => { currentArticle.Events.Add(e); });

            foreach (var item in diseaseObject)
            {
                var ald = new SurveillanceXtbl_Article_Location_Disease
                {
                    ArticleId = articleId,
                    LocationGeoNameId = item.LocationId,
                    DiseaseId = item.DiseaseId,
                    NewSuspectedCount = item.NewSuspectedCount,
                    NewConfirmedCount = item.NewConfirmedCount,
                    NewReportedCount = item.NewReportedCount,
                    NewDeathCount = item.NewDeathCount,
                    TotalSuspectedCount = item.TotalSuspectedCount,
                    TotalConfirmedCount = item.TotalConfirmedCount,
                    TotalReportedCount = item.TotalReportedCount,
                    TotalDeathCount = item.TotalDeathCount
                };

                currentArticle.Xtbl_Article_Location_Disease.Add(ald);
            }

            dbContext.SaveChanges();
            return 1;
        }

        public int UpdateArticleReadStatus(string articleID, bool isRead)
        {
            var currentArticle = dbContext.SurveillanceProcessedArticles.Where(s => s.ArticleId == articleID).SingleOrDefault();
            currentArticle.LastUpdatedByUserName = User.Identity.Name;
            currentArticle.IsRead = isRead;

            if (ModelState.IsValid)
            {
                dbContext.SaveChanges();
                return 1;
            }

            return 0;
        }

        public ActionResult UpdateArticleClusterID(string articleID, Decimal? clusterID)
        {
            var currentArticle = dbContext.SurveillanceProcessedArticles.Where(s => s.ArticleId == articleID).SingleOrDefault();
            currentArticle.LastUpdatedByUserName = User.Identity.Name;
            currentArticle.SimilarClusterId = clusterID;

            if (ModelState.IsValid)
            {
                dbContext.SaveChanges();
                return null;
            }

            return null;
        }

        /// <summary>
        /// Handles GET request for retrieving list of events filtered by the specified search term.
        /// </summary>
        /// <param name="term">Event title to match search on. Returned events can be partial matches of the search term.</param>
        /// <returns>HTTP response containing list of matching events</returns>
        [HttpGet]
        public ActionResult GetEventDataJson(string term)
        {
            var response = dbContext.SurveillanceEvents
                                .Where(s => s.EventTitle.Contains(term))
                                .Select(m => new
                                {
                                    eventId = m.EventId,
                                    label = m.EventTitle,
                                    isPublished = m.IsPublished,
                                    eventStartDate = m.StartDate.Value.ToString().Replace("/Date(", "").Replace(")/", ""),
                                    eventEndDate = m.EndDate.Value.ToString().Replace("/Date(", "").Replace(")/", "")
                                }).ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Handles GET request for retrieving list of locations filtered by the specified search term.
        /// </summary>
        /// <param name="term">Geoname to match search on. Returned locations can be partial matches of the search term.</param>
        /// <returns>HTTP response containing list of matching locations</returns>
        [HttpGet]
        public ActionResult GetLocationDataJson(string term)
        {
            var locationSearchSP = dbContext.usp_SearchGeonames(term).ToList();

            using (var context = new BiodSurveillanceDataEntities())
            {
                var result = new List<GeonameBdLocation>();
                for (int i = 0; i < locationSearchSP.Count; i++)
                {
                    var m_item = new GeonameBdLocation
                    {
                        geonameId = locationSearchSP[i].GeonameId,
                        label = locationSearchSP[i].DisplayName
                    };
                    result.Add(m_item);
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Handles GET request for retrieving list of diseases filtered by the specified search term.
        /// </summary>
        /// <param name="term">Disease name to match search on. Returned diseases can be partial matches of the search term.</param>
        /// <returns>HTTP response containing list of matching diseases</returns>
        [HttpGet]
        public ActionResult GetDiseaseDataJson(string term)
        {
            var response = dbContext.Diseases
                                .Where(s => s.DiseaseName.Contains(term))
                                .Select(m => new
                                {
                                    diseaseId = m.DiseaseId,
                                    label = m.DiseaseName,
                                }).ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Groups processed articles for the specified event ID.
        /// Each group, uniquely identified by geoname ID, contains non-spam articles ordered by descending publish date.
        /// </summary>
        /// <param name="Id">event ID to base search on</param>
        /// <returns>Serialized list of non-spam processed articles group by event location (<c>EventArticlesCategorizedByLocations</c>)</returns>
        public ActionResult GetArticlesForEventGroupedByLocation(int? Id)
        {
            try
            {
                var eventItem = dbContext.SurveillanceEvents.Find(Id);
                var DiseaseIdForEvent = eventItem.DiseaseId;
                var ProcessedArticleForEvent = eventItem.ProcessedArticles.Where(s => s.HamTypeId != 1).OrderByDescending(p => p.FeedPublishedDate).ToList();

                List<EventArticlesCategorizedByLocations> articleListCategorizedbyUniqueLocations = new List<EventArticlesCategorizedByLocations>();

                //...Get unique locations defined by Events
                List<int> uniqueLocationIds = new List<int>();
                var eventLocations = dbContext.SurveillanceXtbl_Event_Location.Where(a => a.EventId == eventItem.EventId).ToList();

                foreach (var item in eventLocations)
                {
                    if (!uniqueLocationIds.Contains(item.GeonameId))
                    {
                        uniqueLocationIds.Add(item.GeonameId);
                    }
                }

                foreach (var uniqueLocId in uniqueLocationIds)
                {
                    EventArticlesCategorizedByLocations articleCategorizedByLoc = new EventArticlesCategorizedByLocations();
                    articleCategorizedByLoc.GeoLocationID = uniqueLocId;
                    articleCategorizedByLoc.GeoLocationName = dbContext.SurveillanceGeonames.Where(s => s.GeonameId == uniqueLocId).Select(m => m.DisplayName).SingleOrDefault();
                    articleCategorizedByLoc.Articles = new List<ArticleWithCaseCounts>();
                    articleListCategorizedbyUniqueLocations.Add(articleCategorizedByLoc);
                }

                //...placeholder list contains those articles that either does not have any geolocations or their defined geolocations does not match with Event's geolocation
                List<string> placeholder = new List<string>();
                List<string> articleIncluded = new List<string>();

                for (var i = 0; i < ProcessedArticleForEvent.Count; i++)
                {
                    var artID = ProcessedArticleForEvent.ElementAt(i).ArticleId;
                    var articleGeoIds = dbContext.SurveillanceXtbl_Article_Location_Disease.Where(a => a.ArticleId == artID).Select(g => g.LocationGeoNameId).ToList();

                    if (articleGeoIds.Count != 0)
                    {
                        foreach (var artGeoId in articleGeoIds)
                        {
                            if (uniqueLocationIds.Contains(artGeoId))
                            {
                                foreach (var unqlocId in uniqueLocationIds)
                                {
                                    if (artGeoId == unqlocId)
                                    {
                                        EventArticlesCategorizedByLocations uniLocationObjById = articleListCategorizedbyUniqueLocations.Where(g => g.GeoLocationID == unqlocId).SingleOrDefault();

                                        ArticleWithCaseCounts articleCaseCount = new ArticleWithCaseCounts();
                                        articleCaseCount.ArticleId = ProcessedArticleForEvent.ElementAt(i).ArticleId;
                                        articleCaseCount.SimilarClusterId = ProcessedArticleForEvent.ElementAt(i).SimilarClusterId;
                                        articleCaseCount.IsCompleted = ProcessedArticleForEvent.ElementAt(i).IsCompleted;
                                        articleCaseCount.IsImportant = ProcessedArticleForEvent.ElementAt(i).IsImportant;
                                        articleCaseCount.IsRead = ProcessedArticleForEvent.ElementAt(i).IsRead;
                                        articleCaseCount.FeedPublishedDate = ProcessedArticleForEvent.ElementAt(i).FeedPublishedDate.ToString("yyyy-MM-dd");  //s.FeedPublishedDate.ToString("yyyy-MM-dd"),
                                        articleCaseCount.ArticleTitle = ProcessedArticleForEvent.ElementAt(i).ArticleTitle;
                                        articleCaseCount.ArticleFeedName = ProcessedArticleForEvent.ElementAt(i).ArticleFeed.ArticleFeedName;

                                        var articleId = ProcessedArticleForEvent.ElementAt(i).ArticleId;
                                        var recordforArticle = dbContext.SurveillanceXtbl_Article_Location_Disease.Where(s => s.ArticleId == articleId && s.LocationGeoNameId == unqlocId && s.DiseaseId == DiseaseIdForEvent).SingleOrDefault();

                                        Location loc = new Location();
                                        if (recordforArticle != null && unqlocId != -1)
                                        {
                                            loc.GeoID = recordforArticle.LocationGeoNameId;
                                            loc.GeoName = dbContext.SurveillanceGeonames.Where(s => s.GeonameId == unqlocId).Select(m => m.DisplayName).SingleOrDefault();
                                            loc.NewSuspectedCount = recordforArticle.NewSuspectedCount ?? 0;
                                            loc.NewConfirmedCount = recordforArticle.NewConfirmedCount ?? 0;
                                            loc.NewReportedCount = recordforArticle.NewReportedCount ?? 0;
                                            loc.NewDeathCount = recordforArticle.NewDeathCount ?? 0;
                                            loc.TotalSuspectedCount = recordforArticle.TotalSuspectedCount ?? 0;
                                            loc.TotalConfirmedCount = recordforArticle.TotalConfirmedCount ?? 0;
                                            loc.TotalReportedCount = recordforArticle.TotalReportedCount ?? 0;
                                            loc.TotalDeathCount = recordforArticle.TotalDeathCount ?? 0;
                                        }
                                        else
                                        {
                                            loc = null;
                                        }

                                        articleCaseCount.LocationCaseCount = loc;
                                        uniLocationObjById.Articles.Add(articleCaseCount);
                                        //Add this articleID to the articleIncluded list
                                        if (!articleIncluded.Contains(artID))
                                        {
                                            articleIncluded.Add(artID);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //add it to the placeholder
                                if (!placeholder.Contains(artID))
                                {
                                    placeholder.Add(artID);
                                }
                            }
                        }
                    }
                    else
                    {
                        //add it to the placeholder when there is no geo loation for the article
                        if (!placeholder.Contains(artID))
                        {
                            placeholder.Add(artID);
                        }
                    }
                }

                // Filter placeholder list. And Create an object for "Other Locations" if placeholder list is not empty
                foreach (var articleId in articleIncluded)
                {
                    if (placeholder.Contains(articleId))
                    {
                        string article = placeholder.Where(x => x == articleId).SingleOrDefault();
                        placeholder.Remove(article);
                    }
                }

                if (placeholder.Count != 0)
                {
                    EventArticlesCategorizedByLocations articleCategorizedByOtherLoc = new EventArticlesCategorizedByLocations();
                    articleCategorizedByOtherLoc.GeoLocationID = 0;
                    articleCategorizedByOtherLoc.GeoLocationName = "Other Locations";

                    List<ArticleWithCaseCounts> articleListWithCaseCount = new List<ArticleWithCaseCounts>();

                    foreach (var articleId in placeholder)
                    {
                        var articleItem = ProcessedArticleForEvent.Where(x => x.ArticleId == articleId).SingleOrDefault();

                        ArticleWithCaseCounts articleCaseCount = new ArticleWithCaseCounts();
                        articleCaseCount.ArticleId = articleItem.ArticleId;
                        articleCaseCount.SimilarClusterId = articleItem.SimilarClusterId;
                        articleCaseCount.IsCompleted = articleItem.IsCompleted;
                        articleCaseCount.IsImportant = articleItem.IsImportant;
                        articleCaseCount.IsRead = articleItem.IsRead;
                        articleCaseCount.FeedPublishedDate = articleItem.FeedPublishedDate.ToString("yyyy-MM-dd");  //s.FeedPublishedDate.ToString("yyyy-MM-dd"),
                        articleCaseCount.ArticleTitle = articleItem.ArticleTitle;
                        articleCaseCount.ArticleFeedName = articleItem.ArticleFeed.ArticleFeedName;
                        articleCaseCount.LocationCaseCount = null;
                        articleListWithCaseCount.Add(articleCaseCount);
                    }
                    articleCategorizedByOtherLoc.Articles = articleListWithCaseCount;
                    articleListCategorizedbyUniqueLocations.Add(articleCategorizedByOtherLoc);
                }

                return Json(articleListCategorizedbyUniqueLocations, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Groups processed articles for the specified suggested event ID.
        /// Each group is uniquely identified by geoname ID.
        /// </summary>
        /// <param name="Id">suggested event ID to base search on</param>
        /// <returns>Serialized list of processed articles group by event location (<c>EventArticlesCategorizedByLocations</c>)</returns>
        public ActionResult GetArticlesForSuggestedEventGroupedByLocation(string Id)
        {
            try
            {
                var SuggestedEventItem = dbContext.SuggestedEvents.Find(Id);
                var ProcessedArticleForSuggestedEvent = SuggestedEventItem.ProcessedArticles.ToList();

                List<EventArticlesCategorizedByLocations> articleListCategorizedbyUniqueLocations = new List<EventArticlesCategorizedByLocations>();

                //...Get unique locations defined by SuggestedEvents
                List<int> uniqueLocationIds = new List<int>();
                var eventLocations = SuggestedEventItem.Geonames.ToList();

                foreach (var item in eventLocations)
                {
                    if (!uniqueLocationIds.Contains(item.GeonameId))
                    {
                        uniqueLocationIds.Add(item.GeonameId);
                    }
                }

                foreach (var uniqueLocId in uniqueLocationIds)
                {
                    EventArticlesCategorizedByLocations articleCategorizedByLoc = new EventArticlesCategorizedByLocations();
                    articleCategorizedByLoc.GeoLocationID = uniqueLocId;
                    articleCategorizedByLoc.GeoLocationName = dbContext.SurveillanceGeonames.Where(s => s.GeonameId == uniqueLocId).Select(m => m.DisplayName).SingleOrDefault();
                    articleCategorizedByLoc.Articles = new List<ArticleWithCaseCounts>();
                    articleListCategorizedbyUniqueLocations.Add(articleCategorizedByLoc);
                }

                //...placeholder list contains those articles that either does not have any geolocations or their defined geolocations does not match with Event's geolocation
                List<string> placeholder = new List<string>();
                List<string> articleIncluded = new List<string>();

                for (var i = 0; i < ProcessedArticleForSuggestedEvent.Count; i++)
                {
                    var artID = ProcessedArticleForSuggestedEvent.ElementAt(i).ArticleId;
                    var articleGeoIds = dbContext.SurveillanceXtbl_Article_Location_Disease.Where(a => a.ArticleId == artID).Select(g => g.LocationGeoNameId).ToList();

                    if (articleGeoIds.Count != 0)
                    {
                        foreach (var artGeoId in articleGeoIds)
                        {
                            if (uniqueLocationIds.Contains(artGeoId))
                            {
                                foreach (var unqlocId in uniqueLocationIds)
                                {
                                    if (artGeoId == unqlocId)
                                    {
                                        EventArticlesCategorizedByLocations uniLocationObjById = articleListCategorizedbyUniqueLocations.Where(g => g.GeoLocationID == unqlocId).SingleOrDefault();

                                        ArticleWithCaseCounts articleCaseCount = new ArticleWithCaseCounts();
                                        articleCaseCount.ArticleId = ProcessedArticleForSuggestedEvent.ElementAt(i).ArticleId;
                                        articleCaseCount.SimilarClusterId = ProcessedArticleForSuggestedEvent.ElementAt(i).SimilarClusterId;
                                        articleCaseCount.IsCompleted = ProcessedArticleForSuggestedEvent.ElementAt(i).IsCompleted;
                                        articleCaseCount.IsImportant = ProcessedArticleForSuggestedEvent.ElementAt(i).IsImportant;
                                        articleCaseCount.IsRead = ProcessedArticleForSuggestedEvent.ElementAt(i).IsRead;
                                        articleCaseCount.FeedPublishedDate = ProcessedArticleForSuggestedEvent.ElementAt(i).FeedPublishedDate.ToString("yyyy-MM-dd");  //s.FeedPublishedDate.ToString("yyyy-MM-dd"),
                                        articleCaseCount.ArticleTitle = ProcessedArticleForSuggestedEvent.ElementAt(i).ArticleTitle;
                                        articleCaseCount.ArticleFeedName = ProcessedArticleForSuggestedEvent.ElementAt(i).ArticleFeed.ArticleFeedName;
                                        articleCaseCount.LocationCaseCount = null;
                                        uniLocationObjById.Articles.Add(articleCaseCount);
                                        //...Add this articleID to the articleIncluded list
                                        if (!articleIncluded.Contains(artID))
                                        {
                                            articleIncluded.Add(artID);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //add it to the placeholder
                                if (!placeholder.Contains(artID))
                                {
                                    placeholder.Add(artID);
                                }
                            }
                        }
                    }
                    else
                    {
                        //add it to the placeholder when there is no geo loation for the article
                        if (!placeholder.Contains(artID))
                        {
                            placeholder.Add(artID);
                        }
                    }
                }

                // Filter placeholder list. And Create an object for "Other Locations" if placeholder list is not empty
                foreach (var articleId in articleIncluded)
                {
                    if (placeholder.Contains(articleId))
                    {
                        string article = placeholder.Where(x => x == articleId).SingleOrDefault();
                        placeholder.Remove(article);
                    }
                }

                if (placeholder.Count != 0)
                {
                    EventArticlesCategorizedByLocations articleCategorizedByOtherLoc = new EventArticlesCategorizedByLocations();
                    articleCategorizedByOtherLoc.GeoLocationID = 0;
                    articleCategorizedByOtherLoc.GeoLocationName = "Other Locations";

                    List<ArticleWithCaseCounts> articleListWithCaseCount = new List<ArticleWithCaseCounts>();

                    foreach (var articleId in placeholder)
                    {
                        var articleItem = ProcessedArticleForSuggestedEvent.Where(x => x.ArticleId == articleId).SingleOrDefault();

                        ArticleWithCaseCounts articleCaseCount = new ArticleWithCaseCounts();
                        articleCaseCount.ArticleId = articleItem.ArticleId;
                        articleCaseCount.SimilarClusterId = articleItem.SimilarClusterId;
                        articleCaseCount.IsCompleted = articleItem.IsCompleted;
                        articleCaseCount.IsImportant = articleItem.IsImportant;
                        articleCaseCount.IsRead = articleItem.IsRead;
                        articleCaseCount.FeedPublishedDate = articleItem.FeedPublishedDate.ToString("yyyy-MM-dd");  //s.FeedPublishedDate.ToString("yyyy-MM-dd"),
                        articleCaseCount.ArticleTitle = articleItem.ArticleTitle;
                        articleCaseCount.ArticleFeedName = articleItem.ArticleFeed.ArticleFeedName;
                        articleCaseCount.LocationCaseCount = null;
                        articleListWithCaseCount.Add(articleCaseCount);
                    }
                    articleCategorizedByOtherLoc.Articles = articleListWithCaseCount;
                    articleListCategorizedbyUniqueLocations.Add(articleCategorizedByOtherLoc);
                }

                return Json(articleListCategorizedbyUniqueLocations, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events - Active and Inactive

        [HttpPost]
        [Obsolete("This API is now deprecated. Use GetEventModalWindow with an unregistered event ID")]
        public int CreateEventPost(EventUpdateModel eventCreate)
        {
            var currentDate = DateTime.Now;
            SurveillanceEvent eventItem = new SurveillanceEvent
            {
                SpeciesId = Constants.Species.HUMAN,
                EventTitle = eventCreate.eventTitle ?? "Untitled event",
                DiseaseId = dbContext.Diseases.OrderBy(d => d.DiseaseName).First().DiseaseId,
                StartDate = eventCreate.startDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventCreate.startDate),
                EndDate = eventCreate.endDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventCreate.endDate),
                PriorityId = Convert.ToInt32(eventCreate.priorityID),
                CreatedDate = currentDate,
                LastUpdatedByUserName = User.Identity.Name,
                LastUpdatedDate = currentDate,
                Summary = eventCreate.summary ?? "",
                Notes = eventCreate.notes ?? "",
                IsLocalOnly = Convert.ToBoolean(eventCreate.alertRadius),
                IsPublished = Convert.ToBoolean(eventCreate.isPublished),
                IsPublishedChangesToApi = bool.Parse(eventCreate.isPublishedChangesToApi)
            };

            dbContext.SurveillanceEvents.Add(eventItem);
            try 
            { 
                dbContext.SaveChanges();
                return eventItem.EventId;
            }
            catch (Exception e)
            {
                return Constants.Event.INVALID_ID;
            }
        }

        //[HttpPost]
        //Cannot retrieve multi-select elements e.g. ReasonForCreationIds through an Event class type. Hence using the FormCollection type
        //public ActionResult CreateEvent([Bind(Include = "EventId,EventTitle,StartDate,EndDate,LastUpdatedDate,ReasonForCreationIds,PriorityId,IsActive,Summary,Notes")] Event eventItem)
        //public ActionResult CreateEvent([Bind(Include = "EventId,EventTitle,StartDate,EndDate,LastUpdatedDate,ReasonForCreationIds,PriorityId,IsPublished,Summary,Notes")] FormCollection form)
        //{            
        //    return RedirectToAction("Index");
        //}

        public ActionResult EditEvent(int id, string viewName)
        {
            var model = new CreatedEventDialogViewModel(dbContext, id);

            return PartialView(viewName, model);
        }

        [HttpPost]
        public int EventUpdate(EventUpdateModel eventModel)
        {
            return UpdateEventFromViewModel(eventModel);
        }

        private int UpdateEventFromViewModel(EventUpdateModel eventModel)
        { 
            int eventID = Convert.ToInt32(eventModel.eventID);
            var currentEvent = dbContext.SurveillanceEvents
                .Include(e => e.ProcessedArticles)
                .SingleOrDefault(e => e.EventId == eventID)
                ??
                new SurveillanceEvent();

            // Update entry in Surveillance DB
            currentEvent.EventTitle = eventModel.eventTitle;
            currentEvent.DiseaseId = Convert.ToInt32(eventModel.diseaseID);
            currentEvent.SpeciesId = eventModel.speciesID;
            currentEvent.StartDate = eventModel.startDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventModel.startDate);
            currentEvent.EndDate = eventModel.endDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventModel.endDate);
            currentEvent.PriorityId = Convert.ToInt32(eventModel.priorityID);
            currentEvent.LastUpdatedByUserName = User.Identity.Name;
            currentEvent.LastUpdatedDate = DateTime.Now;
            currentEvent.Summary = eventModel.summary;
            currentEvent.Notes = eventModel.notes;
            currentEvent.IsLocalOnly = Convert.ToBoolean(eventModel.alertRadius);
            currentEvent.IsPublished = Convert.ToBoolean(eventModel.isPublished);
            currentEvent.IsPublishedChangesToApi = eventModel.isPublishedChangesToApi.IsNullOrWhiteSpace() ? (bool?)null : bool.Parse(eventModel.isPublishedChangesToApi);

            // Change currentEventItem state to modified
            var eventItem = dbContext.Entry(currentEvent);
            eventItem.State = EntityState.Modified;

            // Load existing items for ManyToMany COllection
            eventItem.Collection(i => i.EventCreationReasons).Load();
            eventItem.Collection(i => i.Xtbl_Event_Location).Load();

            // Clear Event items
            currentEvent.EventCreationReasons.Clear();
            currentEvent.Xtbl_Event_Location.Clear();

            string[] selectedReasonIDs = eventModel.reasonIDs == null || eventModel.reasonIDs[0] == "" ? null : eventModel.reasonIDs;
            if (selectedReasonIDs != null)
            {
                currentEvent.EventCreationReasons = dbContext.SurveillanceEventCreationReasons
                    .Where(r => selectedReasonIDs.Contains(r.ReasonId.ToString()))
                    .ToList();
            }

            var locationObject = JsonConvert.DeserializeObject<List<EventLocation>>(eventModel.locationObject ?? "[]");
            currentEvent.Xtbl_Event_Location = locationObject
                .Select(item => new SurveillanceXtbl_Event_Location
                {
                    EventId = eventID,
                    GeonameId = item.GeonameId,
                    EventDate = item.EventDate,
                    SuspCases = item.SuspCases,
                    RepCases = item.RepCases,
                    ConfCases = item.ConfCases,
                    Deaths = item.Deaths
                })
                .ToList();

            if (ModelState.IsValid)
            {
                if (eventID == Constants.Event.INVALID_ID)
                {
                    dbContext.SurveillanceEvents.Add(currentEvent);
                }
                dbContext.SaveChanges();

                return currentEvent.EventId;
            }

            return Constants.Event.INVALID_ID;
        }

        private static HttpClient GetHttpClient(string baseUrl)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            return client;
        }

        static async Task<HttpResponseMessage> UpdateZebraEventCaseHistory(int eventId)
        {
            HttpResponseMessage response;
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApi");
                var requestUrl = "api/ZebraUpdateEventCaseHistory";
                using (var client = GetHttpClient(baseUrl))
                {
                    response = await client.PostAsJsonAsync(requestUrl, eventId).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseResult = await response.Content.ReadAsStringAsync();
                    }
                }

                //...UAT Sync
                if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsProduction")))
                {
                    HttpResponseMessage responseUAT = new HttpResponseMessage();
                    var baseUrl_UAT = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApiUAT");
                    var requestUrl_UAT = "api/ZebraUpdateEventCaseHistory";
                    using (var client = GetHttpClient(baseUrl_UAT))
                    {
                        responseUAT = await client.PostAsJsonAsync(requestUrl_UAT, eventId);

                        if (responseUAT.IsSuccessStatusCode)
                        {
                            var responseResult = await responseUAT.Content.ReadAsStringAsync();
                        }
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                return new HttpResponseMessage();
            }
        }

        public async Task<ActionResult> PublishChangesToApi(EventUpdateModel eventModel)
        {
            Logging.Log($"Publishing changes for event {eventModel.eventID}: {eventModel.eventTitle}");
            eventModel.isPublishedChangesToApi = bool.TrueString;

            Logging.Log("Saving event metadata into Surveillance db");
            var eventID = UpdateEventFromViewModel(eventModel);
            if (eventID != -1)
            {
                var currentEvent = dbContext.SurveillanceEvents
                .Include(e => e.ProcessedArticles)
                .SingleOrDefault(e => e.EventId == eventID);
                if (currentEvent == null)
                {
                    return Json(new { status = "error", message = $"no database entry found for event {eventID}" });
                }

                eventModel.associatedArticles = GetSerializedArticlesForEvent(currentEvent);
                eventModel.LastUpdatedByUserName = User.Identity.Name;

                Logging.Log("Saving event into Zebra db");
                var result = await EventUpdateZebraApi(eventModel);
                if (result)
                {
                    Logging.Log($"Sending proximal email notification for event {eventID}");
                    await SendProximalEmailNotification(Convert.ToInt32(eventModel.eventID));

                    // Update history table to match latest count
                    // This will prevent future non-case-count updates from sending a proximal e-mail
                    await UpdateHistoricalCaseCountApi(eventModel);

                    Logging.Log($"Successfully published changes for event {eventID}");
                    return Json(new { status = "success", data = eventModel.eventID });
                }
            }

            eventModel.isPublishedChangesToApi = bool.FalseString;
            Logging.Log($"Failed to publish changes for event {eventModel.eventID}");

            return Json(new { status = "error", message = "failed to publish event" });
        }
        
        static async Task<bool> EventUpdateZebraApi(EventUpdateModel eventModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApi");
                var requestUrl = "api/ZebraEventUpdate";
                using (var client = GetHttpClient(baseUrl))
                {
                    response = await client.PostAsJsonAsync(requestUrl, eventModel);
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }

                    await response.Content.ReadAsStringAsync();
                }

                //...UAT Sync
                // TODO Refactor obsolete code baseUrl_UAT is not used
                if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsProduction")))
                {
                    HttpResponseMessage responseUAT = new HttpResponseMessage();
                    var baseUrl_UAT = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApiUAT");
                    var requestUrl_UAT = "api/ZebraEventUpdate";
                    using (var client = GetHttpClient(baseUrl_UAT))
                    {
                        responseUAT = await client.PostAsJsonAsync(requestUrl_UAT, eventModel);

                        if (responseUAT.IsSuccessStatusCode)
                        {
                            var responseResult = await responseUAT.Content.ReadAsStringAsync();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                return false;
            }
        }

        static async Task<bool> UpdateHistoricalCaseCountApi(EventUpdateModel eventModel)
        {
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApi");
                var requestUrl = "api/ZebraUpdateEventCaseHistory";
                using (var client = GetHttpClient(baseUrl))
                {
                    var response = await client.PostAsJsonAsync(requestUrl, eventModel.eventID);
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                }

                //...UAT Sync
                if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsProduction")))
                {
                    var baseUrl_UAT = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApiUAT");
                    var requestUrl_UAT = "api/ZebraUpdateEventCaseHistory";
                    using (var client = GetHttpClient(baseUrl_UAT))
                    {
                        await client.PostAsJsonAsync(requestUrl_UAT, eventModel.eventID);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                return false;
            }
        }

        private string GetSerializedArticlesForEvent(SurveillanceEvent EventItem)
        {
            List<ArticleUpdateForZebra> articleList = EventItem.ProcessedArticles
                .Select(item => new ArticleUpdateForZebra
                {
                    ArticleId = item.ArticleId,
                    ArticleTitle = item.ArticleTitle,
                    SystemLastModifiedDate = item.SystemLastModifiedDate,
                    CertaintyScore = item.CertaintyScore,
                    ArticleFeedId = item.ArticleFeedId,
                    FeedURL = item.FeedURL,
                    FeedSourceId = item.FeedSourceId,
                    FeedPublishedDate = item.FeedPublishedDate,
                    HamTypeId = item.HamTypeId,
                    OriginalSourceURL = item.OriginalSourceURL,
                    IsCompleted = item.IsCompleted,
                    SimilarClusterId = item.SimilarClusterId,
                    OriginalLanguage = item.OriginalLanguage,
                    UserLastModifiedDate = item.UserLastModifiedDate,
                    LastUpdatedByUserName = item.LastUpdatedByUserName,
                    Notes = item.Notes,
                    ArticleBody = null, //article body increases payload
                                IsRead = item.IsRead,
                    DiseaseObject = "",
                    SelectedPublishedEventIds = new List<int>()
                })
                .ToList();

            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue
            };

            return serializer.Serialize(articleList);
        }

        public async Task<ActionResult> EventPublishAsync(EventUpdateModel eventModel)
        {
            Logging.Log($"Publishing event {eventModel.eventID}: {eventModel.eventTitle}");
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Logging.Log("Saving event metadata into Surveillance db");

                    if (!ModelState.IsValid)
                    {
                        return Json(new { status = "error", message = "invalid model passed"});
                    }

                    eventModel.isPublishedChangesToApi = bool.FalseString;
                    var eventID = UpdateEventFromViewModel(eventModel);
                    if (eventID != -1)
                    {
                        var currentEvent = dbContext.SurveillanceEvents
                            .Include(e => e.ProcessedArticles)
                            .Single(e => e.EventId == eventID);
                        eventModel.associatedArticles = GetSerializedArticlesForEvent(currentEvent);
                        eventModel.LastUpdatedByUserName = User.Identity.Name;
                        eventModel.eventID = currentEvent.EventId.ToString();

                        Logging.Log("Saving event into Zebra db");
                        var result = await EventUpdateZebraApi(eventModel);
                        if (result)
                        {
                            currentEvent.IsPublished = true;
                            dbContext.SaveChanges();
                            dbContextTransaction.Commit();
                            Logging.Log($"Successfully published event {currentEvent.EventId}");

                            Logging.Log($"Sending event email notification for event {currentEvent.EventId}");
                            await SendEventEmailNotification(currentEvent.EventId);

                            return Json(new { status = "success", data = currentEvent.EventId });
                        } else
                        {
                            return Json(new { status = "error", message = $"Error: EventId: {currentEvent.EventId} DiseaseId: {currentEvent.DiseaseId}" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log("Failed to publish event");
                    dbContextTransaction.Rollback();

                    return Json(new { status = "error", message = ex.Message });
                }
            }

            return Json(new { status = "error", message = "failed to publish event" });
        }


        public async Task<ActionResult> SendEventEmailNotification(int eventID)
        {
            try
            {
                //...Destination
                var baseUrlDest = ConfigurationManager.AppSettings.Get("ZebraEmailUsersLocatedInEventDestinationAreaApi");
                var requestUrlDest = baseUrlDest + "?eventId=" + eventID;
                string resultStringDest = string.Empty;

                using (var client = GetHttpClient(baseUrlDest))
                {
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                    HttpResponseMessage response = await client.PostAsync(requestUrlDest, null).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        resultStringDest = await response.Content.ReadAsStringAsync();
                    }
                }

                //...Local UAT Publish 
                if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsProduction")))
                {
                    //...Destination UAT publish
                    var baseUrlDest_UAT = ConfigurationManager.AppSettings.Get("ZebraEmailUsersLocatedInEventDestinationAreaApiUAT");
                    var requestUrlDest_UAT = baseUrlDest_UAT + "?eventId=" + eventID;
                    string resultStringDest_UAT = string.Empty;

                    using (var client = GetHttpClient(baseUrlDest_UAT))
                    {
                        client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                        client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                        //HttpResponseMessage response = client.GetAsync(requestUrlDest_UAT).Result;
                        HttpResponseMessage response = await client.PostAsync(requestUrlDest_UAT, null).ConfigureAwait(false);

                        if (response.IsSuccessStatusCode)
                        {
                            resultStringDest_UAT = await response.Content.ReadAsStringAsync();
                        }
                    }
                }//UAT

                return Json("success");
            }
            catch (Exception ex)
            {
                var responseResult = "Error: " + ex.Message;
                return Json("failed");
            }
        }


        public async Task<ActionResult> SendProximalEmailNotification(int eventId)
        {
            try
            {
                var baseUrlProximal = ConfigurationManager.AppSettings.Get("ZebraEmailUsersProximalEmailaApi");
                var requestUrlProximal = baseUrlProximal + "?eventId=" + eventId;
                string resultStringProximal = string.Empty;
                string responseResult = "success";

                using (var client = GetHttpClient(baseUrlProximal))
                {
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                    HttpResponseMessage response = await client.PostAsync(requestUrlProximal, null).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        resultStringProximal = await response.Content.ReadAsStringAsync();
                    }
                }

                //Send the proximal email to UAT if the environment is production
                if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsProduction")))
                {
                    var baseUrlProximal_UAT = ConfigurationManager.AppSettings.Get("ZebraEmailUsersProximalEmailaApiUAT");
                    var requestUrlLocal_UAT = baseUrlProximal_UAT + "?eventId=" + eventId;
                    string resultStringProximal_UAT = string.Empty;

                    using (var client = GetHttpClient(baseUrlProximal_UAT))
                    {
                        client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                        client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                        HttpResponseMessage response = await client.PostAsync(requestUrlLocal_UAT, null).ConfigureAwait(false);
                        if (response.IsSuccessStatusCode)
                        {
                            resultStringProximal_UAT = await response.Content.ReadAsStringAsync();
                        }
                    }
                }

                return Json("success");
            }
            catch (Exception Ex)
            {
                var responseResult = "Error: " + Ex.Message;
                return Json("failed");
            }
        }


        public async Task<ActionResult> SyncAllEventsToMongoDB()
        {

            var responseResult = "";
            using (var db = new BiodSurveillanceDataEntities())
            {

                var eventPrioritiesKvp = new List<KeyValuePair<int, string>>();
                var eventPriority = db.SurveillanceEventPriorities.ToList();
                for (var i = 0; i < eventPriority.Count; i++)
                {
                    eventPrioritiesKvp.Add(new KeyValuePair<int, string>(eventPriority.ElementAt(i).PriorityId, eventPriority.ElementAt(i).PriorityTitle));
                }

                var eventsOnDev = db.SurveillanceEvents.ToList();
                foreach (var eventItem in eventsOnDev)
                {

                    JavaScriptSerializer js = new JavaScriptSerializer();

                    // Get Event Reasons
                    var eventReasons = eventItem.EventCreationReasons.ToList();
                    List<string> selectedReasonTitles = new List<string>();
                    if (eventReasons.Count != 0)
                    {
                        foreach (var reasonItem in eventReasons)
                        {
                            selectedReasonTitles.Add(reasonItem.ReasonName);
                        }
                    };

                    // Get Event associated articles
                    var articleLists = eventItem.ProcessedArticles.ToList();
                    List<string> articleIdsAssociated = new List<string>();
                    foreach (var art in articleLists)
                    {
                        articleIdsAssociated.Add(art.ArticleId);
                    }


                    // Get Event Locations with case counts
                    List<EventCaseCountMongo> locations = new List<EventCaseCountMongo>();
                    var eventItems_Location = eventItem.Xtbl_Event_Location.ToList();
                    if (eventItems_Location.Count != 0)
                    {
                        //JavaScriptSerializer js = new JavaScriptSerializer();
                        //var locObjArr = JsonConvert.DeserializeObject<List<EventLocation>>(eventmodel.locationObject);

                        var locationGroups = eventItems_Location.GroupBy(x => x.GeonameId);
                        foreach (var group in locationGroups)
                        {
                            EventCaseCountMongo location = new EventCaseCountMongo();
                            location.geonameId = group.Key;

                            List<EventCaseCountsByDateMongo> caseCounts = new List<EventCaseCountsByDateMongo>();
                            foreach (var item in group)
                            {
                                EventCaseCountsByDateMongo caseCount = new EventCaseCountsByDateMongo();
                                caseCount.date = item.EventDate.ToString("yyyy-MM-dd");
                                caseCount.suspected = item.SuspCases ?? 0;
                                caseCount.confirmed = item.ConfCases ?? 0;
                                caseCount.reported = item.RepCases ?? 0;
                                caseCount.deaths = item.Deaths ?? 0;
                                caseCounts.Add(caseCount);
                            }
                            location.caseCountsByDate = caseCounts;
                            locations.Add(location);
                        }
                    }

                    EventMetadataSyncMongoDB evtMongo = new EventMetadataSyncMongoDB();
                    evtMongo.eventId = eventItem.EventId;
                    evtMongo.eventName = eventItem.EventTitle;
                    evtMongo.priority = eventPrioritiesKvp.First(kvp => kvp.Key == eventItem.PriorityId).Value;
                    evtMongo.summary = eventItem.Summary;
                    evtMongo.startDate = (eventItem.StartDate != null) ? eventItem.StartDate.Value.ToString("yyyy-MM-dd") : null;
                    evtMongo.endDate = (eventItem.EndDate != null) ? eventItem.EndDate.Value.ToString("yyyy-MM-dd") : null;
                    evtMongo.diseaseId = eventItem.DiseaseId;
                    evtMongo.speciesId = eventItem.SpeciesId;
                    evtMongo.localOnly = eventItem.IsLocalOnly;
                    evtMongo.approvedForPublishing = eventItem.IsPublished ?? false;
                    evtMongo.publishedDate = null;
                    evtMongo.reasonForCreation = selectedReasonTitles;
                    evtMongo.notes = eventItem.Notes;
                    evtMongo.locations = locations;
                    evtMongo.associatedArticleIds = articleIdsAssociated;

                    JavaScriptSerializer js1 = new JavaScriptSerializer();

                    try
                    {
                        var baseUrl = ConfigurationManager.AppSettings.Get("MongoDBSyncMetadataApi");
                        var requestUrl = "api/v1/Surveillance/Events";
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(baseUrl);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("MongodbBasicAuthUsernameAndPassword"));
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                            var response = await client.PostAsJsonAsync(requestUrl, evtMongo);

                            if (response.IsSuccessStatusCode)
                            {
                                responseResult = await response.Content.ReadAsStringAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        responseResult = "Error: " + ex.Message;
                    }
                }
            }
            return Json(responseResult);
        }

        public async Task<ActionResult> EventSynchMongoDB(EventUpdateModel eventModel, bool isPublishing)
        {
            var eventId = Convert.ToInt32(eventModel.eventID);

            var currentEvent = dbContext.SurveillanceEvents
                .Include(e => e.EventPriority)
                .Include(e => e.ProcessedArticles)
                .Include(e => e.EventCreationReasons)
                .Single(e => e.EventId == eventId);

            EventMetadataSyncMongoDB eventToSync = new EventMetadataSyncMongoDB
            {
                eventId = eventId,
                eventName = string.IsNullOrWhiteSpace(eventModel.eventTitle) ? null : eventModel.eventTitle,
                priority = currentEvent.EventPriority.PriorityTitle,
                summary = eventModel.summary,
                startDate = string.IsNullOrWhiteSpace(eventModel.startDate) ? "" : eventModel.startDate,
                endDate = string.IsNullOrWhiteSpace(eventModel.endDate) ? "" : eventModel.endDate,
                diseaseId = Convert.ToInt32(eventModel.diseaseID),
                speciesId = eventModel.speciesID,
                localOnly = Convert.ToBoolean(eventModel.alertRadius),
                approvedForPublishing = currentEvent.IsPublished,
                publishedDate = isPublishing ? DateTime.UtcNow.ToString("o") : "", //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                reasonForCreation = currentEvent.EventCreationReasons.Select(r => r.ReasonName).ToList(),
                notes = string.IsNullOrWhiteSpace(eventModel.notes) ? null : eventModel.notes,
                associatedArticleIds = currentEvent.ProcessedArticles.Select(a => a.ArticleId).ToList(),
                locations = new List<EventCaseCountMongo>()
            };

            if (!string.IsNullOrEmpty(eventModel.locationObject))
            {
                eventToSync.locations = JsonConvert.DeserializeObject<List<EventLocation>>(eventModel.locationObject)
                    .GroupBy(loc => loc.GeonameId)
                    .Select(loc => new EventCaseCountMongo
                    {
                        geonameId = loc.Key,
                        caseCountsByDate = loc.Select(item => new EventCaseCountsByDateMongo
                        {
                            date = item.EventDate.ToString("yyyy-MM-dd"),
                            suspected = item.SuspCases,
                            confirmed = item.ConfCases,
                            reported = item.RepCases,
                            deaths = item.Deaths
                        }).ToList()
                    }).ToList();
            }

            var responseResult = "";
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("MongoDBSyncMetadataApi");
                var requestUrl = "api/v1/Surveillance/Events";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("MongodbBasicAuthUsernameAndPassword"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    var response = await client.PostAsJsonAsync(requestUrl, eventToSync);

                    if (response.IsSuccessStatusCode)
                    {
                        responseResult = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                responseResult = "Error: " + ex.Message;
            }
            return Json(responseResult);
        }

        static async Task<HttpResponseMessage> ArticleUpdateZebraApi(ArticleUpdateForZebra articleModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApi");
                var requestUrl = "api/ZebraArticleUpdate";
                using (var client = GetHttpClient(baseUrl))
                {
                    response = await client.PostAsJsonAsync(requestUrl, articleModel);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseResult = await response.Content.ReadAsStringAsync();
                    }
                }

                //...UAT Sync articles
                if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsProduction")))
                {
                    HttpResponseMessage responseUAT = new HttpResponseMessage();
                    var baseUrl_UAT = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApiUAT");
                    var requestUrl_UAT = "api/ZebraArticleUpdate";
                    using (var client = GetHttpClient(baseUrl_UAT))
                    {
                        responseUAT = await client.PostAsJsonAsync(requestUrl_UAT, articleModel);

                        if (responseUAT.IsSuccessStatusCode)
                        {
                            var responseResult = await responseUAT.Content.ReadAsStringAsync();
                        }
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                return response;
            }
        }

        public async Task<int> ArticlePublishAsync(ArticleUpdateModel articleUpdateModel)
        {
            var responseResult = 0;

            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var articleItem = dbContext.SurveillanceProcessedArticles
                        .Include(a => a.Events)
                        .Include(a => a.Xtbl_Article_Location_Disease)
                        .Single(s => s.ArticleId == articleUpdateModel.ArticleID);

                    //...Save to Surveillence DB
                    var publishedEventIds = articleItem.Events.Where(e => e.IsPublished == true).Select(e => e.EventId).ToList();

                    //...Create the Article model that contains article info, DiseaseObj and SelectedPublishedEventIds
                    var article_item = new ArticleUpdateForZebra
                    {
                        ArticleId = articleItem.ArticleId,
                        ArticleTitle = articleItem.ArticleTitle,
                        SystemLastModifiedDate = articleItem.SystemLastModifiedDate,
                        CertaintyScore = articleItem.CertaintyScore,
                        ArticleFeedId = articleItem.ArticleFeedId,
                        FeedURL = articleItem.FeedURL,
                        FeedSourceId = articleItem.FeedSourceId,
                        FeedPublishedDate = articleItem.FeedPublishedDate,
                        HamTypeId = articleItem.HamTypeId,
                        OriginalSourceURL = articleItem.OriginalSourceURL,
                        IsCompleted = articleItem.IsCompleted,
                        SimilarClusterId = articleItem.SimilarClusterId,
                        OriginalLanguage = articleItem.OriginalLanguage,
                        UserLastModifiedDate = articleItem.UserLastModifiedDate,
                        LastUpdatedByUserName = User.Identity.Name,
                        Notes = articleItem.Notes,
                        ArticleBody = articleItem.ArticleBody,
                        IsRead = articleItem.IsRead,
                        DiseaseObject = articleUpdateModel.DiseaseObject,
                        SelectedPublishedEventIds = publishedEventIds

                    };

                    //var result = ArticleUpdateZebraDatabase(article_item);
                    var result = await ArticleUpdateZebraApi(article_item);
                    if (result.IsSuccessStatusCode)
                    {
                        Logging.Log("Success");
                        responseResult = 1;
                    }

                    //Sync article metadata to MongoDB
                    if (bool.Parse(ConfigurationManager.AppSettings.Get("syncToMongodb")))
                    {
                        var response = ArticleSynchMongoDB(articleUpdateModel);
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception Ex)
                {
                    Logging.Log("Error: " + Ex.Message + "\n" + Ex.InnerException);
                    responseResult = 0;
                    dbContextTransaction.Rollback();
                }
            }
            return responseResult;
        }

        public async Task<ActionResult> ArticleSynchMongoDB(ArticleUpdateModel articleUpdateModel)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            var responseResult = "";

            var articleId = articleUpdateModel.ArticleID;
            var articleItemUpdated = db.SurveillanceProcessedArticles.Find(articleId);

            ArticleMetadataSyncMongoDB articleMongo = new ArticleMetadataSyncMongoDB();
            articleMongo.spamFilterLabel = int.Parse(articleUpdateModel.HamTypeId);
            //articleMongo.userLastModifiedDate = DateTime.UtcNow.ToString("o"); //articleItemUpdated.UserLastModifiedDate.Value.ToUniversalTime().ToString("o"); 

            List<int> diseaseIds = new List<int>();
            List<ArticleLocationAndCaseCountsMongo> locations = new List<ArticleLocationAndCaseCountsMongo>();
            if (!String.IsNullOrEmpty(articleUpdateModel.DiseaseObject))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var locObjArr = JsonConvert.DeserializeObject<List<ArticleLocationDisease>>(articleUpdateModel.DiseaseObject);

                var locationGroups = locObjArr.GroupBy(x => x.LocationId);
                foreach (var group in locationGroups)
                {
                    ArticleLocationAndCaseCountsMongo location = new ArticleLocationAndCaseCountsMongo();
                    location.geonameId = group.Key;

                    List<ArticleDiseaseMongo> diseases = new List<ArticleDiseaseMongo>();
                    foreach (var item in group)
                    {
                        ArticleDiseaseMongo disease = new ArticleDiseaseMongo();
                        disease.diseaseId = item.DiseaseId;

                        ArticleCaseCountMongo newCaseCounts = new ArticleCaseCountMongo();
                        ArticleCaseCountMongo totalCaseCounts = new ArticleCaseCountMongo();
                        newCaseCounts.suspected = item.NewSuspectedCount;
                        newCaseCounts.confirmed = item.NewConfirmedCount;
                        newCaseCounts.reported = item.NewReportedCount;
                        newCaseCounts.deaths = item.NewDeathCount;
                        disease.newCaseCounts = newCaseCounts;

                        totalCaseCounts.suspected = item.TotalSuspectedCount;
                        totalCaseCounts.confirmed = item.TotalConfirmedCount;
                        totalCaseCounts.reported = item.TotalReportedCount;
                        totalCaseCounts.deaths = item.TotalDeathCount;
                        disease.totalCaseCounts = totalCaseCounts;

                        diseases.Add(disease);

                        //unique disease ID list
                        if (!diseaseIds.Contains(item.DiseaseId))
                        {
                            diseaseIds.Add(item.DiseaseId);
                        }
                    }
                    location.disease = diseases;

                    locations.Add(location);
                }
            }
            articleMongo.locations = locations;
            articleMongo.diseaseIds = diseaseIds;
            var currEventIds = articleUpdateModel.SelectedEventIds;
            String[] currSelectedEventIDs = (currEventIds != null) ? currEventIds.Split(',') : null;
            List<int> associatedEventIds = new List<int>();

            if (currSelectedEventIDs != null)
            {
                foreach (var item in currSelectedEventIDs)
                {
                    var eventId = int.Parse(item);
                    associatedEventIds.Add(eventId);
                }
            }

            articleMongo.associatedEventIds = associatedEventIds;
            articleMongo.notes = articleUpdateModel.Notes;

            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("MongoDBSyncMetadataApi");
                var requestUrl = "api/v1/Surveillance/ProcessedArticles/" + articleId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //var byteArray = Encoding.ASCII.GetBytes("superuser:surveillance");
                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("MongodbBasicAuthUsernameAndPassword"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    var response = await client.PutAsJsonAsync(requestUrl, articleMongo);

                    if (response.IsSuccessStatusCode)
                    {
                        responseResult = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                responseResult = "Error: " + ex.Message;
            }
            return Json(responseResult);
        }

        [HttpPost]
        public string DeleteEvent(string ID)
        {

            var evtID = int.Parse(ID);

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            //.....Delete Event from db.Events
            var eventItem = (from e in db.SurveillanceEvents
                             where e.EventId == evtID
                             select e).SingleOrDefault();

            db.SurveillanceEvents.Remove(eventItem);
            db.SaveChanges();

            return "Deleted";
        }

        public ActionResult GetArticleModalWindow(string id, bool hasSimilarArticle, bool isChild, string parentId)
        {
            var model = new ArticleDetailsById
            {
                HamTypes = ArticleByFilterView.GetHamType(dbContext),
                ArticleDetails = ArticleDetailsById.GetArticleById(dbContext, id, hasSimilarArticle, isChild, parentId),
                diseases = ArticleDetailsById.GetArticleDiseaseById(dbContext, id)
            };

            return PartialView("_ArticleDialogWindow", model);
        }

        public ActionResult GetEventModalWindow(int id)
        {
            var model = new CreatedEventDialogViewModel(dbContext, id);

            return PartialView("_EventDialogWindow", model);
        }

        public ActionResult GetEventEmptyModelWindowOnCreate()
        {
            var model = new CreatedEventDialogViewModel(dbContext, -1);

            return PartialView("_EventDialogWindow", model);
        }

        /// <summary>
        /// Handles GET request for retrieving disease type for the specified disease ID
        /// </summary>
        /// <param name="diseaseId">disease identifier</param>
        /// <returns>disease type string</returns>
        [HttpGet]
        public ActionResult GetDiseaseType(int diseaseId)
        {
            var disease = dbContext.Diseases
                                .Where(d => d.DiseaseId == diseaseId)
                                .FirstOrDefault();
            var diseaseType = disease?.DiseaseType ?? "";
            
            return Json(diseaseType, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Tested server-side paging using LINQ
        //............................................Testing Server-side paging for retrieving article list Using LINQ solution ....................................*/
        public JsonResult TestInitialArticleList(string articleType, int draw, int length, int start, string filterObj, string searchString)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            var old = DateTime.Now.AddMonths(-12); // 1 year old

            List<ArticleGridWithSimilarCluster> finalParentArticleList = new List<ArticleGridWithSimilarCluster>();
            List<IGrouping<Decimal?, SurveillanceProcessedArticle>> articleGroupResult = new List<IGrouping<Decimal?, SurveillanceProcessedArticle>>();


            if (articleType == "all")
            {
                articleGroupResult = (from r in db.SurveillanceProcessedArticles
                                      where r.HamTypeId != 1 &&
                                               (r.FeedPublishedDate >= old)
                                      //orderby r.FeedPublishedDate descending
                                      group r by r.SimilarClusterId).ToList();

            }
            else if (articleType == "unprocessed")
            {
                articleGroupResult = (from r in db.SurveillanceProcessedArticles
                                      where r.HamTypeId != 1 &&
                                            (r.IsCompleted == null || r.IsCompleted == false) &&
                                            (r.FeedPublishedDate >= old)
                                      //orderby r.FeedPublishedDate descending
                                      group r by r.SimilarClusterId).ToList();
            }
            else
            { //spam 

                var spamArticles = ArticleCount.SpamArticleList();
                articleGroupResult = (from r in spamArticles
                                          //orderby r.FeedPublishedDate descending
                                      group r by r.SimilarClusterId).ToList();

            }

            finalParentArticleList = GetParentArticles(articleGroupResult);
            var finalParentArticleListWithSearchString = finalParentArticleList.Where(c => c.ArticleTitle.Contains(searchString)).ToList();

            var displayDataCurrentPage = finalParentArticleListWithSearchString.Skip(start).Take(length);

            //return Json(finalArticleList);
            return Json(new
            {
                datasource = displayDataCurrentPage,
                drawTable = draw,
                recordsTotal = finalParentArticleListWithSearchString.Count,
                recordsFiltered = finalParentArticleListWithSearchString.Count
            }, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetChildArticleList(string articleType, string parentArticleId, Decimal? similarClusterId)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            List<ArticleBase> childArticleList = new List<ArticleBase>();

            var articleFeedKvp = new List<KeyValuePair<int, string>>();

            var articleFeed = db.SurveillanceArticleFeeds.ToList();
            for (var i = 0; i < articleFeed.Count; i++)
            {
                articleFeedKvp.Add(new KeyValuePair<int, string>(articleFeed.ElementAt(i).ArticleFeedId, articleFeed.ElementAt(i).ArticleFeedName));
            }


            var old = DateTime.Now.AddMonths(-12); // 1 year old
                                                   //List<ProcessedArticle> childItems = (from r in db.ProcessedArticles
                                                   //                                     where r.HamTypeId != 1 && (r.FeedPublishedDate >= old) && r.SimilarClusterId == SimilarClusterId && r.ArticleId != articleId
                                                   //                                     select r).ToList();

            List<SurveillanceProcessedArticle> childItems = new List<SurveillanceProcessedArticle>();
            if (articleType == "all")
            {
                childItems = (from r in db.SurveillanceProcessedArticles
                              where r.HamTypeId != 1 &&
                                       (r.FeedPublishedDate >= old) && r.SimilarClusterId == similarClusterId && r.ArticleId != parentArticleId
                              select r).ToList();

            }
            else if (articleType == "unprocessed")
            {
                childItems = (from r in db.SurveillanceProcessedArticles
                              where r.HamTypeId != 1 &&
                                    (r.IsCompleted == null || r.IsCompleted == false) &&
                                    (r.FeedPublishedDate >= old) && r.SimilarClusterId == similarClusterId && r.ArticleId != parentArticleId
                              select r).ToList();
            }
            else
            { //spam 

                var spamArticles = ArticleCount.SpamArticleList();
                childItems = (from r in spamArticles
                              where r.SimilarClusterId == similarClusterId && r.ArticleId != parentArticleId
                              select r).ToList();

            }

            var childArticleSorted = childItems.OrderByDescending(d => d.FeedPublishedDate);

            foreach (var n in childArticleSorted)
            {
                ArticleBase child = new ArticleBase();
                child.ArticleId = n.ArticleId;
                child.ArticleTitle = n.ArticleTitle;
                child.SimilarClusterId = n.SimilarClusterId;
                child.IsCompleted = n.IsCompleted;
                child.IsRead = n.IsRead;
                child.FeedPublishedDateToString = n.FeedPublishedDate.ToString("yyyy-MM-dd");
                child.ArticleFeedId = n.ArticleFeedId;
                child.ArticleFeedName = articleFeedKvp.First(kvp => kvp.Key == n.ArticleFeedId).Value;

                childArticleList.Add(child);
            }

            return Json(childArticleList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Server-side paging using Stored Procedure
        /*............................................Server-side paging for retrieving article list using SP solution...............................*/
        public JsonResult GetParentArticleListSP(string articleType, int draw, int length, int start, string filterString, string searchString)
        {
            // TODO: This needs to be revisited when implementing PT-646. There should not be a need to do 2 different SP calls
            try
            {
                if (string.IsNullOrWhiteSpace(filterString))
                {
                    // TODO: See if usp_GetParentArticleList is faster than usp_GetParentArticleListByFilters. Also, simplify parameters of this method to be included in filterString
                    var parentArticleList = dbContext.usp_GetParentArticleList(articleType, start, length, searchString).ToList();
                    var totalRecords = dbContext.usp_GetTotalArticleRecords(articleType, searchString).FirstOrDefault().Value;

                    return Json(new
                    {
                        datasource = parentArticleList,
                        drawTable = draw,
                        recordsTotal = totalRecords, // FIXME: This does not actually return total number of articles but number of filtered articles
                        recordsFiltered = totalRecords
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var filter = JsonConvert.DeserializeObject<ArticleFilter>(filterString);
                    var locationIds = filter.locationIds.Length > 0 ? string.Join(",", filter.locationIds) : "";
                    var sourceIds = filter.sourceIds.Length > 0 ? string.Join(",", filter.sourceIds) : "";
                    var diseaseIds = filter.diseaseIds.Length > 0 ? string.Join(",", filter.diseaseIds) : "";

                    var nonSpamIds = string.Join(",", dbContext.SurveillanceHamTypes.Where(t => t.HamTypeId != Constants.HamType.SPAM).Select(t => t.HamTypeId).ToArray());
                    var hamType = (filter.hamType == Constants.HamType.ALL_NON_SPAM) ? nonSpamIds : filter.hamType.ToString();

                    var parentArticleList = dbContext.usp_GetParentArticleListByFilters(articleType, start, length, filter.startDate, filter.endDate, hamType, sourceIds, diseaseIds, locationIds, searchString).ToList();
                    var totalRecords = dbContext.usp_GetTotalArticleRecordsByFilters(articleType, filter.startDate, filter.endDate, hamType, sourceIds, diseaseIds, locationIds, searchString).FirstOrDefault().Value;

                    return Json(new
                    {
                        datasource = parentArticleList,
                        drawTable = draw,
                        recordsTotal = totalRecords, // FIXME: This does not actually return total number of articles but number of filtered articles
                        recordsFiltered = totalRecords
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                return Json("failed", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetChildArticleListSP(string articleType, string parentArticleId, Decimal? similarClusterId)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            IEnumerable<usp_GetChildArticleList_Result> childArticleList = db.usp_GetChildArticleList(articleType, parentArticleId, similarClusterId).ToList();

            return Json(childArticleList, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Suggested Event
        [HttpGet]
        public ActionResult SuggestedEventView(string suggestedEventId, string viewName)
        {
            SuggestedEvent suggestedEvent = dbContext.SuggestedEvents.Find(suggestedEventId);

            SuggestedEventViewModel suggestedEventItem = new SuggestedEventViewModel
            {
                EventId = suggestedEventId,
                EventTitle = suggestedEvent.EventTitle,
                StartDate = null,
                EndDate = null,
                AssociatedArticle = null
            };

            return PartialView(viewName, suggestedEventItem);
        }

        [HttpGet]
        public ActionResult SuggestedEventDialogView(string suggestedEventId)
        {
            return PartialView("_SuggestedEventDialogView", new SuggestedEventDialogViewModel(dbContext, suggestedEventId));
        }

        /// <summary>
        /// Handles POST request for converting a suggested event into a real event.
        /// This involves deleting the suggested event from the Suggested Event DB table
        /// and adding into the Event DB table. Synchronizer is also executed to notify
        /// Data Services of this change.
        /// </summary>
        /// <returns>event ID of the real event created, or -1 if creation failed</returns>
        [HttpPost]
        public int SaveSuggestedEventOnDialogWindow(SuggestedEventModel eventModel)
        {
            try
            {
                var suggestedEvent = dbContext.SuggestedEvents
                    .Include(e => e.ProcessedArticles)
                    .Include(e => e.Geonames)
                    .Where(e => e.SuggestedEventId == eventModel.eventID)
                    .SingleOrDefault();
                if (suggestedEvent == null)
                {
                    return Constants.Event.INVALID_ID;
                }

                SurveillanceEvent realEvent = new SurveillanceEvent
                {
                    EventTitle = eventModel.eventTitle,
                    DiseaseId = Convert.ToInt32(eventModel.diseaseID),
                    SpeciesId = eventModel.speciesID,
                    StartDate = eventModel.startDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventModel.startDate),
                    EndDate = eventModel.endDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventModel.endDate),
                    PriorityId = Convert.ToInt32(eventModel.priorityID),
                    LastUpdatedByUserName = User.Identity.Name,
                    LastUpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Summary = eventModel.summary,
                    Notes = eventModel.notes,
                    IsLocalOnly = Convert.ToBoolean(eventModel.alertRadius),
                    IsPublished = Convert.ToBoolean(eventModel.isPublished),
                    IsPublishedChangesToApi = false,
                    ProcessedArticles = new List<SurveillanceProcessedArticle>()
                };

                if (!ModelState.IsValid)
                {
                    return Constants.Event.INVALID_ID;
                }

                dbContext.SurveillanceEvents.Add(realEvent);
                dbContext.SaveChanges();

                // Change currentEventItem state to modified
                var eventItem = dbContext.Entry(realEvent);
                eventItem.State = EntityState.Modified;

                // Load existing items for ManyToMany COllection
                eventItem.Collection(i => i.EventCreationReasons).Load();
                eventItem.Collection(i => i.Xtbl_Event_Location).Load();

                var currentEvent = dbContext.SurveillanceEvents.Find(realEvent.EventId);

                //...Clear Event items
                currentEvent.EventCreationReasons.Clear();
                currentEvent.Xtbl_Event_Location.Clear();

                string[] selectedReasonIDs = eventModel.reasonIDs == null || eventModel.reasonIDs[0] == "" ? null : eventModel.reasonIDs;
                if (selectedReasonIDs != null)
                {
                    currentEvent.EventCreationReasons = dbContext.SurveillanceEventCreationReasons
                        .Where(r => selectedReasonIDs.Contains(r.ReasonId.ToString()))
                        .ToList();
                }

                var locationObject = JsonConvert.DeserializeObject<List<EventLocation>>(eventModel.locationObject ?? "[]");
                locationObject.AddRange(suggestedEvent.Geonames
                    .Select(g => new EventLocation
                    {
                        GeonameId = g.GeonameId, 
                        EventDate = Constants.Date.DEFAULT
                    })
                    .ToList());
                currentEvent.Xtbl_Event_Location = locationObject
                    .Select(item => new SurveillanceXtbl_Event_Location
                    {
                        EventId = currentEvent.EventId,
                        GeonameId = item.GeonameId,
                        EventDate = item.EventDate,
                        SuspCases = item.SuspCases,
                        RepCases = item.RepCases,
                        ConfCases = item.ConfCases,
                        Deaths = item.Deaths
                    })
                    .ToList();

                var articleIds = suggestedEvent.ProcessedArticles.Select(a => a.ArticleId).ToList();
                dbContext.SurveillanceProcessedArticles
                    .Include(a => a.Events)
                    .Include(a => a.Xtbl_Article_Location_Disease)
                    .Where(a => articleIds.Contains(a.ArticleId))
                    .ForEach(a => a.Events.Add(currentEvent));

                if (!ModelState.IsValid)
                {
                    return Constants.Event.INVALID_ID;
                }

                dbContext.SuggestedEvents.Remove(suggestedEvent);
                dbContext.SaveChanges();

                //Sync to MongoDB
                EventUpdateModel eventmodel = GetMongodbModelForSuggestedEvent(currentEvent, eventModel);
                var mongoSync = EventSynchMongoDB(eventmodel, false);

                //...User Action Update in the API
                SuggestedEventUserAction userAction = new SuggestedEventUserAction
                {
                    userAction = (int)EnumUserAction.UserActions.Created,
                    reasonsForRejection = new List<string>(),
                    eventId = currentEvent.EventId
                };
                Task<HttpResponseMessage> response = UpdateUserActiononSuggestedEvent(eventModel.eventID, userAction);

                return currentEvent.EventId;
            }
            catch (Exception Ex)
            {
                Logging.Log("Error: " + Ex.Message + "\n" + Ex.InnerException);
                return Constants.Event.INVALID_ID;
            }
        }

        // TODO: Deprecate this API and have callers use SaveSuggestedEventOnDialogWindow
        [HttpPost]
        public int QuickSaveSuggestedEvent(string suggestedEventId)
        {
            var suggestedEvent = dbContext.SuggestedEvents.Where(e => e.SuggestedEventId == suggestedEventId).SingleOrDefault();
            if (suggestedEvent == null)
            {
                return Constants.Event.INVALID_ID;
            }

            var eventModel = new SuggestedEventModel
            {
                eventID = suggestedEvent.SuggestedEventId,
                eventTitle = suggestedEvent.EventTitle,
                diseaseID = suggestedEvent.DiseaseId.ToString(),
                speciesID = Constants.Species.HUMAN,
                priorityID = Constants.Priority.MEDIUM.ToString(),
                isPublished = "false",
                LastUpdatedByUserName = User.Identity.Name
            };

            return SaveSuggestedEventOnDialogWindow(eventModel);
        }

        [HttpPost]
        public async Task<ActionResult> PublishSuggestedEventOnDialogWindow(SuggestedEventModel eventModel)
        {
            try
            {
                Logging.Log("Converting suggested event into a real event");
                var realEventId = SaveSuggestedEventOnDialogWindow(eventModel);
                if (realEventId == Constants.Event.INVALID_ID)
                {
                    return Json(new { status = "error", message = "Failed to convert suggested event into a real event." });
                }

                var realEvent = dbContext.SurveillanceEvents.Find(realEventId);
                EventUpdateModel eventmodel = GetMongodbModelForSuggestedEvent(realEvent, eventModel);

                Logging.Log("Saving event into Zebra db");
                var result = await EventUpdateZebraApi(eventmodel);
                if (result)
                {
                    realEvent.IsPublished = true;
                    if (ModelState.IsValid)
                    {
                        dbContext.SaveChanges();
                    }
                    
                    await SendEventEmailNotification(realEventId);

                    Logging.Log("Syncing event metadata into Mongo DB");
                    await EventSynchMongoDB(eventmodel, true);

                    Logging.Log("Syncing suggested event metadata into Mongo DB");
                    SuggestedEventUserAction userAction = new SuggestedEventUserAction
                    {
                        userAction = (int)EnumUserAction.UserActions.Created,
                        reasonsForRejection = new List<string>(),
                        eventId = realEventId
                    };
                    Task<HttpResponseMessage> response = UpdateUserActiononSuggestedEvent(eventModel.eventID, userAction);
                }
                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                return Json(new { status = "error", message = "Failed to publish event." });
            }
        }

        public EventUpdateModel GetMongodbModelForSuggestedEvent(SurveillanceEvent realEvent, SuggestedEventModel suggestedEventItemInfo)
        {
            return new EventUpdateModel
            {
                eventID = realEvent.EventId.ToString(),
                eventTitle = realEvent.EventTitle,
                startDate = realEvent.StartDate?.ToString("yyyy-MM-dd"),
                endDate = realEvent.EndDate?.ToString("yyyy-MM-dd"),
                diseaseID = realEvent.DiseaseId.ToString(),
                speciesID = realEvent.SpeciesId,
                reasonIDs = suggestedEventItemInfo.reasonIDs ?? new string[] { "" },
                alertRadius = realEvent.IsLocalOnly.ToString(),
                priorityID = realEvent.PriorityId.ToString(),
                isPublished = realEvent.IsPublished.ToString() ?? "false",
                summary = realEvent.Summary,
                notes = realEvent.Notes,
                locationObject = suggestedEventItemInfo.locationObject ?? "",
                LastUpdatedByUserName = User.Identity.Name,
                associatedArticles = new JavaScriptSerializer().Serialize(
                    (dbContext.SurveillanceEvents
                    .Where(e => e.EventId == realEvent.EventId)
                    .Select(e => e.ProcessedArticles)
                    .SingleOrDefault()
                    ??
                    new List<SurveillanceProcessedArticle>())
                    .Select(item => new ArticleUpdateForZebra
                    {
                        ArticleId = item.ArticleId,
                        ArticleTitle = item.ArticleTitle,
                        SystemLastModifiedDate = item.SystemLastModifiedDate,
                        CertaintyScore = item.CertaintyScore,
                        ArticleFeedId = item.ArticleFeedId,
                        FeedURL = item.FeedURL,
                        FeedSourceId = item.FeedSourceId,
                        FeedPublishedDate = item.FeedPublishedDate,
                        HamTypeId = item.HamTypeId,
                        OriginalSourceURL = item.OriginalSourceURL,
                        IsCompleted = item.IsCompleted,
                        SimilarClusterId = item.SimilarClusterId,
                        OriginalLanguage = item.OriginalLanguage,
                        UserLastModifiedDate = item.UserLastModifiedDate,
                        LastUpdatedByUserName = item.LastUpdatedByUserName,
                        Notes = item.Notes,
                        ArticleBody = item.ArticleBody,
                        IsRead = item.IsRead,
                        DiseaseObject = "",
                        SelectedPublishedEventIds = new List<int>()
                    })
                    .ToList())
            };
        }

        [HttpPost]
        public JsonResult DeleteSuggestedEvent(string eventId)
        {
            var eventItem = dbContext.SuggestedEvents
                .SingleOrDefault(e => e.SuggestedEventId == eventId);
            if (eventItem == null)
            {
                return Json(new { status = "error", message = $"no suggested event exists with ID {eventId}" });
            }

            dbContext.SuggestedEvents.Remove(eventItem);
            dbContext.SaveChanges();

            SuggestedEventUserAction userAction = new SuggestedEventUserAction
            {
                userAction = (int)EnumUserAction.UserActions.Deleted,
                reasonsForRejection = new List<string>(),
                eventId = null
            };
            Task<HttpResponseMessage> response = UpdateUserActiononSuggestedEvent(eventId, userAction);

            return Json(new { status = "success" });
        }

        /// <summary>
        /// Asynchronous task for updating user action for the suggested event metadata into Mongo DB
        /// </summary>
        /// <param name="suggestedEventId">event ID of the suggested event to be synced</param>
        /// <param name="userAction">int specifying the action performed (from <c>EnumUserAction.UserActions</c>)</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> UpdateUserActiononSuggestedEvent(string suggestedEventId, SuggestedEventUserAction userAction)
        {
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("MongoDBSyncMetadataApi");
                var requestUrl = "api/v1/Surveillance/SuggestedEvents/" + suggestedEventId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("MongodbBasicAuthUsernameAndPassword"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    var responseResult = await client.PutAsync(requestUrl, new StringContent(JsonConvert.SerializeObject(userAction), Encoding.UTF8, "application/json"));

                    if (responseResult.IsSuccessStatusCode)
                    {
                        await responseResult.Content.ReadAsStringAsync();
                    }

                    return responseResult;
                }
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                return new HttpResponseMessage();
            }
        }
        #endregion
    }
}