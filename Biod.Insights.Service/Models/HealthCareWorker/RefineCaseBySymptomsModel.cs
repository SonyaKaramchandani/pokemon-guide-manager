using System.Collections.Generic;

namespace Biod.Insights.Service.Models.HealthCareWorker
{
    public class RefineCaseBySymptomsModel
    {
        public int CaseId { get; set; }
        public List<SymptomAnswer> SymptomAnswers { get; set; }
    }
}