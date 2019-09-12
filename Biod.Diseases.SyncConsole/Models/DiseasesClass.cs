using System;

namespace Biod.Diseases.SyncConsole.Models
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
        public string type { get; set; }
        public int? diseaseParentId { get; set; }
        public string pronunciation { get; set; }
        public string severityLevel { get; set; }
        public string biosecurityRisk { get; set; }
        public int? outbreakPotential { get; set; }
        public object syndromeDescription { get; set; }
        public bool? isChronic { get; set; }
        public string treatmentAvailable { get; set; }
        public Alternatediseasename[] alternateDiseaseNames { get; set; }
        public Pathogen[] pathogens { get; set; }
        public Transmissionmode[] transmissionModes { get; set; }
        public Diseasesymptom[] diseaseSymptoms { get; set; }
        public Incubation incubation { get; set; }
        public Symptomaticperiod symptomaticPeriod { get; set; }
        public Prevention[] preventions { get; set; }
        public Environmentalfactor[] environmentalFactors { get; set; }
        public DateTime lastModified { get; set; }
    }

    public class Incubation
    {
        public float minimumDays { get; set; }
        public float maximumDays { get; set; }
        public float averageDays { get; set; }
        public string notes { get; set; }
        public string source { get; set; }
    }

    public class Symptomaticperiod
    {
        public float minimumDays { get; set; }
        public float maximumDays { get; set; }
        public float averageDays { get; set; }
        public string notes { get; set; }
        public string source { get; set; }
    }

    public class Alternatediseasename
    {
        public int alternateNameId { get; set; }
        public string alternateName { get; set; }
        public bool isColloquial { get; set; }
        public bool isSearchTerm { get; set; }
        public string language { get; set; }
    }

    public class Pathogen
    {
        public int pathogenId { get; set; }
        public string pathogen { get; set; }
        public string pathogenType { get; set; }
        public int pathogenTypeId { get; set; }
    }

    public class Transmissionmode
    {
        public int transmissionModeId { get; set; }
        public string transmissionMode { get; set; }
        public int rank { get; set; }
        public string contact { get; set; }
        public string agents { get; set; }
        public string actions { get; set; }
    }

    public class Diseasesymptom
    {
        public int symptomId { get; set; }
        public string symptom { get; set; }
        public string system { get; set; }
        public int associationScore { get; set; }
        public string frequency { get; set; }
    }

    public class Prevention
    {
        public int preventionId { get; set; }
        public string preventionType { get; set; }
        public bool oral { get; set; }
        public float riskReduction { get; set; }
        public string duration { get; set; }
        public string preventionAccessibility { get; set; }
        public bool travel { get; set; }
        public int categoryId { get; set; }
    }

    public class Environmentalfactor
    {
        public int environmentalFactorId { get; set; }
        public string environmentalFactor { get; set; }
    }

}

