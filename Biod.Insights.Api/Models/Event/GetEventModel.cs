namespace Biod.Insights.Api.Models.Event
{
    public class GetEventModel
    {
        public EventInformationModel EventInformation { get; set; }
        
        public RiskModel ImportationRisk { get; set; }
        
        public RiskModel ExportationRisk { get; set; }
        
        public bool IsLocal { get; set; }
    }
}