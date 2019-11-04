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
        
        public string Summary { get; set; }
        
        public string ExportationPriorityTitle { get; set; }
        
        public string CountryName { get; set; }
        
        public bool HasOutlookReport { get; set; }

        public static MapPinEventModel FromEventsInfoModel(EventsInfoModel eventsInfoModel)
        {
            return new MapPinEventModel
            {
                EventId = eventsInfoModel.EventId,
                EventTitle = eventsInfoModel.EventTitle,
                Summary = eventsInfoModel.Summary,
                CountryName = eventsInfoModel.EventCountry.CountryName,
                StartDate = eventsInfoModel.StartDate,
                EndDate = eventsInfoModel.EndDate,
                ExportationPriorityTitle = eventsInfoModel.ExportationPriorityTitle,
                HasOutlookReport = eventsInfoModel.HasOutlookReport ?? false
            };
        }
    }
}