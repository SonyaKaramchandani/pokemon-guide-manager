namespace Biod.Insights.Service.Models.HealthCareWorker
{
    // Class generated using Unleash OpenSpec Api VStudio extension
    // https://medium.com/@unchase/how-to-generate-c-or-typescript-client-code-for-openapi-swagger-specification-d882d59e3b77
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.11.0 (Newtonsoft.Json v12.0.0.2)")]
    public class ApiGetHealthCareWorkerModel
    {
        /// <summary>Unique BlueDot Id of a disease, (int)</summary>
        [Newtonsoft.Json.JsonProperty("diseaseId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? DiseaseId { get; set; }

        /// <summary>Name of the disease, (varchar(64))</summary>
        [Newtonsoft.Json.JsonProperty("disease", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Disease { get; set; }

        /// <summary>Type of agent that causes the disease, (nvarchar(512))</summary>
        [Newtonsoft.Json.JsonProperty("agentType", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string AgentType { get; set; }

        /// <summary>Whether there is a treatment available for the disease, (varchar(10))</summary>
        [Newtonsoft.Json.JsonProperty("treatmentAvailable", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string TreatmentAvailable { get; set; }

        /// <summary>The severity of a disease, based on mortality rate and physical symptoms, (varchar(10))</summary>
        [Newtonsoft.Json.JsonProperty("severityLevel", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string SeverityLevel { get; set; }

        /// <summary>Subjective weight (a.k.a. scaling/Isaac factor) that essentially accounts for quirks in the likeliness of acquiring a disease, (float)</summary>
        [Newtonsoft.Json.JsonProperty("hcwModelWeight", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? HcwModelWeight { get; set; }

        /// <summary>The maximum acquisition multiplier of acquisitionModeRank = 1. The acquisition multiplier values capture ease of acquisition by humans, values are 1-10 (int)</summary>
        [Newtonsoft.Json.JsonProperty("rank1MaxAcquisitionMultiplier", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Rank1MaxAcquisitionMultiplier { get; set; }

        [Newtonsoft.Json.JsonProperty("rank1MaxAcquisitionModeLabel", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> Rank1MaxAcquisitionModeLabel { get; set; }

        [Newtonsoft.Json.JsonProperty("syndromes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> Syndromes { get; set; }

        [Newtonsoft.Json.JsonProperty("category1Syndromes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> Category1Syndromes { get; set; }

        [Newtonsoft.Json.JsonProperty("category2Syndromes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> Category2Syndromes { get; set; }

        [Newtonsoft.Json.JsonProperty("activities", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> Activities { get; set; }

        /// <summary>Indicates whether a prevention method is available including vaccine and prophylaxis, yes or no, (varchar(64))</summary>
        [Newtonsoft.Json.JsonProperty("preventionAvailable", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PreventionAvailable { get; set; }

        /// <summary>The amount the risk of acquiring a disease is reduced from taking the prevention, (float)</summary>
        [Newtonsoft.Json.JsonProperty("riskReduction", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double RiskReduction { get; set; }

        /// <summary>Category of interventionType, denotes specific type of intervention (varchar(50))</summary>
        [Newtonsoft.Json.JsonProperty("interventionCategory", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string InterventionCategory { get; set; }

        /// <summary>George prevalence value for a disease, (float)</summary>
        [Newtonsoft.Json.JsonProperty("diseasePrev", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? DiseasePrev { get; set; }

        /// <summary>Indicates whether the disease has seasonality data in the specified seasonality zone</summary>
        [Newtonsoft.Json.JsonProperty("hasSeasonalityData", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string HasSeasonalityData { get; set; }

        /// <summary>Indicates whether the disease is off season during the travel date</summary>
        [Newtonsoft.Json.JsonProperty("isOffSeason", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IsOffSeason { get; set; }

        /// <summary>Magnitude/weight of modifier on the disease during offSeason, (varchar(100))</summary>
        [Newtonsoft.Json.JsonProperty("offSeasonWeight", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string OffSeasonWeight { get; set; }

        /// <summary>George prevalence value for a disease that is weighted by off season weight, (float)</summary>
        [Newtonsoft.Json.JsonProperty("diseasePrev_offSeasonWeighted", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? DiseasePrev_offSeasonWeighted { get; set; }

        /// <summary>Approximate minimum number of days for disease incubation to begin, (float)</summary>
        [Newtonsoft.Json.JsonProperty("minimumDays", Required = Newtonsoft.Json.Required.Always)]
        public double MinimumDays { get; set; }

        /// <summary>Approximate maximum number of days for disease incubation to end, (float)</summary>
        [Newtonsoft.Json.JsonProperty("maximumDays", Required = Newtonsoft.Json.Required.Always)]
        public double MaximumDays { get; set; }

        /// <summary>Approximate average number of days for disease incubation to last, (float)</summary>
        [Newtonsoft.Json.JsonProperty("averageDays", Required = Newtonsoft.Json.Required.Always)]
        public double AverageDays { get; set; }

        /// <summary>Indicates whether the person is Within, Before, or After the incubation period</summary>
        [Newtonsoft.Json.JsonProperty("checkIncubationPeriod", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CheckIncubationPeriod { get; set; }

        /// <summary>Indicates on the symptom onset date, how many days it has been from the incubation period. Negative value means days before min incubation starts. 0 means within incubation period. Positive value means days after the max incubation period.</summary>
        [Newtonsoft.Json.JsonProperty("daysFromIncubationWhenSymptomOnset", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? DaysFromIncubationWhenSymptomOnset { get; set; }

        /// <summary>Indicates whether the disease is endemic in the region</summary>
        [Newtonsoft.Json.JsonProperty("isEndemic", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? IsEndemic { get; set; }

        /// <summary>Indicates whether the disease is a disease outbreak event in the region</summary>
        [Newtonsoft.Json.JsonProperty("isDiseaseEvent", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? IsDiseaseEvent { get; set; }

        /// <summary>Indicates whether the disease is a disease is communicable to a healthcare worker, yes or no</summary>
        [Newtonsoft.Json.JsonProperty("isCommunicable", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IsCommunicable { get; set; }

        /// <summary>A HCW value that shows indicates the risk and probability of getting a disease, calculated by diseasePrevalence * offSeasonWeight (if off season) * hcwModelWeight * rank1MaxAcquisitionMultiplier, (float)</summary>
        [Newtonsoft.Json.JsonProperty("acquisitionRiskOutput", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? AcquisitionRiskOutput { get; set; }

        /// <summary>A tiering system for syndromes, 1-3, (int)</summary>
        [Newtonsoft.Json.JsonProperty("syndromeTier", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? SyndromeTier { get; set; }

        /// <summary>Indicates whether the person is within incubation criteria, which means within 3 days from the incubation period.</summary>
        [Newtonsoft.Json.JsonProperty("withinIncubationCriteria", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? WithinIncubationCriteria { get; set; }

        [Newtonsoft.Json.JsonProperty("acquisitionModes", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> AcquisitionModes { get; set; }
    }
}