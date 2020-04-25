using System.Collections.Generic;

namespace Biod.Insights.Service.Models.HealthCareWorker
{
    public class RefineCaseModel
    {
        public int CaseId { get; set; }
        public List<ActivityAnswer> ActivityAnswers { get; set; }
        public List<VaccineAnswer> VaccineAnswers { get; set; }
    }
}