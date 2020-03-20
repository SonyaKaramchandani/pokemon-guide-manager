using System.ComponentModel.DataAnnotations.Schema;

namespace Biod.Insights.Data.CustomModels
{
    public class usp_SearchGeonames_Result
    {
        public int GeonameId { get; set; }
        
        public string DisplayName { get; set; }
        
        public string LocationType { get; set; }
        
        [Column(TypeName = "decimal(10,5)")]
        public decimal Longitude { get; set; }
        
        [Column(TypeName = "decimal(10,5)")]
        public decimal Latitude { get; set; }
    }
}