namespace Biod.Insights.Service.Models.Risk
{
    public class GeorgeRiskClass
    {
        public Location[] locations { get; set; }
        public string cacheTag { get; set; }
    }

    public class Location
    {
        public int locationId { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public Diseaserisk[] diseaseRisks { get; set; }
        public string waterQuality { get; set; }
    }

    public class Diseaserisk
    {
        public int diseaseRiskId { get; set; }
        public int diseaseId { get; set; }
        public string url { get; set; }
        public bool seasonal { get; set; }
        public float mediaBuzz { get; set; }
        public Defaultrisk defaultRisk { get; set; }
        public object[] contextSpecificMessages { get; set; }
    }

    public class Defaultrisk
    {
        public Level level { get; set; }
        public float riskValue { get; set; }
    }

    public class Level
    {
        public string phrase { get; set; }
        public int level { get; set; }
    }
}
