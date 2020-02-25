namespace Biod.Insights.Data.CustomModels
{
    public class usp_ZebraEventGetArticlesByEventId_Result
    {
        public string ArticleTitle { get; set; }
        public string FeedURL { get; set; }
        public System.DateTime FeedPublishedDate { get; set; }
        public string ArticleFeedName { get; set; }
        public string OriginalSourceURL { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public int? SeqId { get; set; }
        public string OriginalLanguage { get; set; }
    }
}