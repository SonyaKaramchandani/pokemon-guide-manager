using System;

namespace Biod.Insights.Api.Models.Event
{
    public class EventInformationModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public DateTime LastUpdatedDate { get; set; }
        
        public string Summary { get; set; }
        
        public int DiseaseId { get; set; }
    }
}