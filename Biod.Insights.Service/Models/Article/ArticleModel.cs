using System;

namespace Biod.Insights.Service.Models.Article
{
    public class ArticleModel
    {
        public string Title { get; set; }
        
        public string Url { get; set; }
        
        public DateTime PublishedDate { get; set; }
        
        public string OriginalLanguage { get; set; }
        
        public string SourceName { get; set; }
    }
}