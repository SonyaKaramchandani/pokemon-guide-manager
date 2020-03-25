using Newtonsoft.Json;

namespace Biod.Insights.Service.Models
{
    public class CaseCountModel
    {
        public int ConfirmedCases { get; set; }
        
        public int ReportedCases { get; set; }
        
        public int SuspectedCases { get; set; }
        
        public int Deaths { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasConfirmedCasesNesting { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasReportedCasesNesting { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasSuspectedCasesNesting { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasDeathsNesting { get; set; }
    }
}