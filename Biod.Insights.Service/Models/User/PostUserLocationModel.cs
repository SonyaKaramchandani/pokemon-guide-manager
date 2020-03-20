using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Service.Models.User
{
    public class PostUserLocationModel
    {
        [Required]
        public int? GeonameId { get; set; }
    }
}