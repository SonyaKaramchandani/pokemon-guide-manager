using System.Collections.Generic;

namespace Biod.Zebra.Library.Models.FilterEventResult
{
    public class FilterEventResultViewModel
    {
        public FilterParamsModel FilterParams { get; set; }
        
        public List<DiseaseGroupResultViewModel> DiseaseGroups { get; set; }
    }
}