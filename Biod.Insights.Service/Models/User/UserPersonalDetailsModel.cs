using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.User
{
    public class UserPersonalDetailsModel
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? UserTypeId { get; set; }

        public string Organization { get; set; }

        [Required]
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