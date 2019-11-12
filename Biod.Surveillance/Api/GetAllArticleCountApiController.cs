using Biod.Surveillance.ViewModels;
using Biod.Zebra.Library.EntityModels.Surveillance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Biod.Surveillance.Api
{
    public class GetAllArticleCountApiController : ApiController
    {

        public ArticleCount Get()
        {
            ArticleCount count = new ArticleCount();

            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

            var ago = DateTime.Now.AddMonths(-12); // 1 year old

            // All Articles
            var allArticles = from r in db.SurveillanceProcessedArticles
                              where r.HamTypeId != 1 &&
                              (r.FeedPublishedDate >= ago)
                              select r;

            count.totalArticles = allArticles.Count();

            //Unprocessed Article
            var allUnprocessedArticles = from r in db.SurveillanceProcessedArticles
                                         where r.HamTypeId != 1 &&
                                         (r.IsCompleted == null || r.IsCompleted == false) &&
                                         (r.FeedPublishedDate >= ago)
                                         select r;

            count.totalUnprocessedArticle = allUnprocessedArticles.Count();


            //........Spam
            var spamArticle = SpamArticleList();

            count.totalSpamArticles = spamArticle.Count();

            return count;
        }


        public static IEnumerable<SurveillanceProcessedArticle> SpamArticleList()
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();


            //........Spam
            DateTime? maxUserLastModified = (from s in db.SurveillanceProcessedArticles
                                             where s.HamTypeId == 1
                                             select s.UserLastModifiedDate).Max();

            DateTime? maxSystemLastModified = (from s in db.SurveillanceProcessedArticles
                                               where s.HamTypeId == 1 && s.UserLastModifiedDate == null
                                               select s.SystemLastModifiedDate).Max();


            var prev = Convert.ToDateTime(maxSystemLastModified).AddMonths(-1); //default date initialized to maxSystemLastModified

            if (maxUserLastModified != null)
            {
                prev = (maxUserLastModified > maxSystemLastModified) ? Convert.ToDateTime(maxUserLastModified).AddMonths(-1) : Convert.ToDateTime(maxSystemLastModified).AddMonths(-1);
            }


            //considers article's UserLastModifiedDate when UserLastModifiedDate is NOT Null
            var allSpamArt_WithUserModifiedDate = (from r in db.SurveillanceProcessedArticles
                                                   where r.HamTypeId == 1 && r.UserLastModifiedDate >= prev
                                                   select r).ToList();

            //considers article's SystemLastModifiedDate when UserLastModifiedDate is Null
            var allSpamArt_WithoutUserModifiedDate = (from r in db.SurveillanceProcessedArticles
                                                      where r.HamTypeId == 1 &&
                                                      (r.SystemLastModifiedDate >= prev) && r.UserLastModifiedDate == null
                                                      select r).ToList();

            var concatSpam = allSpamArt_WithUserModifiedDate.Concat(allSpamArt_WithoutUserModifiedDate);

            return concatSpam;

        }



    }
}
