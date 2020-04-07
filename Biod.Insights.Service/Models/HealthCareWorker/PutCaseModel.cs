namespace Biod.Insights.Service.Models.HealthCareWorker
{
    public class PutCaseModel
    {
        public int CaseId { get; set; }
        public int DiagnosedDiseaseId { get; set; }
        public string OtherDiagnosedDiseaseName { get; set; }

        public override string ToString()
        {
            return $"{CaseId}, {DiagnosedDiseaseId}, {OtherDiagnosedDiseaseName}";
        }
    }
}