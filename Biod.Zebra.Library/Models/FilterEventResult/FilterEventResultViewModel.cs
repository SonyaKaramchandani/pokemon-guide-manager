using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;

namespace Biod.Zebra.Library.Models.FilterEventResult
{
    public class FilterEventResultViewModel
    {
        // TODO: Move to Configuration
        private const decimal Threshold = 0.01m;
        
        public FilterParamsModel FilterParams { get; set; }
        
        public List<DiseaseGroupResultViewModel> DiseaseGroups { get; set; }

        public int TotalResults { get; set; }

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
            
            return new FilterEventResultViewModel
            {
                FilterParams = eventsInfoViewModel.FilterParams,
                DiseaseGroups = diseaseGroups
                    .Where(g => !userRelevanceSettings.NeverNotifyDiseaseIds.Contains(g.Key.DiseaseId))
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
                                .Where(e => userRelevanceSettings.AlwaysNotifyDiseaseIds.Contains(g.Key.DiseaseId)
                                            || userRelevanceSettings.RiskOnlyDiseaseIds.Contains(g.Key.DiseaseId) && (e.LocalSpread || e.ImportationProbabilityMax >= Threshold))
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
                TotalResults = eventsInfoViewModel.EventsInfo.Select(e => e.EventId).Distinct().Count()
            };
        }

        public static FilterEventResultViewModel FromFilterEventsInfoViewModel(EventsInfoViewModel eventsInfoViewModel)
        {
            var diseaseGroups = eventsInfoViewModel.EventsInfo
                .Where(e => e.DiseaseId > 0)
                .GroupBy(e => new { e.DiseaseId, e.DiseaseName})
                .OrderBy(g => g.Key.DiseaseName);
            
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
                                .Where(e => !eventsInfoViewModel.FilterParams.locationOnly || e.LocalSpread || e.ImportationProbabilityMax >= Threshold)
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
                TotalResults = eventsInfoViewModel.EventsInfo.Select(e => e.EventId).Distinct().Count()
            };
        }
    }
}