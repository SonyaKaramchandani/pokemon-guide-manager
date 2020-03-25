using System.Collections.Generic;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Models.Disease;

namespace Biod.Insights.Service.Data.CustomModels
{
    public class DiseaseJoinResult
    {
        public int DiseaseId { get; set; }
        
        public string DiseaseName { get; set; }
        
        public int OutbreakPotentialAttributeId { get; set; }
        
        public IEnumerable<string> Agents { get; set; }
        
        public IEnumerable<string> AgentTypes { get; set; }
        
        public IEnumerable<AcquisitionModeModel> AcquisitionModes { get; set; }
        
        public IEnumerable<string> TransmissionModes { get; set; }
        
        public IEnumerable<string> PreventionMeasures { get; set; }
        
        public DiseaseSpeciesIncubation IncubationPeriod { get; set; }

        public string BiosecurityRisk { get; set; }
        
        public IEnumerable<XtblUserDiseaseRelevance> UserDiseaseRelevance { get; set; }
    }
}