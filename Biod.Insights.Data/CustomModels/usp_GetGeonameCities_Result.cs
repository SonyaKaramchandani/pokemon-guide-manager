using System.ComponentModel.DataAnnotations.Schema;

namespace Biod.Insights.Data.CustomModels
{
    public class usp_GetGeonameCities_Result
    {
        public int GeonameId { get; set; }
        
        public string DisplayName { get; set; }
        
        [Column(TypeName = "decimal(10,5)")]
        public decimal Longitude { get; set; }
        
        [Column(TypeName = "decimal(10,5)")]
        public decimal Latitude { get; set; }

        public string ShapeAsText { get; set; }
    }
}