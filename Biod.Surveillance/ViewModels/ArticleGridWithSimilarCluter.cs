using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{

    public class ArticleBase
    {
        public Decimal? SimilarClusterId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsRead { get; set; }
        public int HamTypeID { get; set; }
        public string FeedPublishedDateToString { get; set; }
        public string ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public int? ArticleFeedId { get; set; }
        public string ArticleFeedName { get; set; }
    }

    public class ArticleGridWithSimilarCluster
    {
        //public ArticleBase ParentArticle { get; set; }

        public Decimal? SimilarClusterId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsRead { get; set; }
        public DateTime FeedPublishedDate { get; set; }
        public string FeedPublishedDateToString { get; set; }
        public string ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public int? ArticleFeedId { get; set; }
        public string ArticleFeedName { get; set; }
        //public List<ArticleBase> ChildArticles { get; set; }
        public bool HasChildArticle { get; set; }
    }
}