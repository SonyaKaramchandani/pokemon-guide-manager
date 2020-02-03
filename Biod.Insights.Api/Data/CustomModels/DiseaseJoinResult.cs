using System.Collections.Generic;
using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class DiseaseJoinResult
    {
        public Diseases Disease { get; set; }

        public BiosecurityRisk BiosecurityRisk { get; set; }

        public IEnumerable<OutbreakPotentialCategory> OutbreakPotentialCategory { get; set; }
        
        public IEnumerable<XtblUserDiseaseRelevance> UserDiseaseRelevance { get; set; }
    }
}