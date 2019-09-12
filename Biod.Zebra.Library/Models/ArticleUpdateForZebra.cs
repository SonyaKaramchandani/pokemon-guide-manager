using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Zebra.Library.Models
{
    public class ArticleUpdateForZebra
    {
        public string ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public DateTime SystemLastModifiedDate { get; set; }
        public Decimal? CertaintyScore { get; set; }
        public int? ArticleFeedId { get; set; }
        public string FeedURL { get; set; }
        public string FeedSourceId { get; set; }
        public DateTime FeedPublishedDate { get; set; }
        public int? HamTypeId { get; set; }
        public string OriginalSourceURL { get; set; }
        public bool? IsCompleted { get; set; }
        public Decimal? SimilarClusterId { get; set; }
        public string OriginalLanguage { get; set; }
        public DateTime? UserLastModifiedDate { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public string Notes { get; set; }
        public string ArticleBody { get; set; }
        public bool? IsRead { get; set; }
        public string DiseaseObject { get; set; }
        public List<int> SelectedPublishedEventIds { get; set; }

    }
}