using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class ArticleWithCaseCounts
    {
        public Decimal? SimilarClusterId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsImportant { get; set; }
        public bool? IsRead { get; set; }
        public string FeedPublishedDate { get; set; }
        public string ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleFeedName { get; set; }
        //public List<Location> LocationCaseCount { get; set; }
        public Location LocationCaseCount { get; set; }
    }


    public class EventArticlesCategorizedByLocations
    {
        public int GeoLocationID { get; set; }
        public string GeoLocationName { get; set; }
        public List<ArticleWithCaseCounts> Articles { get; set; }
    }


}