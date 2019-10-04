using Biod.Surveillance.Models.Surveillance;
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
        public ArticleGridWithSimilarCluster() { }

        public ArticleGridWithSimilarCluster(ProcessedArticle article)
        {
            ArticleId = article.ArticleId;
            ArticleTitle = article.ArticleTitle;
            SimilarClusterId = article.SimilarClusterId;
            IsCompleted = article.IsCompleted;
            IsRead = article.IsRead;
            FeedPublishedDate = article.FeedPublishedDate;
            FeedPublishedDateToString = article.FeedPublishedDate.ToString("yyyy-MM-dd"); //feed published dates are converted to String for easy displaying on the front-end through accessing the Model
            ArticleFeedId = article.ArticleFeedId;
            ArticleFeedName = "";
        }

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

        public override bool Equals(object obj)
        {
            return obj is ArticleGridWithSimilarCluster cluster &&
                   EqualityComparer<decimal?>.Default.Equals(SimilarClusterId, cluster.SimilarClusterId) &&
                   EqualityComparer<bool?>.Default.Equals(IsCompleted, cluster.IsCompleted) &&
                   EqualityComparer<bool?>.Default.Equals(IsRead, cluster.IsRead) &&
                   FeedPublishedDate == cluster.FeedPublishedDate &&
                   FeedPublishedDateToString == cluster.FeedPublishedDateToString &&
                   ArticleId == cluster.ArticleId &&
                   ArticleTitle == cluster.ArticleTitle &&
                   EqualityComparer<int?>.Default.Equals(ArticleFeedId, cluster.ArticleFeedId) &&
                   ArticleFeedName == cluster.ArticleFeedName &&
                   HasChildArticle == cluster.HasChildArticle;
        }

        public override int GetHashCode()
        {
            var hashCode = -1429681320;
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal?>.Default.GetHashCode(SimilarClusterId);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(IsCompleted);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(IsRead);
            hashCode = hashCode * -1521134295 + FeedPublishedDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FeedPublishedDateToString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArticleId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArticleTitle);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(ArticleFeedId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArticleFeedName);
            hashCode = hashCode * -1521134295 + HasChildArticle.GetHashCode();
            return hashCode;
        }
    }
}