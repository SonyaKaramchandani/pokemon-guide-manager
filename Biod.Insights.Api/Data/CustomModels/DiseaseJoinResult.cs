using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class DiseaseJoinResult
    {
        public Diseases Disease { get; set; }
        
        public BiosecurityRisk BiosecurityRisk { get; set; }
        
        public OutbreakPotentialCategory OutbreakPotentialCategory { get; set; }
    }
}