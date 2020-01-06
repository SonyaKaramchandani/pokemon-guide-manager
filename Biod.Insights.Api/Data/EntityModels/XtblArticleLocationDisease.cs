using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblArticleLocationDisease
    {
        public string ArticleId { get; set; }
        public int LocationGeoNameId { get; set; }
        public int DiseaseId { get; set; }
        public int? TotalDeathCount { get; set; }
        public int? TotalConfirmedCount { get; set; }
        public int? TotalSuspectedCount { get; set; }
        public int? TotalReportedCount { get; set; }
        public int? NewDeathCount { get; set; }
        public int? NewConfirmedCount { get; set; }
        public int? NewSuspectedCount { get; set; }
        public int? NewReportedCount { get; set; }

        public virtual ProcessedArticle Article { get; set; }
        public virtual Diseases Disease { get; set; }
        public virtual ActiveGeonames LocationGeoName { get; set; }
    }
}
