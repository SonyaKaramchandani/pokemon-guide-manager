namespace Biod.Insights.Data.CustomModels
{
    public class ufn_GetProximalEventLocations_Result
    {
        public int EventId { get; set; }
        
        public int GeonameId { get; set; }
        
        public int RepCases { get; set; }
        
        public int ConfCases { get; set; }
        
        public int SuspCases { get; set; }
        
        public int Deaths { get; set; }
        
        public int LocationType { get; set; }
        
        public string DisplayName { get; set; }
        
        public int? Admin1GeonameId { get; set; }
        
        public int CountryGeonameId { get; set; }
        
        public bool IsWithinLocation { get; set; }
        
    }
}