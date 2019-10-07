using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Biod.Zebra.Library.Models.User
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "The First Name field is required")]
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "The Last Name field is required")]
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "The Role field is required")]
        [JsonProperty(PropertyName = "Role")]
        public string[] RoleNames { get; set; }
        
        [JsonProperty(PropertyName = "Organization")]
        public string Organization { get; set; }
        
        [Phone]
        [JsonProperty(PropertyName = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "The Email field is required")]
        [EmailAddress]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "The Location field is required")]
        [JsonProperty(PropertyName = "Location")]
        [Range(1, int.MaxValue, ErrorMessage = "The Location field: please select a location from drop-down list after key input.")]
        public int LocationGeonameId { get; set; }

        [JsonProperty(PropertyName = "ResetPassword")]
        public bool ResetPasswordRequired { get; set; } = true;
        
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The Password must be at least {2} characters long.", MinimumLength = 6)]
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [JsonProperty(PropertyName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}