namespace Biod.Insights.Service.Models.HealthCareWorker
{
    public class UpdateCaseModel
    {
        public int CaseId { get; set; }
        public int DiagnosedDiseaseId { get; set; }
        public string OtherDiagnosedDiseaseName { get; set; }
    }
}