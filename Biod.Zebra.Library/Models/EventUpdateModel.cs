using System.ComponentModel.DataAnnotations;

namespace Biod.Zebra.Library.Models
{
    public class EventUpdateModel
    {
        [Required(ErrorMessage="Event ID is required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Event ID must be numeric")]
        public string eventID { get; set; }

        public string eventTitle { get; set; }

        [Required(ErrorMessage = "Start Date is required", AllowEmptyStrings = false)]
        public string startDate { get; set; }

        public string endDate { get; set; }

        public string diseaseID { get; set; }

        public int speciesID { get; set; }

        [Required(ErrorMessage="Reason IDs array is required")]
        public string[] reasonIDs { get; set; }

        [Required(ErrorMessage="Alert Radius is required")]
        [RegularExpression("^([Tt]rue|[Ff]alse)$", ErrorMessage = "Alert Radius must be a boolean value")]
        public string alertRadius { get; set; }

        public string priorityID { get; set; }

        public string isPublished { get; set; }

        public string summary { get; set; }

        public string notes { get; set; }

        public string locationObject { get; set; }

        public string eventMongoId { get; set; }

        public string associatedArticles { get; set; }

        public string LastUpdatedByUserName { get; set; }
    }
}