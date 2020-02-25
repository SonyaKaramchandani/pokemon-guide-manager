using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class ArticleFeed
    {
        public ArticleFeed()
        {
            ProcessedArticle = new HashSet<ProcessedArticle>();
        }

        public int ArticleFeedId { get; set; }
        public string ArticleFeedName { get; set; }
        public string DisplayName { get; set; }
        public int? SeqId { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<ProcessedArticle> ProcessedArticle { get; set; }
    }
}
