using SignalRSurveillance;
using Biod.Surveillance.Models.Surveillance;
using Biod.Surveillance.ViewModels;
//using Microsoft.AspNet.SignalR;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections;
using Biod.Surveillance.Infrastructures;
using Microsoft.Ajax.Utilities;

namespace Biod.Surveillance.Controllers
{
    //[System.Web.Mvc.Authorize]
    public class HomeController : Controller
    {
        protected BiodSurveillanceDataEntities dbContext;

        public HomeController()
        {
            dbContext = new BiodSurveillanceDataEntities();
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

        //public void NotifyUpdates()
        //{
        //    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
        //    hubContext.Clients.All.UpdateSurveillance(User.Identity.Name.Substring(0, User.Identity.Name.IndexOf("@")));
        //}

        //[Authorize]
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


        public JsonResult GetAllProcessedArticles(string ID)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            var old = DateTime.Now.AddMonths(-12); // 1 year old

            List<ArticleGridWithSimilarCluster> finalArticleList = new List<ArticleGridWithSimilarCluster>();
            List<IGrouping<Decimal?, ProcessedArticle>> articleGroupResult = new List<IGrouping<Decimal?, ProcessedArticle>>();


            if (ID == "all")
            {
                articleGroupResult = (from r in db.ProcessedArticles
                                      where r.HamTypeId != 1 &&
                                               (r.FeedPublishedDate >= old)
                                      //orderby r.FeedPublishedDate descending
                                      group r by r.SimilarClusterId).ToList();
            }
            else if (ID == "unprocessed")
            {
                articleGroupResult = (from r in db.ProcessedArticles
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
                                      group r by r.SimilarClusterId).ToList();
            }

            finalArticleList = GetArticleWithDuplicates(articleGroupResult);

            return Json(finalArticleList);


        }


        [HttpPost]
        public JsonResult GetFilteredArticle(FormCollection form)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            List<ProcessedArticle> filteredArticleList = new List<ProcessedArticle>();
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
                IEnumerable<ProcessedArticle> articleProcessed = null;

                if (diseaseIdsCSV != "" && locationIdsCSV != "")
                {
                    articleProcessed = (from r in db.Xtbl_Article_Location_Disease
                                        where diseaseIds.Contains(r.DiseaseId) &&
                                           locationIds.Contains(r.LocationGeoNameId)
                                        select r.ProcessedArticle).Distinct();
                }
                else if (diseaseIdsCSV != "" && locationIdsCSV == "")
                {

                    articleProcessed = (from r in db.Xtbl_Article_Location_Disease
                                        where diseaseIds.Contains(r.DiseaseId)
                                        select r.ProcessedArticle).Distinct();

                    //var test = db.Xtbl_Article_Location_Disease.Where(r => diseaseIds.Contains(r.DiseaseId)).Select(s =>s.ProcessedArticle).Distinct().ToList();

                }
                else if (diseaseIdsCSV == "" && locationIdsCSV != "")
                {
                    articleProcessed = (from r in db.Xtbl_Article_Location_Disease
                                        where locationIds.Contains(r.LocationGeoNameId)
                                        select r.ProcessedArticle).Distinct();
                }
                else
                {
                    articleProcessed = (from r in db.Xtbl_Article_Location_Disease
                                        select r.ProcessedArticle).Distinct();
                }

                IEnumerable<ProcessedArticle> query1 = articleProcessed;

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
                var query = from r in db.ProcessedArticles select r;

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


            List<IGrouping<Decimal?, ProcessedArticle>> filteredArticleGroupResult = new List<IGrouping<Decimal?, ProcessedArticle>>();

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
            else
            {   //spam 

                var prev = DateTime.Now.AddMonths(-1); // 1 month old

                filteredArticleGroupResult = (from r in filteredArticleList
                                              where r.HamTypeId == 1 &&
                                                   (r.FeedPublishedDate >= prev)
                                              orderby r.FeedPublishedDate ascending
                                              group r by r.SimilarClusterId).ToList();

            }


            List<ArticleGridWithSimilarCluster> finalResult = GetArticleWithDuplicates(filteredArticleGroupResult);
            var resultJsonMax = Json(finalResult);

            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.MaxJsonLength = Int32.MaxValue; 
            ////var obj = serializer.Deserialize<myObject>(resultJsonMax.ToString());

            return new JsonResult()
            {
                ContentEncoding = Encoding.Default,
                ContentType = "application/json",
                Data = finalResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }


        public List<ArticleGridWithSimilarCluster> GetArticleWithDuplicates(List<IGrouping<Decimal?, ProcessedArticle>> articleGroupResult)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            var articleFeedKvp = new List<KeyValuePair<int, string>>();

            var articleFeed = db.ArticleFeeds.ToList();
            for (var i = 0; i < articleFeed.Count; i++)
            {
                articleFeedKvp.Add(new KeyValuePair<int, string>(articleFeed.ElementAt(i).ArticleFeedId, articleFeed.ElementAt(i).ArticleFeedName));
            }

            List<ArticleGridWithSimilarCluster> finalArticleList = new List<ArticleGridWithSimilarCluster>();

            foreach (var articleGroup in articleGroupResult)
            {

                //sort article group by feedPublishDate
                var articleGroupSorted = articleGroup.OrderByDescending(d => d.FeedPublishedDate);


                if (articleGroup.Key != null && articleGroup.Key > 0)
                {

                    ArticleGridWithSimilarCluster parentArticle = new ArticleGridWithSimilarCluster();
                    List<ArticleBase> childArticleList = new List<ArticleBase>();

                    var parentItem = articleGroupSorted.First();
                    var childItems = articleGroupSorted.Skip(1);

                    parentArticle.ArticleId = parentItem.ArticleId;
                    parentArticle.ArticleTitle = parentItem.ArticleTitle;
                    parentArticle.SimilarClusterId = parentItem.SimilarClusterId;
                    parentArticle.IsCompleted = parentItem.IsCompleted;
                    parentArticle.IsRead = parentItem.IsRead;
                    parentArticle.FeedPublishedDate = parentItem.FeedPublishedDate;
                    parentArticle.FeedPublishedDateToString = parentItem.FeedPublishedDate.ToString("yyyy-MM-dd"); //feed published dates are converted to String for easy displaying on the front-end through accessing the Model
                    parentArticle.ArticleFeedId = parentItem.ArticleFeedId;
                    parentArticle.ArticleFeedName = articleFeedKvp.First(kvp => kvp.Key == parentItem.ArticleFeedId).Value;


                    foreach (var n in childItems)
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
                    finalArticleList.Add(parentArticle);
                }
                else
                {  // i.e. SimilarClusterId == NULL or Negative
                    foreach (var n in articleGroupSorted)
                    {
                        ArticleGridWithSimilarCluster parentArticleNoCluster = new ArticleGridWithSimilarCluster();
                        List<ArticleBase> childArticleList = new List<ArticleBase>();

                        parentArticleNoCluster.ArticleId = n.ArticleId;
                        parentArticleNoCluster.ArticleTitle = n.ArticleTitle;
                        parentArticleNoCluster.SimilarClusterId = n.SimilarClusterId;
                        parentArticleNoCluster.IsCompleted = n.IsCompleted;
                        parentArticleNoCluster.IsRead = n.IsRead;
                        parentArticleNoCluster.FeedPublishedDate = n.FeedPublishedDate;
                        parentArticleNoCluster.FeedPublishedDateToString = n.FeedPublishedDate.ToString("yyyy-MM-dd");
                        parentArticleNoCluster.ArticleFeedId = n.ArticleFeedId;
                        parentArticleNoCluster.ArticleFeedName = articleFeedKvp.First(kvp => kvp.Key == n.ArticleFeedId).Value;
                        //parentArticleNoCluster.ChildArticles = childArticleList;
                        finalArticleList.Add(parentArticleNoCluster);
                    }
                }
            }

            //sort finalArticleList by FeedPubDate
            var finalArticleListSortd = finalArticleList.OrderByDescending(d => d.FeedPublishedDate).ToList();

            return finalArticleListSortd;
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
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            dynamic model = null;
            model = new ArticleDetailsById();
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
            var currentArticle = dbContext.ProcessedArticles
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

            dbContext.Events.Where(e => currSelectedEventIDs.Contains(e.EventId)).ForEach(e => { currentArticle.Events.Add(e); });

