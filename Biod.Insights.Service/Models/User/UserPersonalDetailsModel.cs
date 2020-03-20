using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.User
{
    public class UserPersonalDetailsModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [DisplayName("Role")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RoleId { get; set; }
        
        public string Organization { get; set; }
        
        [Required]
        [DisplayName("Location")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int LocationGeonameId { get; set; }
        
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}