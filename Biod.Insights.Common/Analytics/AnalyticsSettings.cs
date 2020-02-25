namespace Biod.Insights.Common.Analytics
{
    public class AnalyticsSettings
    {
        public bool IsGoogleAnalyticsEnabled { get; set; }
        
        public bool IsLitmusAnalyticsEnabled { get; set; }
        
        public bool IsHeapAnalyticsEnabled { get; set; }
        
        public bool IsHotjarAnalyticsEnabled { get; set; }
        
        public string LitmusTrackingId { get; set; }
        
        public string GaTrackingId { get; set; }
        
        public string HeapTrackingId { get; set; }
        
        public string HotjarTrackingId { get; set; }
    }
}