            foreach (var item in diseaseObject)
            {
                var ald = new Xtbl_Article_Location_Disease
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
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            var currentArticle = db.ProcessedArticles.Where(s => s.ArticleId == articleID).SingleOrDefault();
            currentArticle.LastUpdatedByUserName = User.Identity.Name;
            currentArticle.IsRead = isRead;

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return 1;
            }

            return 0;
        }

        public ActionResult UpdateArticleClusterID(string articleID, Decimal? clusterID)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            var currentArticle = db.ProcessedArticles.Where(s => s.ArticleId == articleID).SingleOrDefault();
            currentArticle.LastUpdatedByUserName = User.Identity.Name;
            currentArticle.SimilarClusterId = clusterID;

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return null;
            }

            return null;
        }

        [HttpGet]
        //....this function returns Event list for the ArticleMetaData - Event Autocomplete
        public ActionResult GetEventDataJson(string term)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            var response = db.Events
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

        [HttpGet]
        //...this function returns Location list for the ArticleMetaData - Location Autocomplete
        public ActionResult GetLocationDataJson(string term)
        {
            //BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            //var response = db.uvw_GeonamesSubset
            //                    .Where(s => s.DisplayName.Contains(term))
            //                    .Select(m => new
            //                    {
            //                        geonameId = m.GeonameId,
            //                        label = m.DisplayName,
            //                    }).Take(10).ToList();

            //return Json(response, JsonRequestBehavior.AllowGet);

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            var locationSearchSP = db.usp_SearchGeonames(term).ToList();


            using (var context = new BiodSurveillanceDataEntities())
            {
                //string m_query = "select s.[GeonameId], s.DisplayName, s.LocationType from("
                //                    + " SELECT top 3 [GeonameId]"
                //                    + " , [DisplayName] , LocationType"
                //                    + " FROM [BiodApi].[place].uvw_GeonamesSubset"
                //                    + " where [DisplayName] like '" + term + "%' and LocationType = 2"
                //                    + " UNION ALL"
                //                    + " SELECT top 3 [GeonameId]"
                //                    + " , [DisplayName]  , LocationType"
                //                    + " FROM [BiodApi].[place].uvw_GeonamesSubset"
                //                    + " where [DisplayName] like '" + term + "%' and LocationType = 4"
                //                    + " UNION ALL"
                //                    + " SELECT top 3 [GeonameId]"
                //                    + " , [DisplayName]    , LocationType"
                //                    + " FROM [BiodApi].[place].uvw_GeonamesSubset"
                //                    + " where [DisplayName] like '" + term + "%' and LocationType = 6"
                //                    + " ) s"
                //                    + " order by s.LocationType";

                //var response = context.uvw_GeonamesSubset.SqlQuery(m_query).ToList();

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


        [HttpGet]
        //...this function returns Disease lists for the ArticleMetaData View - Disease Autocomplete
        public ActionResult GetDiseaseDataJson(string term)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            var response = db.Diseases
                                .Where(s => s.DiseaseName.Contains(term))
                                .Select(m => new
                                {
                                    diseaseId = m.DiseaseId,
                                    label = m.DiseaseName,
                                }).ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArticlesForEventGroupedByLocation(int? Id)
        {
            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
                var eventItem = db.Events.Find(Id);
                var DiseaseIdForEvent = eventItem.DiseaseId;
                var ProcessedArticleForEvent = eventItem.ProcessedArticles.Where(s => s.HamTypeId != 1).OrderByDescending(p => p.FeedPublishedDate).ToList();


                //..............................................Implemention of Event articles grouped by Locations.................
                List<EventArticlesCategorizedByLocations> articleListCategorizedbyUniqueLocations = new List<EventArticlesCategorizedByLocations>();

                //...Get unique locations defined by Events
                List<int> uniqueLocationIds = new List<int>();
                var eventLocations = db.Xtbl_Event_Location.Where(a => a.EventId == eventItem.EventId).ToList();

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
                    articleCategorizedByLoc.GeoLocationName = db.Geonames.Where(s => s.GeonameId == uniqueLocId).Select(m => m.DisplayName).SingleOrDefault();
                    articleCategorizedByLoc.Articles = new List<ArticleWithCaseCounts>();
                    articleListCategorizedbyUniqueLocations.Add(articleCategorizedByLoc);
                }

                //...placeholder list contains those articles that either does not have any geolocations or their defined geolocations does not match with Event's geolocation
                List<string> placeholder = new List<string>();
                List<string> articleIncluded = new List<string>();

                for (var i = 0; i < ProcessedArticleForEvent.Count; i++)
                {
                    var artID = ProcessedArticleForEvent.ElementAt(i).ArticleId;
                    var articleGeoIds = db.Xtbl_Article_Location_Disease.Where(a => a.ArticleId == artID).Select(g => g.LocationGeoNameId).ToList();

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
                                        //var uniqueLocKey = uniqueLocArticleGroup.Where(x => x.Key == locId).SingleOrDefault();
                                        //uniqueLocKey.Value.Add(artID);

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
                                        var recordforArticle = db.Xtbl_Article_Location_Disease.Where(s => s.ArticleId == articleId && s.LocationGeoNameId == unqlocId && s.DiseaseId == DiseaseIdForEvent).SingleOrDefault();

                                        Location loc = new Location();
                                        if (recordforArticle != null && unqlocId != -1)
                                        {
                                            loc.GeoID = recordforArticle.LocationGeoNameId;
                                            loc.GeoName = db.Geonames.Where(s => s.GeonameId == unqlocId).Select(m => m.DisplayName).SingleOrDefault();
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


        public ActionResult GetArticlesForSuggestedEventGroupedByLocation(string Id)
        {
            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
                var SuggestedEventItem = db.SuggestedEvents.Find(Id);
                //var DiseaseIdForEvent = eventItem.DiseaseId;
                var ProcessedArticleForSuggestedEvent = SuggestedEventItem.ProcessedArticles.ToList();


                //..............................................Implemention of Event articles grouped by Locations.................
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
                    articleCategorizedByLoc.GeoLocationName = db.Geonames.Where(s => s.GeonameId == uniqueLocId).Select(m => m.DisplayName).SingleOrDefault();
                    articleCategorizedByLoc.Articles = new List<ArticleWithCaseCounts>();
                    articleListCategorizedbyUniqueLocations.Add(articleCategorizedByLoc);
                }

                //...placeholder list contains those articles that either does not have any geolocations or their defined geolocations does not match with Event's geolocation
                List<string> placeholder = new List<string>();
                List<string> articleIncluded = new List<string>();

                for (var i = 0; i < ProcessedArticleForSuggestedEvent.Count; i++)
                {
                    var artID = ProcessedArticleForSuggestedEvent.ElementAt(i).ArticleId;
                    var articleGeoIds = db.Xtbl_Article_Location_Disease.Where(a => a.ArticleId == artID).Select(g => g.LocationGeoNameId).ToList();

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
                                        //var uniqueLocKey = uniqueLocArticleGroup.Where(x => x.Key == locId).SingleOrDefault();
                                        //uniqueLocKey.Value.Add(artID);

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
        public int CreateEventPost(EventUpdateModel eventCreate)
        {
            var currentDate = DateTime.Now;
            Event eventItem = new Event
            {
                SpeciesId = 1, // 1 = Human
                EventTitle = eventCreate.eventTitle ?? "Untitled event",
                DiseaseId = (eventCreate.diseaseID != null) ? int.Parse(eventCreate.diseaseID) : (int?)null,
                StartDate = eventCreate.startDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventCreate.startDate),
                EndDate = eventCreate.endDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventCreate.endDate),
                PriorityId = int.Parse(eventCreate.priorityID),
                CreatedDate = currentDate,
                LastUpdatedByUserName = User.Identity.Name,
                LastUpdatedDate = currentDate,
                Summary = eventCreate.summary ?? "",
                Notes = eventCreate.notes ?? "",
                IsLocalOnly = bool.Parse(eventCreate.alertRadius),
                IsPublished = bool.Parse(eventCreate.isPublished),
                IsPublishedChangesToApi = bool.Parse(eventCreate.isPublishedChangesToApi)
            };

            if (ModelState.IsValid)
            {
                dbContext.Events.Add(eventItem);
                dbContext.SaveChanges();

                return eventItem.EventId;
            }

            return 0;
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
            var model = new EventViewModel(dbContext, id);

            return PartialView(viewName, model);
        }

        [HttpPost]
        public int EventUpdate(EventUpdateModel eventModel)
        {
            int eventID = eventModel.eventID;
            var currentEvent = dbContext.Events.Where(s => s.EventId == eventID).SingleOrDefault();

            // Sync Event Metadata to Zebra if the event is published
            if (bool.Parse(eventModel.isPublished))
            {
                List<ArticleUpdateForZebra> articleList = currentEvent.ProcessedArticles
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
                var jsonArticle = serializer.Serialize(articleList);

                eventModel.associatedArticles = jsonArticle;
                eventModel.LastUpdatedByUserName = User.Identity.Name;

                var result = EventUpdateZebraApi(eventModel);
            }

            // Update entry in Surveillance DB
            currentEvent.EventTitle = eventModel.eventTitle;
            currentEvent.DiseaseId = int.Parse(eventModel.diseaseID);
            currentEvent.SpeciesId = int.Parse(eventModel.SpeciesID);
            currentEvent.StartDate = eventModel.startDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventModel.startDate);
            currentEvent.EndDate = eventModel.endDate.IsNullOrWhiteSpace() ? (DateTime?)null : DateTime.Parse(eventModel.endDate);
            currentEvent.PriorityId = int.Parse(eventModel.priorityID);
            currentEvent.LastUpdatedByUserName = User.Identity.Name;
            currentEvent.LastUpdatedDate = DateTime.Now;
            currentEvent.Summary = eventModel.summary;
            currentEvent.Notes = eventModel.notes;
            currentEvent.IsLocalOnly = bool.Parse(eventModel.alertRadius);
            currentEvent.IsPublished = bool.Parse(eventModel.isPublished);
            currentEvent.IsPublishedChangesToApi = false;

            // Change currentEventItem state to modified
            var eventItem = dbContext.Entry(currentEvent);
            eventItem.State = EntityState.Modified;

            // Load existing items for ManyToMany COllection
            eventItem.Collection(i => i.EventCreationReasons).Load();
            eventItem.Collection(i => i.Xtbl_Event_Location).Load();

            // Clear Event items
            currentEvent.EventCreationReasons.Clear();
            currentEvent.Xtbl_Event_Location.Clear();

            string[] selectedReasonIDs = (eventModel.reasonIDs[0] != "") ? eventModel.reasonIDs : null;
            if (selectedReasonIDs != null)
            {
                currentEvent.EventCreationReasons = dbContext.EventCreationReasons
                    .Where(r => selectedReasonIDs.Contains(r.ReasonId.ToString()))
                    .ToList();
            }

            var locationObject = JsonConvert.DeserializeObject<List<EventLocation>>(eventModel.locationObject);
            currentEvent.Xtbl_Event_Location = locationObject
                .Select(item => new Xtbl_Event_Location
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
                dbContext.SaveChanges();
                return currentEvent.EventId;
            }

            return 1;
        }

        static async Task<HttpResponseMessage> UpdateZebraEventCaseHistory(int eventId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApi");
                var requestUrl = "api/ZebraUpdateEventCaseHistory";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
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
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl_UAT);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
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
                return response;
            }
        }

        public async Task<ActionResult> PublishChangesToApi(EventUpdateModel eventmodel)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            int eventID = eventmodel.eventID;
            //...check if this event exists. If not, create this event first
            var eventExists = db.Events.Where(s => s.EventId == eventID).SingleOrDefault();

            //.....Saving to Surveillance DB
            String[] selectedReasonIDs = (eventmodel.reasonIDs[0] != "") ? eventmodel.reasonIDs : null;

            var currentEventItem = db.Events.Where(s => s.EventId == eventID).SingleOrDefault();

            currentEventItem.EventTitle = eventmodel.eventTitle;
            currentEventItem.LastUpdatedDate = DateTime.Now;

            var startDate = (eventmodel.startDate != "") ? eventmodel.startDate : null;
            currentEventItem.StartDate = (startDate != null) ? DateTime.Parse(startDate) : (DateTime?)null;

            var endDate = (eventmodel.endDate != "") ? eventmodel.endDate : null;
            currentEventItem.EndDate = (endDate != null) ? DateTime.Parse(endDate) : (DateTime?)null;

            currentEventItem.DiseaseId = int.Parse(eventmodel.diseaseID);
            currentEventItem.IsLocalOnly = bool.Parse(eventmodel.alertRadius);
            currentEventItem.PriorityId = int.Parse(eventmodel.priorityID);
            currentEventItem.Summary = eventmodel.summary;
            currentEventItem.Notes = eventmodel.notes;
            currentEventItem.IsPublished = bool.Parse(eventmodel.isPublished);
            currentEventItem.IsPublishedChangesToApi = true;
            currentEventItem.LastUpdatedByUserName = User.Identity.Name;

            var locationObject = JsonConvert.DeserializeObject<List<EventLocation>>(eventmodel.locationObject);

            // Change currentEventItem state to modified
            var eventItem = db.Entry(currentEventItem);
            eventItem.State = EntityState.Modified;

            //load existing items for ManyToMany COllection
            eventItem.Collection(i => i.EventCreationReasons).Load();
            eventItem.Collection(i => i.Xtbl_Event_Location).Load();

            //Clear Event items
            currentEventItem.EventCreationReasons.Clear();
            currentEventItem.Xtbl_Event_Location.Clear();

            if (selectedReasonIDs != null)
            {
                foreach (var reasonId in selectedReasonIDs)
                {
                    var reasonItem = db.EventCreationReasons.Find(int.Parse(reasonId));
                    currentEventItem.EventCreationReasons.Add(reasonItem);
                }
            }

            //Adding Xtbl_Event_Location
            foreach (var item in locationObject)
            {
                Xtbl_Event_Location evtLoc = new Xtbl_Event_Location();
                evtLoc.EventId = eventID;
                evtLoc.GeonameId = item.GeonameId;
                evtLoc.EventDate = item.EventDate;
                evtLoc.SuspCases = item.SuspCases;
                evtLoc.RepCases = item.RepCases;
                evtLoc.ConfCases = item.ConfCases;
                evtLoc.Deaths = item.Deaths;

                currentEventItem.Xtbl_Event_Location.Add(evtLoc);
            }

            if (ModelState.IsValid)
            {
                //Update Surveillance DB
                db.SaveChanges();

                //.....Sync to ZebraDB
                List<ArticleUpdateForZebra> articleList = new List<ArticleUpdateForZebra>();
                var articleLists = db.Events.Where(e => e.EventId == eventID).Select(s => s.ProcessedArticles).SingleOrDefault();

                for (var i = 0; i < articleLists.Count; i++)
                {
                    var m_item = new ArticleUpdateForZebra
                    {
                        ArticleId = articleLists.ElementAt(i).ArticleId,
                        ArticleTitle = articleLists.ElementAt(i).ArticleTitle,
                        SystemLastModifiedDate = articleLists.ElementAt(i).SystemLastModifiedDate,
                        CertaintyScore = articleLists.ElementAt(i).CertaintyScore,
                        ArticleFeedId = articleLists.ElementAt(i).ArticleFeedId,
                        FeedURL = articleLists.ElementAt(i).FeedURL,
                        FeedSourceId = articleLists.ElementAt(i).FeedSourceId,
                        FeedPublishedDate = articleLists.ElementAt(i).FeedPublishedDate,
                        HamTypeId = articleLists.ElementAt(i).HamTypeId,
                        OriginalSourceURL = articleLists.ElementAt(i).OriginalSourceURL,
                        IsCompleted = articleLists.ElementAt(i).IsCompleted,
                        SimilarClusterId = articleLists.ElementAt(i).SimilarClusterId,
                        OriginalLanguage = articleLists.ElementAt(i).OriginalLanguage,
                        UserLastModifiedDate = articleLists.ElementAt(i).UserLastModifiedDate,
                        LastUpdatedByUserName = articleLists.ElementAt(i).LastUpdatedByUserName,
                        Notes = articleLists.ElementAt(i).Notes,
                        ArticleBody = null, //article body increases payload
                        IsRead = articleLists.ElementAt(i).IsRead,
                        DiseaseObject = "",
                        SelectedPublishedEventIds = new List<int>()
                    };
                    articleList.Add(m_item);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue;
                var jsonArticle = serializer.Serialize(articleList);
                eventmodel.associatedArticles = jsonArticle;
                eventmodel.LastUpdatedByUserName = User.Identity.Name;

                // 1....Update Event for ZebraDB if there is changes in the MetaData
                var resultUpdate = await EventUpdateZebraApi(eventmodel);

                if (resultUpdate.IsSuccessStatusCode)
                {
                    //2. Send PROXIMAL email
                    var response = SendProximalEmailNotification(eventmodel.eventID);
                }

                return Json("success");
            }
            else
            {
                return Json("Model is not valid!");
            }
        }

        static async Task<HttpResponseMessage> EventUpdateZebraApi(EventUpdateModel eventModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApi");
                var requestUrl = "api/ZebraEventUpdate";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    response = await client.PostAsJsonAsync(requestUrl, eventModel);

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
                    var requestUrl_UAT = "api/ZebraEventUpdate";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl_UAT);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                        responseUAT = await client.PostAsJsonAsync(requestUrl_UAT, eventModel);

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

        public async Task<ActionResult> EventPublishAsync(EventUpdateModel eventmodel)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            //int responseResult = 0;
            string responseResult = "";
            Logging.Log("Entered EventPublishAsync controller");

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                //string result = String.Empty;
                HttpResponseMessage result = new HttpResponseMessage();
                try
                {
                    // 1. Save Event metadata to Survaillence DB
                    var eventID = EventUpdate(eventmodel);
                    Logging.Log("Step 1");

                    if (eventID != 0)
                    {
                        // 2. Save to Zebra DB. On success, update "IsPublished" property in Surveillance DB
                        //Logging.Log("Step 2");

                        //...Get Associated Article Info if there is any
                        List<ArticleUpdateForZebra> articleList = new List<ArticleUpdateForZebra>();

                        var articleLists = db.Events.Where(e => e.EventId == eventID).Select(s => s.ProcessedArticles).SingleOrDefault();
                        for (var i = 0; i < articleLists.Count; i++)
                        {
                            var m_item = new ArticleUpdateForZebra
                            {
                                ArticleId = articleLists.ElementAt(i).ArticleId,
                                ArticleTitle = articleLists.ElementAt(i).ArticleTitle,
                                SystemLastModifiedDate = articleLists.ElementAt(i).SystemLastModifiedDate,
                                CertaintyScore = articleLists.ElementAt(i).CertaintyScore,
                                ArticleFeedId = articleLists.ElementAt(i).ArticleFeedId,
                                FeedURL = articleLists.ElementAt(i).FeedURL,
                                FeedSourceId = articleLists.ElementAt(i).FeedSourceId,
                                FeedPublishedDate = articleLists.ElementAt(i).FeedPublishedDate,
                                HamTypeId = articleLists.ElementAt(i).HamTypeId,
                                OriginalSourceURL = articleLists.ElementAt(i).OriginalSourceURL,
                                IsCompleted = articleLists.ElementAt(i).IsCompleted,
                                SimilarClusterId = articleLists.ElementAt(i).SimilarClusterId,
                                OriginalLanguage = articleLists.ElementAt(i).OriginalLanguage,
                                UserLastModifiedDate = articleLists.ElementAt(i).UserLastModifiedDate,
                                LastUpdatedByUserName = articleLists.ElementAt(i).LastUpdatedByUserName,
                                Notes = articleLists.ElementAt(i).Notes,
                                ArticleBody = null, //article body increases payload
                                IsRead = articleLists.ElementAt(i).IsRead,
                                DiseaseObject = "",
                                SelectedPublishedEventIds = new List<int>()
                            };
                            articleList.Add(m_item);
                        }

                        var jsonSerialiser = new JavaScriptSerializer();
                        var jsonArticle = jsonSerialiser.Serialize(articleList);
                        eventmodel.associatedArticles = jsonArticle;
                        eventmodel.LastUpdatedByUserName = User.Identity.Name;
                        //Logging.Log("Step 3");
                        result = await EventUpdateZebraApi(eventmodel);
                        if (result.IsSuccessStatusCode)
                        {
                            //Logging.Log("Step 6");
                            // 1. update "IsPublished" property of Surveillance DB

                            Event evt = db.Events.Find(eventID);
                            evt.IsPublished = true;
                            if (ModelState.IsValid)
                            {
                                db.SaveChanges();
                            }
                            //Logging.Log("Published");
                            responseResult = "success";

                            // 3. Send Zebra Email notification
                            var response = SendEventEmailNotification(eventID);
                        }
                    }
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    responseResult = "Error: " + ex.Message;
                    Logging.Log("Failed");
                    dbContextTransaction.Rollback();
                }
            }
            return Json(responseResult);
        }


        public async Task<ActionResult> SendEventEmailNotification(int eventID)
        {
            try
            {
                //...Local
                var baseUrlLocal = ConfigurationManager.AppSettings.Get("ZebraEmailUsersLocatedInEventAreaApi");
                var requestUrlLocal = baseUrlLocal + "?EventId=" + eventID;
                string resultStringLocal = string.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrlLocal);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var byteArray = Encoding.ASCII.GetBytes("bluedot\\api:BlueD0t2018$");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                    //HttpResponseMessage response = client.GetAsync(requestUrlLocal).Result;
                    HttpResponseMessage response = await client.GetAsync(requestUrlLocal).ConfigureAwait(false); //alternative solution - this also doesn't block Async code
                    if (response.IsSuccessStatusCode)
                    {
                        resultStringLocal = await response.Content.ReadAsStringAsync();
                    }
                }

                //...Destination
                var baseUrlDest = ConfigurationManager.AppSettings.Get("ZebraEmailUsersLocatedInEventDestinationAreaApi");
                var requestUrlDest = baseUrlDest + "?EventId=" + eventID;
                string resultStringDest = string.Empty;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrlDest);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var byteArray = Encoding.ASCII.GetBytes("bluedot\\api:BlueD0t2018$");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                    //HttpResponseMessage response = client.GetAsync(requestUrlDest).Result;
                    HttpResponseMessage response = await client.GetAsync(requestUrlDest).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        resultStringDest = await response.Content.ReadAsStringAsync();
                    }
                }

                //...Local UAT Publish 
                if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsProduction")))
                {
                    //Local
                    var baseUrlLocal_UAT = ConfigurationManager.AppSettings.Get("ZebraEmailUsersLocatedInEventAreaApiUAT");
                    var requestUrlLocal_UAT = baseUrlLocal_UAT + "?EventId=" + eventID;
                    string resultStringLocal_UAT = string.Empty;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrlLocal_UAT);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var byteArray = Encoding.ASCII.GetBytes("bluedot\\api:BlueD0t2018$");
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                        client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                        client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                        //HttpResponseMessage response = client.GetAsync(requestUrlLocal_UAT).Result;
                        HttpResponseMessage response = await client.GetAsync(requestUrlLocal_UAT).ConfigureAwait(false); //alternative solution - this also doesn't block Async code. ConfigureAwait(false) configures the task so that continuation after the await does not have to be run in the caller context, therefore avoiding any possible deadlocks.

                        if (response.IsSuccessStatusCode)
                        {
                            resultStringLocal_UAT = await response.Content.ReadAsStringAsync();
                        }
                    }

                    //...Destination UAT publish
                    var baseUrlDest_UAT = ConfigurationManager.AppSettings.Get("ZebraEmailUsersLocatedInEventDestinationAreaApiUAT");
                    var requestUrlDest_UAT = baseUrlDest_UAT + "?EventId=" + eventID;
                    string resultStringDest_UAT = string.Empty;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrlDest_UAT);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var byteArray = Encoding.ASCII.GetBytes("bluedot\\api:BlueD0t2018$");
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                        client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                        client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                        //HttpResponseMessage response = client.GetAsync(requestUrlDest_UAT).Result;
                        HttpResponseMessage response = await client.GetAsync(requestUrlDest_UAT).ConfigureAwait(false);

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
                var requestUrlProximal = baseUrlProximal + "?EventId=" + eventId;
                string resultStringProximal = string.Empty;
                string responseResult = "success";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrlProximal);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var byteArray = Encoding.ASCII.GetBytes("bluedot\\api:BlueD0t2018$");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                    HttpResponseMessage response = await client.GetAsync(requestUrlProximal).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        resultStringProximal = await response.Content.ReadAsStringAsync();
                    }
                }

                //Send the proximal email to UAT if the environment is production
                if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsProduction")))
                {
                    var baseUrlProximal_UAT = ConfigurationManager.AppSettings.Get("ZebraEmailUsersProximalEmailaApiUAT");
                    var requestUrlLocal_UAT = baseUrlProximal_UAT + "?EventId=" + eventId;
                    string resultStringProximal_UAT = string.Empty;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrlProximal_UAT);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var byteArray = Encoding.ASCII.GetBytes("bluedot\\api:BlueD0t2018$");
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                        client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                        client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
                        HttpResponseMessage response = await client.GetAsync(requestUrlLocal_UAT).ConfigureAwait(false);
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
                var eventPriority = db.EventPriorities.ToList();
                for (var i = 0; i < eventPriority.Count; i++)
                {
                    eventPrioritiesKvp.Add(new KeyValuePair<int, string>(eventPriority.ElementAt(i).PriorityId, eventPriority.ElementAt(i).PriorityTitle));
                }

                var eventsOnDev = db.Events.ToList();
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

        public async Task<ActionResult> EventSynchMongoDB(EventUpdateModel eventmodel, Boolean isPublishing)
        {
            using (var db = new BiodSurveillanceDataEntities())
            {

                var responseResult = "";
                //var eventToSync = int.Parse(eventmodel.eventID);
                var eventToSync = eventmodel.eventID;
                var eventDetails = db.Events.Find(eventToSync);
                var eventID = eventDetails.EventId;
                var articleLists = db.Events.Where(e => e.EventId == eventID).Select(s => s.ProcessedArticles).SingleOrDefault();
                List<string> articleIdsAssociated = new List<string>();
                foreach (var art in articleLists)
                {
                    articleIdsAssociated.Add(art.ArticleId);
                }


                var priorityId = int.Parse(eventmodel.priorityID);
                var priorityTitle = db.EventPriorities.Where(p => p.PriorityId == priorityId).Select(s => s.PriorityTitle).SingleOrDefault();

                String[] selectedReasonIDs = (eventmodel.reasonIDs[0] != "") ? eventmodel.reasonIDs : null;
                List<string> selectedReasonTitles = new List<string>();
                if (selectedReasonIDs != null)
                {
                    foreach (var reasonId in selectedReasonIDs)
                    {
                        var reasonItem = db.EventCreationReasons.Find(int.Parse(reasonId));
                        selectedReasonTitles.Add(reasonItem.ReasonName);
                    }
                };

                EventMetadataSyncMongoDB evtMongo = new EventMetadataSyncMongoDB();
                //evtMongo.eventId = int.Parse(eventmodel.eventID);
                evtMongo.eventId = eventmodel.eventID;
                evtMongo.eventName = !String.IsNullOrEmpty(eventmodel.eventTitle) ? eventmodel.eventTitle : null;
                evtMongo.priority = priorityTitle;
                evtMongo.summary = eventmodel.summary;
                evtMongo.startDate = !String.IsNullOrEmpty(eventmodel.startDate) ? eventmodel.startDate : "";
                evtMongo.endDate = !String.IsNullOrEmpty(eventmodel.endDate) ? eventmodel.endDate : "";
                evtMongo.diseaseId = !String.IsNullOrEmpty(eventmodel.diseaseID) ? Convert.ToInt32(eventmodel.diseaseID) : (Int32?)null;
                evtMongo.localOnly = bool.Parse(eventmodel.alertRadius);
                evtMongo.approvedForPublishing = eventDetails.IsPublished;
                //evtMongo.publishedDate = (!isPublishing) ? "" : DateTime.UtcNow.ToString("o"); //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                evtMongo.publishedDate = (isPublishing) ? DateTime.UtcNow.ToString("o") : ""; //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                evtMongo.reasonForCreation = selectedReasonTitles;
                evtMongo.notes = !String.IsNullOrEmpty(eventmodel.notes) ? eventmodel.notes : null;

                List<EventCaseCountMongo> locations = new List<EventCaseCountMongo>();
                if (!String.IsNullOrEmpty(eventmodel.locationObject))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var locObjArr = JsonConvert.DeserializeObject<List<EventLocation>>(eventmodel.locationObject);

                    var locationGroups = locObjArr.GroupBy(x => x.GeonameId);
                    foreach (var group in locationGroups)
                    {
                        EventCaseCountMongo location = new EventCaseCountMongo();
                        location.geonameId = group.Key;

                        List<EventCaseCountsByDateMongo> caseCounts = new List<EventCaseCountsByDateMongo>();
                        foreach (var item in group)
                        {
                            EventCaseCountsByDateMongo caseCount = new EventCaseCountsByDateMongo();
                            caseCount.date = item.EventDate.ToString("yyyy-MM-dd");
                            caseCount.suspected = item.SuspCases;
                            caseCount.confirmed = item.ConfCases;
                            caseCount.reported = item.RepCases;
                            caseCount.deaths = item.Deaths;
                            caseCounts.Add(caseCount);
                        }
                        location.caseCountsByDate = caseCounts;
                        locations.Add(location);
                    }
                }
                evtMongo.locations = locations;
                evtMongo.associatedArticleIds = articleIdsAssociated;

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
                return Json(responseResult);

            }
        }

        static async Task<HttpResponseMessage> ArticleUpdateZebraApi(ArticleUpdateForZebra articleModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApi");
                var requestUrl = "api/ZebraArticleUpdate";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //var byteArray = Encoding.ASCII.GetBytes("bluedot\\api:BlueD0t2018$");
                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
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
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl_UAT);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
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
                    var articleItem = dbContext.ProcessedArticles
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
            var articleItemUpdated = db.ProcessedArticles.Find(articleId);

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
            var eventItem = (from e in db.Events
                             where e.EventId == evtID
                             select e).SingleOrDefault();

            db.Events.Remove(eventItem);
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
            var model = new EventViewModel(dbContext, id);

            return PartialView("_EventDialogWindow", model);
        }

        public ActionResult GetEventEmptyModelWindowOnCreate()
        {
            var model = new EventViewModel(dbContext, -1);

            return PartialView("_EventDialogWindow", model);
        }
        #endregion

        #region Tested server-side paging using LINQ
        //............................................Testing Server-side paging for retrieving article list Using LINQ solution ....................................*/
        public JsonResult TestInitialArticleList(string articleType, int draw, int length, int start, string filterObj, string searchString)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            var old = DateTime.Now.AddMonths(-12); // 1 year old

            List<ArticleGridWithSimilarCluster> finalParentArticleList = new List<ArticleGridWithSimilarCluster>();
            List<IGrouping<Decimal?, ProcessedArticle>> articleGroupResult = new List<IGrouping<Decimal?, ProcessedArticle>>();


            if (articleType == "all")
            {
                articleGroupResult = (from r in db.ProcessedArticles
                                      where r.HamTypeId != 1 &&
                                               (r.FeedPublishedDate >= old)
                                      //orderby r.FeedPublishedDate descending
                                      group r by r.SimilarClusterId).ToList();

            }
            else if (articleType == "unprocessed")
            {
                articleGroupResult = (from r in db.ProcessedArticles
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

            finalParentArticleList = GetParentArticleList(articleGroupResult);
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

        public List<ArticleGridWithSimilarCluster> GetParentArticleList(List<IGrouping<Decimal?, ProcessedArticle>> articleGroupResult)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            var articleFeedKvp = new List<KeyValuePair<int, string>>();

            var articleFeed = db.ArticleFeeds.ToList();
            for (var i = 0; i < articleFeed.Count; i++)
            {
                articleFeedKvp.Add(new KeyValuePair<int, string>(articleFeed.ElementAt(i).ArticleFeedId, articleFeed.ElementAt(i).ArticleFeedName));
            }

            List<ArticleGridWithSimilarCluster> finalArticleList = new List<ArticleGridWithSimilarCluster>();

            foreach (var articleGroup in articleGroupResult)
            {

                //sort article group by feedPublishDate
                var articleGroupSorted = articleGroup.OrderByDescending(d => d.FeedPublishedDate);


                if (articleGroup.Key != null && articleGroup.Key > 0)
                {

                    ArticleGridWithSimilarCluster parentArticle = new ArticleGridWithSimilarCluster();
                    //List<ArticleBase> childArticleList = new List<ArticleBase>();

                    var parentItem = articleGroupSorted.First();

                    parentArticle.ArticleId = parentItem.ArticleId;
                    parentArticle.ArticleTitle = parentItem.ArticleTitle;
                    parentArticle.SimilarClusterId = parentItem.SimilarClusterId;
                    parentArticle.IsCompleted = parentItem.IsCompleted;
                    parentArticle.IsRead = parentItem.IsRead;
                    parentArticle.FeedPublishedDate = parentItem.FeedPublishedDate;
                    parentArticle.FeedPublishedDateToString = parentItem.FeedPublishedDate.ToString("yyyy-MM-dd"); //feed published dates are converted to String for easy displaying on the front-end through accessing the Model
                    parentArticle.ArticleFeedId = parentItem.ArticleFeedId;
                    parentArticle.ArticleFeedName = articleFeedKvp.First(kvp => kvp.Key == parentItem.ArticleFeedId).Value;
                    parentArticle.HasChildArticle = (articleGroup.Count() > 1) ? true : false;
                    finalArticleList.Add(parentArticle);
                }
                else
                {  // i.e. SimilarClusterId == NULL or Negative

                    foreach (var n in articleGroupSorted)
                    {
                        ArticleGridWithSimilarCluster parentArticleNoCluster = new ArticleGridWithSimilarCluster();
                        List<ArticleBase> childArticleList = new List<ArticleBase>();

                        parentArticleNoCluster.ArticleId = n.ArticleId;
                        parentArticleNoCluster.ArticleTitle = n.ArticleTitle;
                        parentArticleNoCluster.SimilarClusterId = n.SimilarClusterId;
                        parentArticleNoCluster.IsCompleted = n.IsCompleted;
                        parentArticleNoCluster.IsRead = n.IsRead;
                        parentArticleNoCluster.FeedPublishedDate = n.FeedPublishedDate;
                        parentArticleNoCluster.FeedPublishedDateToString = n.FeedPublishedDate.ToString("yyyy-MM-dd");
                        parentArticleNoCluster.ArticleFeedId = n.ArticleFeedId;
                        parentArticleNoCluster.ArticleFeedName = articleFeedKvp.First(kvp => kvp.Key == n.ArticleFeedId).Value;
                        finalArticleList.Add(parentArticleNoCluster);
                    }
                }
            }

            //sort finalArticleList by FeedPubDate
            var finalArticleListSortd = finalArticleList.OrderByDescending(d => d.FeedPublishedDate).ToList();

            return finalArticleListSortd;
        }

        public JsonResult GetChildArticleList(string articleType, string parentArticleId, Decimal? similarClusterId)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            List<ArticleBase> childArticleList = new List<ArticleBase>();

            var articleFeedKvp = new List<KeyValuePair<int, string>>();

            var articleFeed = db.ArticleFeeds.ToList();
            for (var i = 0; i < articleFeed.Count; i++)
            {
                articleFeedKvp.Add(new KeyValuePair<int, string>(articleFeed.ElementAt(i).ArticleFeedId, articleFeed.ElementAt(i).ArticleFeedName));
            }


            var old = DateTime.Now.AddMonths(-12); // 1 year old
                                                   //List<ProcessedArticle> childItems = (from r in db.ProcessedArticles
                                                   //                                     where r.HamTypeId != 1 && (r.FeedPublishedDate >= old) && r.SimilarClusterId == SimilarClusterId && r.ArticleId != articleId
                                                   //                                     select r).ToList();

            List<ProcessedArticle> childItems = new List<ProcessedArticle>();
            if (articleType == "all")
            {
                childItems = (from r in db.ProcessedArticles
                              where r.HamTypeId != 1 &&
                                       (r.FeedPublishedDate >= old) && r.SimilarClusterId == similarClusterId && r.ArticleId != parentArticleId
                              select r).ToList();

            }
            else if (articleType == "unprocessed")
            {
                childItems = (from r in db.ProcessedArticles
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
        public JsonResult GetParentArticleListSP(string articleType, int draw, int length, int start, string filterObj, string searchString)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            IEnumerable<usp_GetParentArticleList_Result> parentArticleList = null;
            IEnumerable<usp_GetParentArticleListByFilters_Result> parentArticleListByFilter = null;

            try
            {
                if (string.IsNullOrEmpty(filterObj))
                {
                    parentArticleList = db.usp_GetParentArticleList(articleType, start, length, searchString).ToList();
                    //parentArticleList = (TempData["ArticleListInitial"] != null) ? (IEnumerable<usp_GetParentArticleList_Result>)TempData["ArticleListInitial"] : db.usp_GetParentArticleList(articleType, start, length, searchString).ToList();
                    var totalRecords = db.usp_GetTotalArticleRecords(articleType, searchString).FirstOrDefault().Value;

                    return Json(new
                    {
                        datasource = parentArticleList,
                        drawTable = draw,
                        recordsTotal = totalRecords,
                        recordsFiltered = totalRecords
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var filterArray = filterObj.Split('&');
                    var selectedArticleType = filterArray[0];
                    var startDate = string.IsNullOrEmpty(filterArray[1]) ? DateTime.Parse("1900-01-01") : DateTime.Parse(filterArray[1]); //(DateTime?) null  // ("1900-01-01") represents null for SP
                    var endDate = string.IsNullOrEmpty(filterArray[2]) ? DateTime.Parse("1900-01-01") : DateTime.Parse(filterArray[2]);
                    var HampType = (filterArray[3] == "0") ? "2,3" : filterArray[3];
                    var articleSourceIds = (filterArray[4] == "null") ? "" : filterArray[4];
                    var selectedLocationIds = filterArray[5];  //""
                    string locationIds = (selectedLocationIds != "") ? string.Join(",", selectedLocationIds.Split(';').Select(str => int.Parse(str.Split('|').ElementAt(0))).ToList()) : "";
                    var articleDiseaseIds = (filterArray[6] == "null") ? "" : filterArray[6];

                    parentArticleListByFilter = db.usp_GetParentArticleListByFilters(articleType, start, length, startDate, endDate, HampType, articleSourceIds, articleDiseaseIds, locationIds, searchString).ToList();
                    var totalRecordsByFilter = db.usp_GetTotalArticleRecordsByFilters(articleType, startDate, endDate, HampType, articleSourceIds, articleDiseaseIds, locationIds, searchString).FirstOrDefault().Value;

                    JavaScriptSerializer js1 = new JavaScriptSerializer();
                    return Json(new
                    {
                        datasource = parentArticleListByFilter,
                        drawTable = draw,
                        recordsTotal = totalRecordsByFilter,
                        recordsFiltered = totalRecordsByFilter
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
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            SuggestedEvent suggestedEvt = db.SuggestedEvents.Find(suggestedEventId);

            SuggestedEventViewModel suggestedEventItem = new SuggestedEventViewModel();
            suggestedEventItem.EventId = suggestedEventId;
            suggestedEventItem.EventTitle = suggestedEvt.EventTitle;
            suggestedEventItem.StartDate = null;
            suggestedEventItem.EndDate = null;
            suggestedEventItem.AssociatedArticle = null;

            return PartialView(viewName, suggestedEventItem);
        }

        [HttpGet]
        public ActionResult SuggestedEventDialogView(string suggestedEventId)
        {
            var model = new SuggestedEventDialogViewModel
            {
                EventInfo = SuggestedEventDialogViewModel.GetSuggestedEventById(suggestedEventId),
                locations = SuggestedEventDialogViewModel.GetSuggestedEventLocationById(suggestedEventId),
                Diseases = SuggestedEventDialogViewModel.GetDiseases()
            };
            model.EventReasonMultiSelect = SuggestedEventDialogViewModel.GetEventReasonsById(model.EventInfo.EventId);
            model.EventPriorities = SuggestedEventDialogViewModel.GetEventPriorities();
            return PartialView("_SuggestedEventDialogView", model);
        }

        [HttpPost]
        public int SaveSuggestedEventOnDialogWindow(SuggestedEventModel suggestedEventItemInfo)
        {
            //1. Create the event
            //2. Update the event
            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
                var suggestedEventItem = db.SuggestedEvents.Where(e => e.SuggestedEventId == suggestedEventItemInfo.eventID).SingleOrDefault();

                //...Adds it to Real Event table
                Event realEventItem = new Event();

                realEventItem.EventTitle = suggestedEventItemInfo.eventTitle;
                var startDate = (suggestedEventItemInfo.startDate != "") ? suggestedEventItemInfo.startDate : null;
                realEventItem.StartDate = (startDate != null) ? DateTime.Parse(startDate) : (DateTime?)null;
                var endDate = (suggestedEventItemInfo.endDate != "") ? suggestedEventItemInfo.endDate : null;
                realEventItem.EndDate = (endDate != null) ? DateTime.Parse(endDate) : (DateTime?)null;
                realEventItem.CreatedDate = DateTime.Now;
                realEventItem.DiseaseId = suggestedEventItemInfo.diseaseID;
                realEventItem.IsLocalOnly = suggestedEventItemInfo.alertRadius;
                realEventItem.PriorityId = suggestedEventItemInfo.priorityID;
                realEventItem.IsPublished = suggestedEventItemInfo.isPublished;
                realEventItem.Summary = suggestedEventItemInfo.summary;
                realEventItem.Notes = suggestedEventItemInfo.notes;
                realEventItem.LastUpdatedByUserName = User.Identity.Name;
                realEventItem.LastUpdatedDate = DateTime.Now;

                if (suggestedEventItem.ProcessedArticles.Count != 0)
                {
                    foreach (var article in suggestedEventItem.ProcessedArticles)
                    {
                        realEventItem.ProcessedArticles.Add(article);
                    }
                }


                if (ModelState.IsValid)
                {
                    //...Adds to Events table
                    db.Events.Add(realEventItem);

                    //...Removes from SuggestedEvents table
                    db.SuggestedEvents.Remove(suggestedEventItem);
                    db.SaveChanges();
                    var realEventID = realEventItem.EventId;

                    var currentEventItem = db.Events.Find(realEventID);

                    //...Update event Reasons and Locations
                    var locationObject = JsonConvert.DeserializeObject<List<EventLocation>>(suggestedEventItemInfo.locationObject);

                    //....Change currentEventItem state to modified
                    var eventItem = db.Entry(currentEventItem);
                    eventItem.State = EntityState.Modified;

                    //...load existing items for ManyToMany COllection
                    eventItem.Collection(i => i.EventCreationReasons).Load();
                    eventItem.Collection(i => i.Xtbl_Event_Location).Load();

                    //...Clear Event items
                    currentEventItem.EventCreationReasons.Clear();
                    currentEventItem.Xtbl_Event_Location.Clear();

                    String[] selectedReasonIDs = (suggestedEventItemInfo.reasonIDs[0] != "") ? suggestedEventItemInfo.reasonIDs : null;
                    if (selectedReasonIDs != null)
                    {
                        foreach (var reasonId in selectedReasonIDs)
                        {
                            var reasonItem = db.EventCreationReasons.Find(int.Parse(reasonId));
                            currentEventItem.EventCreationReasons.Add(reasonItem);
                        }
                    }

                    //Adding Xtbl_Event_Location
                    foreach (var item in locationObject)
                    {
                        Xtbl_Event_Location evtLoc = new Xtbl_Event_Location();
                        evtLoc.EventId = currentEventItem.EventId;
                        evtLoc.GeonameId = item.GeonameId;
                        evtLoc.EventDate = item.EventDate;
                        evtLoc.SuspCases = item.SuspCases;
                        evtLoc.RepCases = item.RepCases;
                        evtLoc.ConfCases = item.ConfCases;
                        evtLoc.Deaths = item.Deaths;

                        currentEventItem.Xtbl_Event_Location.Add(evtLoc);
                    }

                    if (ModelState.IsValid)
                    {
                        db.SaveChanges();

                        //Sync to MongoDB
                        EventUpdateModel eventmodel = GetMongodbModelForSuggestedEvent(currentEventItem, suggestedEventItemInfo);
                        var mongoSync = EventSynchMongoDB(eventmodel, false);

                        //...User Action Update in the API
                        SuggestedEventUserAction userAction = new SuggestedEventUserAction();
                        userAction.userAction = (int)EnumUserAction.UserActions.Created;
                        List<string> arrRejectionReasons = new List<string>();
                        userAction.reasonsForRejection = arrRejectionReasons;
                        userAction.eventId = currentEventItem.EventId;
                        Task<HttpResponseMessage> response = UpdateUserActiononSuggestedEvent(suggestedEventItemInfo.eventID, userAction);

                        return currentEventItem.EventId;
                    }
                    // return realEventID;
                }
                return 0;
            }
            catch (Exception Ex)
            {
                Logging.Log("Error: " + Ex.Message + "\n" + Ex.InnerException);
                return 0;
            }
        }

        [HttpPost]
        public int QuickSaveSuggestedEvent(string suggestedEventId)
        {
            //1. Create the event
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            var suggestedEventItem = db.SuggestedEvents.Where(e => e.SuggestedEventId == suggestedEventId).SingleOrDefault();

            //...Adds it to Real Event table
            Event realEventItem = new Event();

            realEventItem.EventTitle = suggestedEventItem.EventTitle;
            realEventItem.StartDate = null;
            realEventItem.EndDate = null;
            realEventItem.CreatedDate = DateTime.Now;
            realEventItem.DiseaseId = suggestedEventItem.DiseaseId;
            realEventItem.IsLocalOnly = true;
            realEventItem.PriorityId = 2;
            realEventItem.IsPublished = false;
            realEventItem.Summary = "";
            realEventItem.Notes = "";
            realEventItem.LastUpdatedDate = DateTime.Now;
            realEventItem.LastUpdatedByUserName = User.Identity.Name;

            if (suggestedEventItem.ProcessedArticles.Count != 0)
            {
                foreach (var article in suggestedEventItem.ProcessedArticles)
                {
                    realEventItem.ProcessedArticles.Add(article);
                }
            }

            if (ModelState.IsValid)
            {
                //...Adds to Events table
                db.Events.Add(realEventItem);

                //...Removes from SuggestedEvents table
                db.SuggestedEvents.Remove(suggestedEventItem);
                db.SaveChanges();


                //Sync to MongoDB
                var eventmodel = GetMongodbModelForRealEvent(realEventItem);
                var mongoSync = EventSynchMongoDB(eventmodel, false);


                //...User Action Update in the API
                SuggestedEventUserAction userAction = new SuggestedEventUserAction();
                userAction.userAction = (int)EnumUserAction.UserActions.Created;
                List<string> arrRejectionReasons = new List<string>();
                userAction.reasonsForRejection = arrRejectionReasons;
                userAction.eventId = realEventItem.EventId;
                Task<HttpResponseMessage> response = UpdateUserActiononSuggestedEvent(suggestedEventId, userAction);

                var realEventID = realEventItem.EventId;
                return realEventID;
            }
            return 0;
        }

        [HttpPost]
        public async Task<ActionResult> PublishSuggestedEventOnDialogWindow(SuggestedEventModel suggestedEventItemInfo)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            string responseResult = "";
            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

                //1. Create the event first with all metadata
                var realEventId = SaveSuggestedEventOnDialogWindow(suggestedEventItemInfo);
                String[] selectedReasonIDs = (suggestedEventItemInfo.reasonIDs[0] != "") ? suggestedEventItemInfo.reasonIDs : null;

                //2. Publish the real event on Zebra
                if (realEventId != 0)
                {
                    var realEvent = db.Events.Find(realEventId);

                    EventUpdateModel eventmodel = GetMongodbModelForSuggestedEvent(realEvent, suggestedEventItemInfo);
                    result = await EventUpdateZebraApi(eventmodel);
                    if (result.IsSuccessStatusCode)
                    {
                        //...Update "IsPublished" property of Surveillance DB
                        Event evt = db.Events.Find(realEventId);
                        evt.IsPublished = true;
                        if (ModelState.IsValid)
                        {
                            db.SaveChanges();
                            responseResult = "success";
                        }
                        //...Send Zebra Email notification
                        var responseEmail = SendEventEmailNotification(realEventId);

                        //Sync to MongoDB
                        var mongoSync = EventSynchMongoDB(eventmodel, true);

                        //...User Action Update in the API
                        SuggestedEventUserAction userAction = new SuggestedEventUserAction();
                        userAction.userAction = (int)EnumUserAction.UserActions.Created;
                        List<string> arrRejectionReasons = new List<string>();
                        userAction.reasonsForRejection = arrRejectionReasons;
                        userAction.eventId = realEventId;
                        Task<HttpResponseMessage> response = UpdateUserActiononSuggestedEvent(suggestedEventItemInfo.eventID, userAction);
                    }
                }
                return Json(responseResult);
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                responseResult = "Failed!";
                return Json(responseResult);
            }
        }

        public EventUpdateModel GetMongodbModelForRealEvent(Event realEventItem)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            EventUpdateModel eventmodel = new EventUpdateModel();
            eventmodel.eventID = realEventItem.EventId;
            eventmodel.eventTitle = realEventItem.EventTitle;
            eventmodel.startDate = (realEventItem.StartDate != null) ? realEventItem.StartDate.Value.ToString("yyyy-MM-dd") : null;
            eventmodel.endDate = (realEventItem.EndDate != null) ? realEventItem.EndDate.Value.ToString("yyyy-MM-dd") : null;
            eventmodel.diseaseID = realEventItem.DiseaseId.ToString();
            var reason = new List<String>();
            reason.Add("");
            eventmodel.reasonIDs = reason.ToArray();
            eventmodel.alertRadius = realEventItem.IsLocalOnly.ToString();
            eventmodel.priorityID = realEventItem.PriorityId.ToString();
            eventmodel.isPublished = realEventItem.IsPublished.ToString();
            eventmodel.summary = realEventItem.Summary;
            eventmodel.notes = realEventItem.Notes;
            eventmodel.locationObject = "";

            //...Get Associated Article Info if there is any
            List<ArticleUpdateForZebra> articleList = new List<ArticleUpdateForZebra>();

            var articleLists = db.Events.Where(e => e.EventId == realEventItem.EventId).Select(s => s.ProcessedArticles).SingleOrDefault();
            for (var i = 0; i < articleLists.Count; i++)
            {
                var m_item = new ArticleUpdateForZebra
                {
                    ArticleId = articleLists.ElementAt(i).ArticleId,
                    ArticleTitle = articleLists.ElementAt(i).ArticleTitle,
                    SystemLastModifiedDate = articleLists.ElementAt(i).SystemLastModifiedDate,
                    CertaintyScore = articleLists.ElementAt(i).CertaintyScore,
                    ArticleFeedId = articleLists.ElementAt(i).ArticleFeedId,
                    FeedURL = articleLists.ElementAt(i).FeedURL,
                    FeedSourceId = articleLists.ElementAt(i).FeedSourceId,
                    FeedPublishedDate = articleLists.ElementAt(i).FeedPublishedDate,
                    HamTypeId = articleLists.ElementAt(i).HamTypeId,
                    OriginalSourceURL = articleLists.ElementAt(i).OriginalSourceURL,
                    IsCompleted = articleLists.ElementAt(i).IsCompleted,
                    SimilarClusterId = articleLists.ElementAt(i).SimilarClusterId,
                    OriginalLanguage = articleLists.ElementAt(i).OriginalLanguage,
                    UserLastModifiedDate = articleLists.ElementAt(i).UserLastModifiedDate,
                    LastUpdatedByUserName = articleLists.ElementAt(i).LastUpdatedByUserName,
                    Notes = articleLists.ElementAt(i).Notes,
                    ArticleBody = articleLists.ElementAt(i).ArticleBody,
                    IsRead = articleLists.ElementAt(i).IsRead,
                    DiseaseObject = "",
                    SelectedPublishedEventIds = new List<int>()
                };
                articleList.Add(m_item);
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var jsonArticle = jsonSerialiser.Serialize(articleList);
            eventmodel.associatedArticles = jsonArticle;
            eventmodel.LastUpdatedByUserName = User.Identity.Name;

            return eventmodel;
        }

        public EventUpdateModel GetMongodbModelForSuggestedEvent(Event realEvent, SuggestedEventModel suggestedEventItemInfo)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            //...create the eventModel to update Zebra
            EventUpdateModel eventmodel = new EventUpdateModel();
            eventmodel.eventID = realEvent.EventId;
            eventmodel.eventTitle = realEvent.EventTitle;
            eventmodel.startDate = (realEvent.StartDate != null) ? realEvent.StartDate.Value.ToString("yyyy-MM-dd") : null;
            eventmodel.endDate = (realEvent.EndDate != null) ? realEvent.EndDate.Value.ToString("yyyy-MM-dd") : null;
            eventmodel.diseaseID = realEvent.DiseaseId.ToString();
            eventmodel.reasonIDs = suggestedEventItemInfo.reasonIDs;
            eventmodel.alertRadius = realEvent.IsLocalOnly.ToString();
            eventmodel.priorityID = realEvent.PriorityId.ToString();
            eventmodel.isPublished = realEvent.IsPublished.ToString();
            eventmodel.summary = realEvent.Summary;
            eventmodel.notes = realEvent.Notes;
            eventmodel.locationObject = suggestedEventItemInfo.locationObject;

            //...Get Associated Article Info if there is any
            List<ArticleUpdateForZebra> articleList = new List<ArticleUpdateForZebra>();

            var articleLists = db.Events.Where(e => e.EventId == realEvent.EventId).Select(s => s.ProcessedArticles).SingleOrDefault();
            for (var i = 0; i < articleLists.Count; i++)
            {
                var m_item = new ArticleUpdateForZebra
                {
                    ArticleId = articleLists.ElementAt(i).ArticleId,
                    ArticleTitle = articleLists.ElementAt(i).ArticleTitle,
                    SystemLastModifiedDate = articleLists.ElementAt(i).SystemLastModifiedDate,
                    CertaintyScore = articleLists.ElementAt(i).CertaintyScore,
                    ArticleFeedId = articleLists.ElementAt(i).ArticleFeedId,
                    FeedURL = articleLists.ElementAt(i).FeedURL,
                    FeedSourceId = articleLists.ElementAt(i).FeedSourceId,
                    FeedPublishedDate = articleLists.ElementAt(i).FeedPublishedDate,
                    HamTypeId = articleLists.ElementAt(i).HamTypeId,
                    OriginalSourceURL = articleLists.ElementAt(i).OriginalSourceURL,
                    IsCompleted = articleLists.ElementAt(i).IsCompleted,
                    SimilarClusterId = articleLists.ElementAt(i).SimilarClusterId,
                    OriginalLanguage = articleLists.ElementAt(i).OriginalLanguage,
                    UserLastModifiedDate = articleLists.ElementAt(i).UserLastModifiedDate,
                    LastUpdatedByUserName = articleLists.ElementAt(i).LastUpdatedByUserName,
                    Notes = articleLists.ElementAt(i).Notes,
                    ArticleBody = articleLists.ElementAt(i).ArticleBody,
                    IsRead = articleLists.ElementAt(i).IsRead,
                    DiseaseObject = "",
                    SelectedPublishedEventIds = new List<int>()
                };
                articleList.Add(m_item);
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var jsonArticle = jsonSerialiser.Serialize(articleList);
            eventmodel.associatedArticles = jsonArticle;
            eventmodel.LastUpdatedByUserName = User.Identity.Name;

            return eventmodel;

        }

        [HttpPost]
        public string DeleteSuggestedEvent(string ID)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            //.....Delete Event from db.Events
            var eventItem = (from e in db.SuggestedEvents
                             where e.SuggestedEventId == ID
                             select e).SingleOrDefault();

            db.SuggestedEvents.Remove(eventItem);
            db.SaveChanges();


            //...User Action Update in the API
            SuggestedEventUserAction userAction = new SuggestedEventUserAction();
            userAction.userAction = (int)EnumUserAction.UserActions.Deleted;
            //var arr = Array.Empty<string>();
            List<string> arrRejectionReasons = new List<string>();
            userAction.reasonsForRejection = arrRejectionReasons;
            userAction.eventId = null;
            Task<HttpResponseMessage> response = UpdateUserActiononSuggestedEvent(ID, userAction);

            return "Deleted";
        }

        public async Task<HttpResponseMessage> UpdateUserActiononSuggestedEvent(string suggestedEventId, SuggestedEventUserAction userAction)
        {
            HttpResponseMessage responseResult = new HttpResponseMessage();
            var response = "";
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
                    responseResult = await client.PutAsync(requestUrl, new StringContent(JsonConvert.SerializeObject(userAction), Encoding.UTF8, "application/json"));

                    if (responseResult.IsSuccessStatusCode)
                    {
                        response = await responseResult.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                response = "Error: " + ex.Message;
            }

            return responseResult;
        }
        #endregion
    }
}