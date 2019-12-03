using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class XtblArticleLocation
    {
        public string ArticleId { get; set; }
        public int LocationGeoNameId { get; set; }

        public virtual ProcessedArticle Article { get; set; }
    }
}
