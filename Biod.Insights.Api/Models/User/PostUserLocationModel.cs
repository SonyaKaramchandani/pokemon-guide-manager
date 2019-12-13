using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Api.Models.User
{
    public class PostUserLocationModel
    {
        [Required]
        public int? GeonameId { get; set; }
    }
}