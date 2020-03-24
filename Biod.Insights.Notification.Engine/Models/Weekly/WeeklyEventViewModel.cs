using Biod.Insights.Service.Models;

namespace Biod.Insights.Notification.Engine.Models.Weekly
{
    public class WeeklyEventViewModel
    {
        public int EventId { get; set; }
        
        public string EventTitle { get; set; }
        
        public int DiseaseId { get; set; }
        
        public bool IsLocal { get; set; }
        
        public int CaseCountChange { get; set; }
        
        public RiskModel ImportationRisk { get; set; }
    }
}