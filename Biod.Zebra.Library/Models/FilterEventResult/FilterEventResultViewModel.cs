using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models.Map;

namespace Biod.Zebra.Library.Models.FilterEventResult
{
    public class FilterEventResultViewModel
    {
        // TODO: Move to Configuration
        private const decimal Threshold = 0.01m;
        
        public FilterParamsModel FilterParams { get; set; }
        
        public List<DiseaseGroupResultViewModel> DiseaseGroups { get; set; }

        public int TotalResults { get; set; }
        
        public MapPinModel MapPinModel { get; set; }

        /// <summary>
        /// Constructs the model from the events info view model for a custom events filter (uses user settings)
        /// </summary>
        /// <param name="dbContext">the db context</param>
        /// <param name="eventsInfoViewModel">the model</param>
        /// <param name="user">the current user</param>
        /// <returns>the remapped model</returns>
        public static FilterEventResultViewModel FromCustomEventsInfoViewModel(ApplicationUser user, BiodZebraEntities dbContext, EventsInfoViewModel eventsInfoViewModel)
        {
            var userRelevanceSettings = AccountHelper.GetUserRelevanceSettings(dbContext, user);
            
            var diseaseGroups = eventsInfoViewModel.EventsInfo
                .Where(e => e.DiseaseId > 0)
                .GroupBy(e => new { e.DiseaseId, e.DiseaseName})
                .OrderBy(g => g.Key.DiseaseName);
            
            var mapPinModel = new MapPinModel
            {
                EventsMap = eventsInfoViewModel.EventsMap,
                MapPinEventModels = eventsInfoViewModel.EventsInfo
                    .Where(e => userRelevanceSettings.AlwaysNotifyDiseaseIds.Contains(e.DiseaseId) // Show pin if the disease is marked as always notify 
                                || e.LocalSpread || e.ImportationProbabilityMax >= Threshold)      // Show pin if it is local spread or has risk
                    .Select(MapPinEventModel.FromEventsInfoModel)
            };

            return new FilterEventResultViewModel
            {
                FilterParams = eventsInfoViewModel.FilterParams,
                DiseaseGroups = diseaseGroups
                    .Where(g => !userRelevanceSettings.NeverNotifyDiseaseIds.Contains(g.Key.DiseaseId)) // Remove all diseases that are marked as never notify
                    .Select(g =>
                    {
                        var minTravellerSum = g.Sum(e => e.ImportationInfectedTravellersMin < 0 ? 0 : e.ImportationInfectedTravellersMin);
                        var maxTravellerSum = g.Sum(e => e.ImportationInfectedTravellersMax < 0 ? 0 : e.ImportationInfectedTravellersMax);
                        var aggregatedRisk = RiskProbabilityHelper.GetAggregatedRiskOfAnyEvent(g.Select(e => e.ImportationProbabilityMax));
                        return new DiseaseGroupResultViewModel
                        {
                            DiseaseId = g.Key.DiseaseId,
                            DiseaseName = g.Key.DiseaseName,
                            ShownEvents = g
                                .Where(e => userRelevanceSettings.AlwaysNotifyDiseaseIds.Contains(g.Key.DiseaseId) // Include if the disease is marked as always notify 
                                            || e.LocalSpread || e.ImportationProbabilityMax >= Threshold)          // Include if it is local spread or has risk
                                .Select(EventResultViewModel.FromEventsInfoModel)
                                .ToList(),
                            HiddenEvents = g
                                .Where(e => userRelevanceSettings.RiskOnlyDiseaseIds.Contains(g.Key.DiseaseId) && !e.LocalSpread && e.ImportationProbabilityMax < Threshold)
                                .Select(EventResultViewModel.FromEventsInfoModel)
                                .ToList(),
                            TravellersText = maxTravellerSum >= Threshold ? StringFormattingHelper.GetTravellerInterval(minTravellerSum, maxTravellerSum, true) : "Negligible",
                            IsAllShown = userRelevanceSettings.AlwaysNotifyDiseaseIds.Contains(g.Key.DiseaseId),
                            IsVisible = aggregatedRisk >= Threshold || g.Any(e => e.LocalSpread)
                        };
                    })
                    .ToList(),
                TotalResults = eventsInfoViewModel.EventsInfo.Select(e => e.EventId).Distinct().Count(),
                MapPinModel = mapPinModel
            };
        }

        public static FilterEventResultViewModel FromFilterEventsInfoViewModel(EventsInfoViewModel eventsInfoViewModel)
        {
            var diseaseGroups = eventsInfoViewModel.EventsInfo
                .Where(e => e.DiseaseId > 0)
                .GroupBy(e => new { e.DiseaseId, e.DiseaseName})
                .OrderBy(g => g.Key.DiseaseName);
            
            var mapPinModel = new MapPinModel
            {
                EventsMap = eventsInfoViewModel.EventsMap,
                MapPinEventModels = eventsInfoViewModel.EventsInfo
                    .Where(e => !eventsInfoViewModel.FilterParams.locationOnly // Show pin if the toggle for Location Only is false
                        || e.ImportationProbabilityMax >= Threshold || e.LocalSpread) // Show pin if it is local spread or has risk
                    .Select(MapPinEventModel.FromEventsInfoModel)
            };
            
            return new FilterEventResultViewModel
            {
                FilterParams = eventsInfoViewModel.FilterParams,
                DiseaseGroups = diseaseGroups
                    .Select(g =>
                    {
                        var minTravellerSum = g.Sum(e => e.ImportationInfectedTravellersMin < 0 ? 0 : e.ImportationInfectedTravellersMin);
                        var maxTravellerSum = g.Sum(e => e.ImportationInfectedTravellersMax < 0 ? 0 : e.ImportationInfectedTravellersMax);
                        var aggregatedRisk = RiskProbabilityHelper.GetAggregatedRiskOfAnyEvent(g.Select(e => e.ImportationProbabilityMax));
                        return new DiseaseGroupResultViewModel
                        {
                            DiseaseId = g.Key.DiseaseId,
                            DiseaseName = g.Key.DiseaseName,
                            ShownEvents = g
                                .Where(e => !eventsInfoViewModel.FilterParams.locationOnly                // Include if the toggle for Location Only is false
                                            || e.LocalSpread || e.ImportationProbabilityMax >= Threshold) // Include if the disease is marked as always notify 
                                .Select(EventResultViewModel.FromEventsInfoModel)
                                .ToList(),
                            HiddenEvents = g
                                .Where(e => eventsInfoViewModel.FilterParams.locationOnly && !e.LocalSpread && e.ImportationProbabilityMax < Threshold)
                                .Select(EventResultViewModel.FromEventsInfoModel)
                                .ToList(),
                            TravellersText = maxTravellerSum >= Threshold ? StringFormattingHelper.GetTravellerInterval(minTravellerSum, maxTravellerSum, true) : "Negligible",
                            IsAllShown = !eventsInfoViewModel.FilterParams.locationOnly,
                            IsVisible = aggregatedRisk >= Threshold || g.Any(e => e.LocalSpread)
                        };
                    })
                    .ToList(),
                TotalResults = eventsInfoViewModel.EventsInfo.Select(e => e.EventId).Distinct().Count(),
                MapPinModel = mapPinModel
            };
        }
    }
}