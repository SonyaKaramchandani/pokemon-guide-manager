using Biod.Zebra.Library.Infrastructures;
using System;

namespace Biod.Zebra.Library.Models.Map
{
    /// <summary>
    /// Model containing all the necessary properties of an event in order for a pin to display properly on the map
    /// </summary>
    public class MapPinEventModel
    {
        public int EventId { get; set; }
        
        public string EventTitle { get; set; }
        
        public string StartDate { get; set; }
        
        public string EndDate { get; set; }
        
        [Obsolete]
        public string Summary { get; set; }

        [Obsolete]
        public string ExportationPriorityTitle { get; set; }
        
        public string CountryName { get; set; }

        public int RepCases { get; set; }

        public int Deaths { get; set; }

        public int ImportationRiskLevel { get; set; }

        public string ImportationProbabilityString { get; set; }

        public int ExportationRiskLevel { get; set; }

        public string ExportationProbabilityString { get; set; }

        public bool LocalSpread { get; set; }

        public bool GlobalView { get; set; }

        public static MapPinEventModel FromEventsInfoModel(EventsInfoModel eventsInfoModel, bool isGlobalView)
        {
            return new MapPinEventModel
            {
                EventId = eventsInfoModel.EventId,
                EventTitle = eventsInfoModel.EventTitle,
                CountryName = eventsInfoModel.EventCountry.CountryName,
                StartDate = eventsInfoModel.StartDate,
                EndDate = eventsInfoModel.EndDate,
                RepCases = eventsInfoModel.RepCases,
                Deaths = eventsInfoModel.Deaths,
                LocalSpread = eventsInfoModel.LocalSpread,
                GlobalView = isGlobalView,
                ImportationRiskLevel = RiskProbabilityHelper.GetRiskLevel(eventsInfoModel.ImportationProbabilityMax),
                ImportationProbabilityString = eventsInfoModel.LocalSpread ? "In or proximal to your area(s) of interest" : StringFormattingHelper.GetInterval(eventsInfoModel.ImportationProbabilityMin, eventsInfoModel.ImportationProbabilityMax, "%"),
                ExportationRiskLevel = RiskProbabilityHelper.GetRiskLevel(eventsInfoModel.ExportationProbabilityMax),
                ExportationProbabilityString = StringFormattingHelper.GetInterval(eventsInfoModel.ExportationProbabilityMin, eventsInfoModel.ExportationProbabilityMax, "%")
            };
        }
    }
}