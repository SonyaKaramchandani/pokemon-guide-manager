using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class XtblArticleEventTransLog
    {
        public int LogId { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string Action { get; set; }
        public string ArticleId { get; set; }
        public int EventId { get; set; }
    }
}
