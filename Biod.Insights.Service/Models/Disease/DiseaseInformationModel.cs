using System.Collections.Generic;

namespace Biod.Insights.Service.Models.Disease
{
    public class DiseaseInformationModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Agents { get; set; }
        
        public string AgentTypes { get; set; }
        
        public IEnumerable<AcquisitionModeGroupModel> AcquisitionModeGroups { get; set; }
        
        public string TransmissionModes { get; set; }
        
        public string IncubationPeriod { get; set; }
        
        public string PreventionMeasure { get; set; }
        
        public string BiosecurityRisk { get; set; }
    }
}