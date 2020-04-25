using System.Collections.Generic;

namespace Biod.Insights.Service.Data.CustomModels
{
    public class SourceAirportJoinResult : AirportJoinResult
    {
        public double MinPrevalence { get; set; }

        public double MaxPrevalence { get; set; }
        
        public int Population { get; set; }
        
        public IEnumerable<GridStationCaseJoinResult> GridStationCases { get; set; }
    }
}