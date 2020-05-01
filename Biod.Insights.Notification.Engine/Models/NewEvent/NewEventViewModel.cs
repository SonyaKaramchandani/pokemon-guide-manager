using System.Collections.Generic;
using Biod.Products.Common.Constants;
using Biod.Insights.Service.Models.Disease;

namespace Biod.Insights.Notification.Engine.Models.NewEvent
{
    public class NewEventViewModel : EmailViewModel
    {
        public override NotificationType NotificationType => NotificationType.NewEvent;
        
        public int ReportedCases { get; set; }
        
        public string Summary { get; set; }
        
        public IEnumerable<string> ArticleSources { get; set; }
        
        public IEnumerable<NewEventLocationViewModel> UserLocations { get; set; }
        
        public int TotalUserLocations { get; set; }
        
        public DiseaseInformationModel DiseaseInformation { get; set; }
    }
}