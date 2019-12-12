using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Api.Models
{
    public class PostUserLocationModel
    {
        [Required]
        public int? GeonameId { get; set; }
    }
}