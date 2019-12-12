using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Models;

namespace Biod.Insights.Api.Helpers
{
    public static class RiskCalculationHelper
    {
        public static RiskModel CalculateRiskCompat(List<usp_ZebraEventGetEventSummary_Result> events)
        {
            var minMagnitude = events.Select(e => (float) (e.ImportationInfectedTravellersMin ?? 0)).Sum();
            var maxMagnitude = events.Select(e => (float) (e.ImportationInfectedTravellersMax ?? 0)).Sum();
            var minProbability = GetAggregatedRiskOfAnyEvent(events.Select(e => (float) (e.ImportationMinProbability ?? 0)));
            var maxProbability = GetAggregatedRiskOfAnyEvent(events.Select(e => (float) (e.ImportationMaxProbability ?? 0)));
            
            return new RiskModel
            {
                MinMagnitude = minMagnitude,
                MaxMagnitude = maxMagnitude,
                MinProbability = minProbability,
                MaxProbability = maxProbability
            };
        }

        /// <summary>
        /// Calculates the aggregated risk of at least one of the events being a risk, with assumption
        /// that the events are independent.
        ///
        /// Pre-conditions: list must be non-empty and each risk must be between 0 and 1 inclusive.
        /// </summary>
        /// <returns>the aggregated risk</returns>
        public static float GetAggregatedRiskOfAnyEvent(IEnumerable<float> riskProbability)
        {
            // Get the probability of each event NOT importing, then multiply it all together to get the
            // risk of none of the events being imported. Subtract from 1 to get the probability of any
            // event being imported.
            return 1 - riskProbability.Select(r => 1 - r).Aggregate((a, b) => a * b);
        }
    }
}