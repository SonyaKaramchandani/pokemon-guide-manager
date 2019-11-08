using System;
using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.Infrastructures;

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
        
        public bool IsLocalSpread { get; set; }

        public int ImportationRiskLevel { get; set; }
        
        public string ImportationRiskText { get; set; }
        
        public int ExportationRiskLevel { get; set; }
        
        public string ExportationRiskText { get; set; }

        public static EventResultViewModel FromEventsInfoModel(EventsInfoModel eventsInfoModel)
        {
            return new EventResultViewModel
            {
                EventId = eventsInfoModel.EventId,
                EventSummary = eventsInfoModel.Summary,
                EventTitle = eventsInfoModel.EventTitle,
                ArticleSourceNames = eventsInfoModel.SourceNameList.Select(s => s.DisplayName).ToList(),
                EventEndDate = eventsInfoModel.EndDate,
                EventStartDate = eventsInfoModel.StartDate,
                IsLocalSpread = eventsInfoModel.LocalSpread,
                ImportationRiskLevel = RiskProbabilityHelper.GetRiskLevel(eventsInfoModel.ImportationProbabilityMax),
                ImportationRiskText = StringFormattingHelper.GetInterval(eventsInfoModel.ImportationProbabilityMin, eventsInfoModel.ImportationProbabilityMax, "%"),
                ExportationRiskLevel = RiskProbabilityHelper.GetRiskLevel(eventsInfoModel.ExportationProbabilityMax),
                ExportationRiskText = StringFormattingHelper.GetInterval(eventsInfoModel.ExportationProbabilityMin, eventsInfoModel.ExportationProbabilityMax, "%")
            };
        }
    }
}