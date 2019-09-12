using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class XtblArticleEvent
    {
        public string ArticleId { get; set; }
        public int EventId { get; set; }

        public ProcessedArticle Article { get; set; }
        public Event Event { get; set; }
    }
}
