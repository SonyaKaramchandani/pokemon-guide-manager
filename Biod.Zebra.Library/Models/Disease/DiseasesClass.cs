using System;

namespace Biod.Zebra.Library.Models.Disease
{

    using System;



    public class Diseases
    {
        public DiseaseClass[] DiseaseList { get; set; }
    }

    public class DiseaseClass
    {
        public int diseaseId { get; set; }
        public string disease { get; set; }
        public string diseaseType { get; set; }
        public bool? isZoonotic { get; set; }
        public int? diseaseParentId { get; set; }
        public string pronunciation { get; set; }
        public string severityLevel { get; set; }
        public string biosecurityRisk { get; set; }
        public int? outbreakPotential { get; set; }
        public object syndromeDescription { get; set; }
        public bool? isChronic { get; set; }
        public string treatmentAvailable { get; set; }
        public Alternatediseasename[] alternateDiseaseNames { get; set; }
        public Agent[] agents { get; set; }
        public Transmissionmode[] transmissionModes { get; set; }
        public Diseasesymptom[] diseaseSymptoms { get; set; }
        public Incubation[] incubation { get; set; }
        public Symptomaticperiod[] symptomaticPeriod { get; set; }
        public Intervention[] interventions { get; set; }
        public Environmentalfactor[] environmentalFactors { get; set; }
        public DateTime lastModified { get; set; }
    }

    public class Alternatediseasename
    {
        public int alternateNameId { get; set; }
        public string alternateName { get; set; }
        public bool isColloquial { get; set; }
        public bool isSearchTerm { get; set; }
        public string lang { get; set; }
    }

    public class Agent
    {
        public int agentId { get; set; }
        public string agent { get; set; }
        public string agentType { get; set; }
        public int agentTypeId { get; set; }
    }

    public class Transmissionmode
    {
        public int transmissionModeId { get; set; }
        public string transmissionMode { get; set; }
        public string transmissionModeDisplayName { get; set; }
        public int speciesId { get; set; }
        public string speciesName { get; set; }
        public int transmissionRank { get; set; }
        public string contact { get; set; }
        public string reservoirVector { get; set; }
        public string actions { get; set; }
    }

    public class Diseasesymptom
    {
        public int symptomId { get; set; }
        public string symptom { get; set; }
        public int speciesId { get; set; }
        public string speciesName { get; set; }
        public string system { get; set; }
        public int associationScore { get; set; }
        public string frequency { get; set; }
    }

    public class Incubation
    {
        public long minimumSeconds { get; set; }
        public long maximumSeconds { get; set; }
        public long averageSeconds { get; set; }
        public float approximateMinimumDays { get; set; }
        public float approximateMaximumDays { get; set; }
        public float approximateAverageDays { get; set; }
        public int speciesId { get; set; }
        public string speciesName { get; set; }
        public string notes { get; set; }
        public string dataSource { get; set; }
    }

    public class Symptomaticperiod
    {
        public int minimumSeconds { get; set; }
        public int maximumSeconds { get; set; }
        public int averageSeconds { get; set; }
        public float approximateMinimumDays { get; set; }
        public float approximateMaximumDays { get; set; }
        public float approximateAverageDays { get; set; }
        public int speciesId { get; set; }
        public string speciesName { get; set; }
        public string notes { get; set; }
        public string dataSource { get; set; }
    }

    public class Intervention
    {
        public int interventionId { get; set; }
        public int interventionTypeId { get; set; }
        public string interventionType { get; set; }
        public string interventionCategoryId { get; set; }
        public string interventionCategory { get; set; }
        public int speciesId { get; set; }
        public string speciesName { get; set; }
        public bool oral { get; set; }
        public float riskReduction { get; set; }
        public string duration { get; set; }
        public string interventionAccessibility { get; set; }
        public bool travel { get; set; }
        public int categoryId { get; set; }
    }

    public class Environmentalfactor
    {
        public int environmentalFactorId { get; set; }
        public string environmentalFactor { get; set; }
    }


}

