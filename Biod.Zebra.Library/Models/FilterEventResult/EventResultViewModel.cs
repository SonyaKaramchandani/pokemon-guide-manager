using System.Collections.Generic;

namespace Biod.Zebra.Library.Models.FilterEventResult
{
    /// <summary>
    /// Minimal fields required to show a single item in the Filter Events result list. 
    /// </summary>
    public class EventResultViewModel
    {
        public int EventId { get; set; }
        
        public string EventTitle { get; set; }
        
        public string EventSummary { get; set; }
        
        public string EventStartDate { get; set; }
        
        public string EventEndDate { get; set; }
        
        public List<string> ArticleSourceNames { get; set; }

        public bool HasOutlookReport { get; set; }
        
        public bool IsLocalSpread { get; set; }

        public int ImportationRiskLevel { get; set; }
        
        public int ImportationRiskText { get; set; }
        
        public int ExportationRiskLevel { get; set; }
        
        public int ExportationRiskText { get; set; }
    }
}