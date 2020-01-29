using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Models;

namespace Biod.Insights.Api.Helpers
{
    public static class RiskCalculationHelper
    {
        public static RiskModel CalculateImportationRisk(List<EventJoinResult> events)
        {
            var modelNotRun = events.All(e => e.Event.IsLocalOnly)
                              || events.All(e => e.XtblEventLocations.All(x => x.LocationType == (int) Constants.LocationType.Country));
            
            var minMagnitude = !modelNotRun ? events.Select(e => (float) (e.ImportationRisk?.MinVolume ?? 0)).Sum() : 0;
            var maxMagnitude = !modelNotRun ? events.Select(e => (float) (e.ImportationRisk?.MaxVolume ?? 0)).Sum() : 0;
            var minProbability = events.Any() && !modelNotRun ? GetAggregatedRiskOfAnyEvent(events.Select(e => (float) (e.ImportationRisk?.MinProb ?? 0))) : 0;
            var maxProbability = events.Any() && !modelNotRun ? GetAggregatedRiskOfAnyEvent(events.Select(e => (float) (e.ImportationRisk?.MaxProb ?? 0))) : 0;
            
            return new RiskModel
            {
                IsModelNotRun = modelNotRun,
                MinMagnitude = minMagnitude,
                MaxMagnitude = maxMagnitude,
                MinProbability = minProbability,
                MaxProbability = maxProbability
            };
        }
        
        public static RiskModel CalculateExportationRisk(List<EventJoinResult> events)
        {
            var modelNotRun = events.All(e => e.Event.IsLocalOnly)
                              || events.All(e => e.XtblEventLocations.All(x => x.LocationType == (int) Constants.LocationType.Country));
            
            var minMagnitude = !modelNotRun ? events.Select(e => (float) (e.Event.EventExtension?.MinExportationVolumeViaAirports ?? 0)).Sum() : 0;
            var maxMagnitude = !modelNotRun ? events.Select(e => (float) (e.Event.EventExtension?.MaxExportationVolumeViaAirports ?? 0)).Sum() : 0;
            var minProbability = events.Any() && !modelNotRun ? GetAggregatedRiskOfAnyEvent(events.Select(e => (float) (e.Event.EventExtension?.MinExportationProbabilityViaAirports ?? 0))) : 0;
            var maxProbability = events.Any() && !modelNotRun ? GetAggregatedRiskOfAnyEvent(events.Select(e => (float) (e.Event.EventExtension?.MaxExportationProbabilityViaAirports ?? 0))) : 0;
            
            return new RiskModel
            {
                IsModelNotRun = modelNotRun,
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