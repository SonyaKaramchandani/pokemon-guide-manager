using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Service.Models.HealthCareWorker
{
    public class PostCaseModel
    {
        public string UserId { get; set; }

        [Required] public int GeonameId { get; set; }

        [Required] public DateTimeOffset ArrivalDate { get; set; }

        [Required] public DateTimeOffset DepartureDate { get; set; }

        [Required] public DateTimeOffset SymptomOnsetDate { get; set; }

        [Required] public List<string> PrimarySyndromes { get; set; }

        public override string ToString()
        {
            var primarySyndromesString = string.Join(",", PrimarySyndromes);
            return $"{UserId}, {GeonameId}, {ArrivalDate}, {DepartureDate}, {SymptomOnsetDate}, {primarySyndromesString}";
        }
    }
}