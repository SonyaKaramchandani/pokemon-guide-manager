using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblArticleEvent
    {
        public string ArticleId { get; set; }
        public int EventId { get; set; }

        public virtual ProcessedArticle Article { get; set; }
        public virtual Event Event { get; set; }
    }
}
