using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblRelatedArticles
    {
        public string MainArticleId { get; set; }
        public string RelatedArticleId { get; set; }

        public virtual ProcessedArticle MainArticle { get; set; }
    }
}
