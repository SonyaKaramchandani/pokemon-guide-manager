using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Notification.Engine.Models
{
    public class InternalEmailViewModel
    {
        [Required]
        public string Subject { get; set; }
        
        [Required]
        public string Body { get; set; }
    }
}