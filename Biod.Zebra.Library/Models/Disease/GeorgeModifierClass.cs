using System;

namespace Biod.Zebra.Library.Models.Disease
{


    public class GeorgeModifiers
    {
        public GeorgeModifierClass[] GeorgeModifierList { get; set; }
    }

    public class GeorgeModifierClass
    {
        public int diseaseId { get; set; }
        public bool vettedForProduct { get; set; }
        public int presenceBitmask { get; set; }
        public float modelWeight { get; set; }
        public float preventability { get; set; }
        public string severityLevel { get; set; }
        public string treatmentAvailable { get; set; }
        public bool isChronic { get; set; }
        public string notes { get; set; }
        public DateTime lastModified { get; set; }
        public Interventionmodifier[] interventionModifiers { get; set; }
        public Severitymodifier[] severityModifiers { get; set; }
        public Activitymodifier[] activityModifiers { get; set; }
        public Seasonalitymodifier[] seasonalityModifiers { get; set; }
    }

    public class Interventionmodifier
    {
        public int interventionId { get; set; }
        public int interventionTypeId { get; set; }
        public string interventionType { get; set; }
        public string interventionCategoryId { get; set; }
        public string interventionCategory { get; set; }
        public int conditionId { get; set; }
        public string condition { get; set; }
        public int messageId { get; set; }
        public int categoryId { get; set; }
        public string category { get; set; }
        public DateTime lastModified { get; set; }
    }

    public class Severitymodifier
    {
        public int conditionId { get; set; }
        public string condition { get; set; }
        public int addend { get; set; }
        public float conditionParameter { get; set; }
        public int categoryId { get; set; }
        public string categoryLabel { get; set; }
        public DateTime lastModified { get; set; }
    }

    public class Activitymodifier
    {
        public int activityId { get; set; }
        public string activity { get; set; }
        public DateTime lastModified { get; set; }
        public float scale { get; set; }
    }

    public class Seasonalitymodifier
    {
        public int zoneId { get; set; }
        public string zone { get; set; }
        public int fromMonth { get; set; }
        public int toMonth { get; set; }
        public float offSeasonWeight { get; set; }
        public DateTime lastModified { get; set; }
    }



}