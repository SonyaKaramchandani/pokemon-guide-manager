using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class ArticleFeed
    {
        public ArticleFeed()
        {
            ProcessedArticle = new HashSet<ProcessedArticle>();
        }

        public int ArticleFeedId { get; set; }
        public string ArticleFeedName { get; set; }

        public ICollection<ProcessedArticle> ProcessedArticle { get; set; }
    }
}
