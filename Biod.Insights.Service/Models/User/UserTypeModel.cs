using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.User
{
    public class UserTypeModel
    {
        [Required] public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string NotificationDescription { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DiseaseRelevanceSettingsModel RelevanceSettings { get; set; }
    }
}