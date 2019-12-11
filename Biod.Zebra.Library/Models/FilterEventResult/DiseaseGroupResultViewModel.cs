using System.Collections.Generic;

namespace Biod.Zebra.Library.Models.FilterEventResult
{
    public class DiseaseGroupResultViewModel
    {
        public int DiseaseId { get; set; }
        
        public string DiseaseName { get; set; }
        
        public int TotalCases { get; set; }
        
        public string TotalCasesText { get; set; }
        
        public decimal MinTravellers { get; set; }
        
        public decimal MaxTravellers { get; set; }
        
        public string TravellersText { get; set; }
        
        public List<EventResultViewModel> ShownEvents { get; set; }
        
        public List<EventResultViewModel> HiddenEvents { get; set; }
        
        /// <summary>
        /// Whether all the events in this list set to be shown. This will be set to true when:
        /// - the disease is set to Relevance Type = 1 (always)
        /// - the "Only Risk to my location" toggle is set to true from the filter panel
        /// </summary>
        public bool IsAllShown { get; set; }
        
        /// <summary>
        /// Whether this disease grouping is visible. This will be set to true when:
        /// - there are at least 1 case
        /// - the max probability risk is greater than or equal to a threshold
        /// </summary>
        public bool IsVisible { get; set; }
    }
}