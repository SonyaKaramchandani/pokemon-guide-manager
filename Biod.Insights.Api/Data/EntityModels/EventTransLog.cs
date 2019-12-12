using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class EventTransLog
    {
        public int LogId { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string Action { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? PriorityId { get; set; }
        public bool? IsPublished { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public int? DiseaseId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EventMongoId { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public bool? IsLocalOnly { get; set; }
        public int? SpeciesId { get; set; }
    }
}
