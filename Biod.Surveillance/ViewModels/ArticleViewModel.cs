using Biod.Surveillance.Models.Surveillance;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Biod.Surveillance.ViewModels
{


    public class ArticleGrid
    {

        public string ArticleID { get; set; }

        public Decimal? SimilarClusterId { get; set; }
        public bool HasSimilarArticle { get; set; }
        public bool IsChildArticle { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsImportant { get; set; }
        public DateTime FeedPublishedDate { get; set; }
        public string ArticleTitle { get; set; }
        public int ArticleFeedID { get; set; }
        public string ArticleFeedName { get; set; }
        public int ArticleHamTypeId { get; set; }
        public string ArticleOriginalLanguage { get; set; }
        public string ArticleTranslatedFrom { get; set; }
        public string ArticleURL { get; set; }
        public int DiseaseId { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Geoname> Geonames { get; set; }
        public string Notes { get; set; }
        public string ArticleBody { get; set; }
        public string ParentArticleId { get; set; }
        public string ParentArticleTitle { get; set; }
    }

    public class SortedArticle{
        public decimal? Group { get; set; }
        public IOrderedEnumerable<ProcessedArticle> Articles { get; set; }
    }

    public class ArticleByFilterView
    {

        public IEnumerable<HamType> HamTypes { get; set; }

        public IList<ArticleGrid> ArticleList { get; set; }

        public dynamic NewArticleList { get; set; }

        public MultiSelectList LocationList { get; set; }

        public MultiSelectList ArticleFeeds { get; set; }

        public MultiSelectList ArticleDiseases { get; set; }



        /*................... Methods Definations............*/

        public static MultiSelectList GetArticleFeeds(string[] selectedValues)
        {
            try
            {
                BiodSurveillanceDataEntities dbContext = new BiodSurveillanceDataEntities();
                //List<ArticleFeed> feeds = new List<ArticleFeed>()
                //{
                //    new ArticleFeed(){ ID = 1, Name = "ProMED" },
                //    new ArticleFeed(){ ID = 2, Name = "WHO" },
                //    new ArticleFeed(){ ID = 3, Name = "GDELT" }

                //};
                var feeds = (from r in dbContext.ArticleFeeds select r);

                return new MultiSelectList(feeds, "ArticleFeedId", "ArticleFeedName", selectedValues);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static MultiSelectList GetDiseases(string[] selectedValues)
        {
            try
            {
                BiodSurveillanceDataEntities dbContext = new BiodSurveillanceDataEntities();

                var diseases = dbContext.Diseases.OrderBy(s => s.DiseaseName);

                return new MultiSelectList(diseases, "DiseaseId", "DiseaseName", selectedValues);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static IList<HamType> GetHamType(BiodSurveillanceDataEntities dbContext)
        {
            return dbContext.HamTypes.ToList();
        }

        public static MultiSelectList GetLocationLists(string[] selectedValues)
        {

            try
            {
                BiodSurveillanceDataEntities dbContext = new BiodSurveillanceDataEntities();
                //var geonames = (from r in dbContext.Geonames select r);
                return null;
                //return new MultiSelectList(geonames, "GeonameId", "Name", selectedValues);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //...Currently Not using this function
        public static IList<ArticleGrid> GetProcessedArticle([Bind(Include = "ArticleId,ArticleTitle,CertaintyScore,ArticleFeedId,FeedURL,FeedSourceId,FeedPublishedData,HamTypeId,OriginalSourceURL,CompletedByUser,SimilarClusterId,OriginalLanguage,UserLastModifiedDate,SystemLastModifiedDate,ArticleFeed,HamType")] string ID)
        {

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            ArticleGrid articleGrid = new ArticleGrid();

            IList<ArticleGrid> response = null;
            var ago = DateTime.Now.AddMonths(-12); // 1 year old           

            if (ID == "all")
            {
                response = (from r in db.ProcessedArticles
                            where r.HamTypeId != 1 &&
                                     (r.FeedPublishedDate >= ago)

                            select new ArticleGrid
                            {
                                ArticleID = r.ArticleId,
                                SimilarClusterId = r.SimilarClusterId ?? null,
                                IsCompleted = r.IsCompleted,
                                FeedPublishedDate = r.FeedPublishedDate,
                                ArticleTitle = r.ArticleTitle,
                                ArticleFeedID = r.ArticleFeedId ?? -1,
                                ArticleFeedName = (from d in db.ArticleFeeds
                                                   where d.ArticleFeedId == r.ArticleFeedId
                                                   select d.ArticleFeedName).FirstOrDefault()
                            }).ToList();
            }
            else if (ID == "unprocessed")
            {
                response = (from r in db.ProcessedArticles
                            where (r.IsCompleted == null || r.IsCompleted == false) &&
                                         (r.FeedPublishedDate >= ago)

                            select new ArticleGrid
                            {
                                ArticleID = r.ArticleId,
                                SimilarClusterId = r.SimilarClusterId ?? null,
                                IsCompleted = r.IsCompleted,
                                FeedPublishedDate = r.FeedPublishedDate,
                                ArticleTitle = r.ArticleTitle,
                                ArticleFeedID = r.ArticleFeedId ?? -1,
                                ArticleFeedName = (from d in db.ArticleFeeds
                                                   where d.ArticleFeedId == r.ArticleFeedId
                                                   select d.ArticleFeedName).FirstOrDefault()
                            }).ToList();
            }
            else //spam article
            {

                var prev = DateTime.Now.AddMonths(-1); // 1 month old

                response = (from r in db.ProcessedArticles
                            where r.HamTypeId == 1 &&
                                  (r.FeedPublishedDate >= prev)

                            select new ArticleGrid
                            {
                                ArticleID = r.ArticleId,
                                SimilarClusterId = r.SimilarClusterId ?? null,
                                IsCompleted = r.IsCompleted,
                                FeedPublishedDate = r.FeedPublishedDate,
                                ArticleTitle = r.ArticleTitle,
                                ArticleFeedID = r.ArticleFeedId ?? -1,
                                ArticleFeedName = (from d in db.ArticleFeeds
                                                   where d.ArticleFeedId == r.ArticleFeedId
                                                   select d.ArticleFeedName).FirstOrDefault()
                            }).ToList();
            }

            return response;
        }


    }


    public class DiseaseRoot
    {
        public int DiseaseID { get; set; }
        public string DiseaseName { get; set; }
        public List<Location> Locations { get; set; }

    }


    public class ArticleDetailsById
    {

        public IEnumerable<HamType> HamTypes { get; set; }

        //public MultiSelectList diseases { get; set; }
        public List<DiseaseRoot> diseases { get; set; }
        public IList<Disease> DiseaseModel { get; set; }
        public IList<Xtbl_Article_Location_Disease> LocationListModel { get; set; }
        public ArticleGrid ArticleDetails { get; set; }
        public MultiSelectList ArticleFeeds { get; set; }
        public MultiSelectList EventMultiList { get; set; }

        public static ArticleGrid GetArticleById(BiodSurveillanceDataEntities dbContext, string id, bool hasSimilarArticle, bool isChild, string parentId)
        {
            try
            {
                var articleInfo = dbContext.ProcessedArticles
                    .Include(pa => pa.Events)
                    .Include(pa => pa.Geonames)
                    .Include(pa => pa.ArticleFeed)
                    .Single(s => s.ArticleId == id);
                var parentArticleId = "";
                var parentArticleTitle = "";

                if (isChild)
                {
                    var parentArticle = dbContext.ProcessedArticles.Single(s => s.ArticleId == parentId);
                    parentArticleId = parentArticle.ArticleId;
                    parentArticleTitle = parentArticle.ArticleTitle;
                }
                
                var translatedFrom = articleInfo.OriginalLanguage;
                //Get translated language from the lookup table only for foreign articles of Ham type 3 that are published March 28, 2019 onwards.
                if (articleInfo.OriginalLanguage != "en" && articleInfo.HamTypeId == 3 && articleInfo.FeedPublishedDate >= Convert.ToDateTime("03/28/2019"))
                {
                    var lookups = dbContext.ArticleGDELTLanguageLookups.Where(t => t.Iso2 == translatedFrom || t.Iso3 == translatedFrom).ToList();
                    translatedFrom = lookups.FirstOrDefault(l => l.Iso2 != null)?.LangName ?? lookups.FirstOrDefault(l => l.Iso3 != null)?.LangName ?? translatedFrom;
                }

                return new ArticleGrid
                {
                    ArticleID = articleInfo.ArticleId,
                    SimilarClusterId = articleInfo.SimilarClusterId,
                    HasSimilarArticle = hasSimilarArticle,
                    IsChildArticle = isChild,
                    IsCompleted = articleInfo.IsCompleted,
                    IsImportant = articleInfo.IsImportant,
                    FeedPublishedDate = articleInfo.FeedPublishedDate,
                    ArticleTitle = articleInfo.ArticleTitle,
                    ArticleFeedID = articleInfo.ArticleFeedId ?? -1,
                    ArticleFeedName = articleInfo.ArticleFeed?.ArticleFeedName,
                    ArticleHamTypeId = articleInfo.HamTypeId ?? -1,
                    ArticleOriginalLanguage = articleInfo.OriginalLanguage,
                    ArticleTranslatedFrom = translatedFrom,
                    ArticleURL = articleInfo.OriginalSourceURL,
                    Events = articleInfo.Events,
                    Geonames = articleInfo.Geonames,
                    Notes = articleInfo.Notes,
                    ArticleBody = (articleInfo.ArticleBody ?? "").Replace("\\n", Environment.NewLine),
                    ParentArticleId = parentArticleId,
                    ParentArticleTitle = parentArticleTitle
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ArticleGrid GetArticleViewModelById(string articleId)
        {

            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
                string parentArticleId = "", parentArticleTitle = "";
                bool hasSimilarArt = false, isChildArticle = false;

                var articleInfo = db.ProcessedArticles.Where(s => s.ArticleId == articleId).SingleOrDefault();
                var similarArticleList = db.ProcessedArticles.Where(c => c.SimilarClusterId == articleInfo.SimilarClusterId).ToList();

                if (similarArticleList.Count > 1)
                {
                    var LatesPublishedtDate = similarArticleList.Max(x => x.FeedPublishedDate);
                    var latestArticle = similarArticleList.First(x => x.FeedPublishedDate == LatesPublishedtDate);
                    parentArticleId = latestArticle.ArticleId;
                    parentArticleTitle = latestArticle.ArticleTitle;
                    isChildArticle = (articleInfo.ArticleId != latestArticle.ArticleId) ? true : false;                    
                    hasSimilarArt = true;
                }
                else
                {
                    parentArticleId = articleInfo.ArticleId;
                    parentArticleTitle = articleInfo.ArticleTitle;
                    hasSimilarArt = false;
                    isChildArticle = false;
                }


                var response = (from r in db.ProcessedArticles
                                where r.ArticleId == articleId 
                                select new ArticleGrid
                                {
                                    ArticleID = r.ArticleId,
                                    SimilarClusterId = r.SimilarClusterId ?? null,
                                    HasSimilarArticle = hasSimilarArt,
                                    IsChildArticle = isChildArticle,
                                    IsCompleted = r.IsCompleted,
                                    FeedPublishedDate = r.FeedPublishedDate,
                                    ArticleTitle = r.ArticleTitle,
                                    ArticleFeedID = r.ArticleFeedId ?? -1,
                                    ArticleFeedName = (from d in db.ArticleFeeds
                                                       where d.ArticleFeedId == r.ArticleFeedId
                                                       select d.ArticleFeedName).FirstOrDefault(),
                                    ArticleHamTypeId = r.HamTypeId ?? -1,
                                    ArticleOriginalLanguage = r.OriginalLanguage,                                   
                                    ArticleURL = r.OriginalSourceURL,
                                    Events = r.Events,
                                    Geonames = r.Geonames,
                                    Notes = r.Notes,
                                    ArticleBody = r.ArticleBody.Replace("\\n", Environment.NewLine),
                                    ParentArticleId = parentArticleId,
                                    ParentArticleTitle = parentArticleTitle

                                }).ToList().FirstOrDefault();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static IList<Xtbl_Article_Location_Disease> GetLocationsById(string ArticleID)
        {

            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
                var response = db.ProcessedArticles.Find(ArticleID).Xtbl_Article_Location_Disease.ToList();


                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DiseaseRoot> GetArticleDiseaseById(BiodSurveillanceDataEntities dbContext, string articleId)
        {

            try
            {
                var article = dbContext.ProcessedArticles
                    .Include(pa => pa.Xtbl_Article_Location_Disease.Select(d => d.Disease))
                    .Include(pa => pa.Xtbl_Article_Location_Disease.Select(d => d.Geoname))
                    .Single(pa => pa.ArticleId == articleId);

                return article.Xtbl_Article_Location_Disease
                    .GroupBy(d => new { id = d.DiseaseId, name = d.Disease?.DiseaseName })
                    .Select(g => new DiseaseRoot
                    {
                        DiseaseID = g.Key.id,
                        DiseaseName = g.Key.name,
                        Locations = g.Select(l => new Location
                        {
                            GeoID = l.LocationGeoNameId,
                            GeoName = l.Geoname?.DisplayName,
                            NewSuspectedCount = l.NewSuspectedCount ?? 0,
                            NewConfirmedCount = l.NewConfirmedCount ?? 0,
                            NewReportedCount = l.NewReportedCount ?? 0,
                            NewDeathCount = l.NewDeathCount ?? 0,
                            TotalSuspectedCount = l.TotalSuspectedCount ?? 0,
                            TotalConfirmedCount = l.TotalConfirmedCount ?? 0,
                            TotalReportedCount = l.TotalReportedCount ?? 0,
                            TotalDeathCount = l.TotalDeathCount ?? 0
                            
                        }).ToList()
                    })
                    .ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }


    public class ArticleCount
    {
        public int? totalArticles { get; set; }

        public int? totalUnprocessedArticle { get; set; }

        public int? totalSpamArticles { get; set; }

        public static ArticleCount GetAllArticleCount()
        {           

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            ArticleCount count = new ArticleCount();

            var getCounts = db.usp_GetArticleCounts().FirstOrDefault();
            count.totalArticles = getCounts.AllCount;
            count.totalUnprocessedArticle = getCounts.UnprocessedCount;
            count.totalSpamArticles = getCounts.SpamCount;

            //var ago = DateTime.Now.AddMonths(-12); // 1 year old

            //// All Articles
            //var allArticles = from r in db.ProcessedArticles
            //                  where r.HamTypeId != 1 &&
            //                  (r.FeedPublishedDate >= ago)
            //                  select r;

            //count.totalArticles = allArticles.Count();

            ////Unprocessed Article
            //var allUnprocessedArticles = from r in db.ProcessedArticles
            //                             where r.HamTypeId != 1 &&
            //                             (r.IsCompleted == null || r.IsCompleted == false) &&
            //                             (r.FeedPublishedDate >= ago)
            //                             select r;

            //count.totalUnprocessedArticle = allUnprocessedArticles.Count();


            ////........Spam
            //var spamArticle = SpamArticleList();

            //count.totalSpamArticles = spamArticle.Count();

            return count;
        }

        public static IEnumerable<ProcessedArticle> SpamArticleList()
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();


            //........Spam
            DateTime? maxUserLastModified = (from s in db.ProcessedArticles
                                             where s.HamTypeId == 1
                                             select s.UserLastModifiedDate).Max(); //latest UserLastModified date

            DateTime? maxSystemLastModified = (from s in db.ProcessedArticles
                                               where s.HamTypeId == 1 && s.UserLastModifiedDate == null
                                               select s.SystemLastModifiedDate).Max();  //latest SystemLastModified date


            
            var prev = Convert.ToDateTime(maxSystemLastModified).AddMonths(-1); //default date initialized to (maxSystemLastModified - 1 month)

            // Prev = Latest date between UserLastModified and SystemLastModified - 1 month
            if (maxUserLastModified != null)
            {
                prev = (maxUserLastModified > maxSystemLastModified) ? Convert.ToDateTime(maxUserLastModified).AddMonths(-1) : Convert.ToDateTime(maxSystemLastModified).AddMonths(-1);
            }


            //considers article's UserLastModifiedDate when UserLastModifiedDate is NOT Null
            var allSpamArt_WithUserModifiedDate = (from r in db.ProcessedArticles
                                                   where r.HamTypeId == 1 && r.UserLastModifiedDate >= prev
                                                   select r).ToList();

            //considers article's SystemLastModifiedDate when UserLastModifiedDate is Null
            var allSpamArt_WithoutUserModifiedDate = (from r in db.ProcessedArticles
                                                      where r.HamTypeId == 1 &&
                                                      (r.SystemLastModifiedDate >= prev) && r.UserLastModifiedDate == null
                                                      select r).ToList();

            var concatSpam = allSpamArt_WithUserModifiedDate.Concat(allSpamArt_WithoutUserModifiedDate);

            return concatSpam;

        }
    }
}