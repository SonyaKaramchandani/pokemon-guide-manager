using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models;

namespace Biod.Zebra.Library.Infrastructures.FilterEventResult
{
    public class EventResultGroupHelper
    {
        /// <summary>
        /// Default group by if no rows in the database have the IsDefault set to true
        /// </summary>
        private const int DEFAULT_GROUP_BY = Constants.GroupByFieldTypes.DISEASE_RISK;
        
        public static int GetDefaultGroupingOption(BiodZebraEntities dbContext)
        {
            return dbContext.EventGroupByFields.FirstOrDefault(o => o.IsDefault)?.Id ?? DEFAULT_GROUP_BY;
        }

        public static List<EventsInfoModel> GroupEvents(
            int groupTypeId, 
            List<EventsInfoModel> eventsInfo, 
            IEnumerable<usp_ZebraDashboardGetInterventionMethods_Result> interventionMethods)
        {
            var eventsQuery = eventsInfo.AsQueryable();
            
            switch (groupTypeId)
            {
                case Constants.GroupByFieldTypes.TRANSMISSION_MODE:
                    // An event may have more than 1 transmission mode. To handle this properly, the event
                    // needs to be cloned for each transmission mode type so that the event will appear
                    // under each group section. This logic was migrated and cleaned up and can be improved.
                    // TODO: Refactor to use a Grouped Result model
                    eventsQuery = eventsInfo.SelectMany(e =>
                        {
                            if (e.Transmissions == null)
                            {
                                e.Group = "1-No Information";
                                return new[] {e};
                            }

                            return e.Transmissions.Split(',')
                                .Select(t =>
                                {
                                    var clone = e.DeepCopy();
                                    clone.Group = $"0-{t.Trim()}";
                                    return clone;
                                });
                        })
                        .AsQueryable();
                    break;
                case Constants.GroupByFieldTypes.BIOSECURITY_RISK:
                    eventsQuery = eventsInfo.Select(e =>
                        {
                            if (e.BiosecurityRisk == null || e.BiosecurityRisk.ToLower() == "no")
                            {
                                e.Group = "No/unknown risk";
                            }
                            else
                            {
                                e.Group = "Category " + e.BiosecurityRisk;
                            }

                            return e;
                        })
                        .AsQueryable();
                    break;
                case Constants.GroupByFieldTypes.PREVENTION_MEASURE:
                    var preventionTypes = new HashSet<string>(interventionMethods.Select(i => i.InterventionDisplayName));
                    eventsQuery = eventsInfo.Select(e =>
                        {
                            e.Group = preventionTypes.Contains(e.Interventions) ? e.Interventions : Constants.PreventionTypes.BEHAVIOURAL;
                            return e;
                        })
                        .AsQueryable();
                    break;
            }

            return eventsQuery.ToList();
        }
    }
}