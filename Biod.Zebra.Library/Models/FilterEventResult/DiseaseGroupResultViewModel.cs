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
    }
}