using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.User
{
    public class UserPersonalDetailsModel
    {
        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }

        [Required]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? UserTypeId { get; set; }

        
        [MaxLength(400)]
        public string Organization { get; set; }

        [Required]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int LocationGeonameId { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MaxLength(256)]
        public string Email { get; set; }

        // ref: https://stackoverflow.com/a/55866098
        // Must match same validation on front-end. See: https://bitbucket.org/bluedottechnologyteam/biodsolution/pull-requests/782/bugfix-pt-1427-phone-validator/diff#Lbiod.insights.app/src/utils/validationPatterns.tsT2
        [RegularExpression(@"^(\+?\d{0,4})?\s?-?\s?(\(?\d{3}\)?)\s?-?\s?(\(?\d{3}\)?)\s?-?\s?(\(?\d{4}\)?)?$", ErrorMessage = "Invalid Phone Number format")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}