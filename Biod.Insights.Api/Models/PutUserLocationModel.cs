using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Api.Models
{
    public class PutUserLocationModel
    {
        [Required]
        public int? GeonameId { get; set; }
    }
}