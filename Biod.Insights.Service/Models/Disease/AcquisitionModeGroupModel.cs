using System.Collections.Generic;

namespace Biod.Insights.Service.Models.Disease
{
    public class AcquisitionModeGroupModel
    {
        public int RankId { get; set; }
        
        public string RankName { get; set; }
        
        public IEnumerable<AcquisitionModeModel> AcquisitionModes { get; set; }
    }
}