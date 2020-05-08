using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Service.Models.User
{
    public class UserCustomSettingsModel
    {
        [Required]
        public IEnumerable<int> GeonameIds { get; set; }
        
        [Required]
        public Guid UserTypeId { get; set; }
        
        [Required]
        public DiseaseRelevanceSettingsModel DiseaseRelevanceSettings { get; set; }
        
        [Required]
        public bool IsPresetSelected { get; set; }
    }
}