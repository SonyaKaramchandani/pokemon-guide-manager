﻿using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class ProcessedArticle
    {
        public ProcessedArticle()
        {
            XtblArticleEvent = new HashSet<XtblArticleEvent>();
            XtblArticleLocation = new HashSet<XtblArticleLocation>();
            XtblArticleLocationDisease = new HashSet<XtblArticleLocationDisease>();
            XtblRelatedArticles = new HashSet<XtblRelatedArticles>();
        }

        public string ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public DateTime SystemLastModifiedDate { get; set; }
        public decimal? CertaintyScore { get; set; }
        public int? ArticleFeedId { get; set; }
        public string FeedUrl { get; set; }
        public string FeedSourceId { get; set; }
        public DateTime FeedPublishedDate { get; set; }
        public int? HamTypeId { get; set; }
        public string OriginalSourceUrl { get; set; }
        public bool? IsCompleted { get; set; }
        public decimal? SimilarClusterId { get; set; }
        public string OriginalLanguage { get; set; }
        public DateTime? UserLastModifiedDate { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public string Notes { get; set; }
        public string ArticleBody { get; set; }
        public bool? IsRead { get; set; }

        public virtual ArticleFeed ArticleFeed { get; set; }
        public virtual HamType HamType { get; set; }
        public virtual ICollection<XtblArticleEvent> XtblArticleEvent { get; set; }
        public virtual ICollection<XtblArticleLocation> XtblArticleLocation { get; set; }
        public virtual ICollection<XtblArticleLocationDisease> XtblArticleLocationDisease { get; set; }
        public virtual ICollection<XtblRelatedArticles> XtblRelatedArticles { get; set; }
    }
}
