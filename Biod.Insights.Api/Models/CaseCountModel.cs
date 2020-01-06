namespace Biod.Insights.Api.Models
{
    public class CaseCountModel
    {
        public int ConfirmedCases { get; set; }
        
        public int SuspectedCases { get; set; }
        
        public int ReportedCases { get; set; }
        
        public int Deaths { get; set; }
    }
}