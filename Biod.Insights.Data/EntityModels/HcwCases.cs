using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class HcwCases
    {
        public int HcwCaseId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastUpdatedDate { get; set; }
        public int GeonameId { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset SymptomOnsetDate { get; set; }
        public string PrimarySyndromes { get; set; }
        public string InitialCaseOutput { get; set; }
        public string RefinementBySymptomsDiseaseIds { get; set; }
        public string RefinementBySymptomsQuestions { get; set; }
        public string RefinementBySymptomsAnswers { get; set; }
        public string RefinementBySymptomsOutput { get; set; }
        public string RefinementByActivitiesAndVaccinesDiseaseIds { get; set; }
        public string RefinementByActivitiesAndVaccinesQuestions { get; set; }
        public string RefinementByActivitiesAndVaccinesAnswers { get; set; }
        public string RefinementByActivitiesAndVaccinesOutput { get; set; }
        public int? DiagnosedDiseaseId { get; set; }
        public string OtherDiagnosedDiseaseName { get; set; }

        public virtual Geonames Geoname { get; set; }
        public virtual UserProfile User { get; set; }
    }
}
