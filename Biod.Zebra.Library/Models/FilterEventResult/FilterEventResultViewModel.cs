using System.Collections.Generic;
using System.Linq;

namespace Biod.Zebra.Library.Models.FilterEventResult
{
    public class FilterEventResultViewModel
    {
        public FilterParamsModel FilterParams { get; set; }
        
        public List<DiseaseGroupResultViewModel> DiseaseGroups { get; set; }

        public int TotalResults { get; set; }

        /// <summary>
        /// Constructs the model from the events info view model
        /// </summary>
        /// <param name="eventsInfoViewModel">the model</param>
        /// <returns>the remapped model</returns>
        public static FilterEventResultViewModel FromEventsInfoViewModel(EventsInfoViewModel eventsInfoViewModel)
        {
            var diseaseGroups = eventsInfoViewModel.EventsInfo
                .Where(e => e.DiseaseId > 0)
                .GroupBy(e => new { e.DiseaseId, e.DiseaseName})
                .OrderBy(g => g.Key.DiseaseName);
            // TODO: Move to Configuration
            const decimal threshold = 0.01m;
            
            return new FilterEventResultViewModel
            {
                FilterParams = eventsInfoViewModel.FilterParams,
                DiseaseGroups = diseaseGroups
                    .Select(g =>
                    {
                        return new DiseaseGroupResultViewModel
                        {
                            DiseaseId = g.Key.DiseaseId,
                            DiseaseName = g.Key.DiseaseName,
                            ShownEvents = g
                                .Where(e => e.LocalSpread || e.ImportationProbabilityMax >= threshold)
                                .Select(EventResultViewModel.FromEventsInfoModel)
                                .ToList(),
                            HiddenEvents = g
                                .Where(e => !e.LocalSpread && e.ImportationProbabilityMax < threshold)
                                .Select(EventResultViewModel.FromEventsInfoModel)
                                .ToList(),
                        };
                    })
                    .ToList(),
                TotalResults = eventsInfoViewModel.EventsInfo.Count()
            };
        }
    }
}