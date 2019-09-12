using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
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

        public ProcessedArticle Article { get; set; }
        public Diseases Disease { get; set; }
        public Geonames LocationGeoName { get; set; }
    }
}
