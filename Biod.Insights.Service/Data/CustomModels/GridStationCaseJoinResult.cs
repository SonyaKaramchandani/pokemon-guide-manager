namespace Biod.Insights.Service.Data.CustomModels
{
    public class GridStationCaseJoinResult
    {
        public int Cases { get; set; }
        
        public int MinCases { get; set; }
        
        public int MaxCases { get; set; }
        
        public double Probability { get; set; }
    }
}