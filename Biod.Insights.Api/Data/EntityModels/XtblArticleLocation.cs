using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblArticleLocation
    {
        public string ArticleId { get; set; }
        public int LocationGeoNameId { get; set; }

        public virtual ProcessedArticle Article { get; set; }
        public virtual ActiveGeonames LocationGeoName { get; set; }
    }
}
