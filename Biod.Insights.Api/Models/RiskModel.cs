namespace Biod.Insights.Api.Models
{
    public class RiskModel
    {
        public bool ModelNotRun { get; set; }
        
        public float MinProbability { get; set; }
        
        public float MaxProbability { get; set; }
        
        public float MinMagnitude { get; set; }
        
        public float MaxMagnitude { get; set; }
    }
}