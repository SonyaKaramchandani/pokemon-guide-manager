using System;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class XtblEventLocationJoinResult
    {
        public int EventId { get; set; }
        
        public int GeonameId { get; set; }
        
        public DateTime EventDate { get; set; }
        
        public int SuspCases { get; set; }
        
        public int ConfCases { get; set; }
        
        public int RepCases { get; set; }
        
        public int Deaths { get; set; }
        
        public string GeonameDisplayName { get; set; }
        
        public int? Admin1GeonameId { get; set; }
        
        public string Admin1Name { get; set; }
        
        public int CountryGeonameId { get; set; }

        public string CountryName { get; set; }
        
        public int? LocationType { get; set; }
    }
}