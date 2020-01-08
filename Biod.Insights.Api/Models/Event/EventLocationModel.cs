namespace Biod.Insights.Api.Models.Event
{
    public class EventLocationModel
    {
        public int GeonameId { get; set; }
        
        public string LocationName { get; set; }
        
        public int LocationType { get; set; }
        
        public CaseCountModel CaseCounts { get; set; } 
    }
}