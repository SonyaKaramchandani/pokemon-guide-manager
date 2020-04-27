using System.Collections.Generic;
using Biod.Products.Common.Constants;

namespace Biod.Insights.Notification.Engine.Models.Proximal
{
    public class ProximalViewModel : EmailViewModel
    {
        public override NotificationType NotificationType => NotificationType.Proximal;

        public string DiseaseName { get; set; }
        
        public int DiseaseId { get; set; }
        
        public string CountryName { get; set; }
        
        public IEnumerable<string> UserLocations { get; set; }

        public string LastUpdatedDate { get; set; }
        
        public IEnumerable<ProximalEventLocationViewModel> EventLocations { get; set; }
    }
}