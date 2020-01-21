using System;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class XtblEventArticleJoinResult
    {
        public int EventId { get; set; }
        
        public string ArticleId { get; set; }
        
        public string ArticleTitle { get; set; }
        
        public int? ArticleFeedId { get; set; }
        
        public string FeedUrl { get; set; }
        
        public DateTime FeedPublishedDate { get; set; }
        
        public string OriginalSourceUrl { get; set; }
        
        public string DisplayName { get; set; }
        
        public int? SeqId { get; set; }
        
        public string OriginalLanguage { get; set; }
    }
}