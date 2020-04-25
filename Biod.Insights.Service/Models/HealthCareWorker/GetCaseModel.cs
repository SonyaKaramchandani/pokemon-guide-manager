using Newtonsoft.Json;
using System;

namespace Biod.Insights.Service.Models.HealthCareWorker
{
    public class GetCaseModel
    {
        public int CaseId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastUpdatedDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? GeonameId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ArrivalDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DepartureDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? SymptomOnsetDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] PrimarySyndromes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InitialCaseOutput { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RefinementDiseaseIds { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RefinementQuestions { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RefinementAnswers { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RefinementOutput { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? DiagnosedDiseaseId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OtherDiagnosedDiseaseName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        public string LocationName { get; set; }
        public string CountryName { get; set; }
    }
}