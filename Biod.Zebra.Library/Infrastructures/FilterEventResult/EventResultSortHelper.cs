using System;
using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models;

namespace Biod.Zebra.Library.Infrastructures.FilterEventResult
{
    public static class EventResultSortHelper
    {
        /// <summary>
        /// Default order by if no rows in the database have the IsDefault set to true
        /// </summary>
        private const int DEFAULT_ORDER_BY = Constants.OrderByFieldTypes.RISK_OF_IMPORTATION;
        
        public static int GetDefaultSortingOption(BiodZebraEntities dbContext)
        {
            return dbContext.EventOrderByFields.FirstOrDefault(o => o.IsDefault)?.Id ?? DEFAULT_ORDER_BY;
        }

        public static List<EventsInfoModel> SortEvents(int orderById, List<EventsInfoModel> eventsInfo)
        {
            var eventsQuery = eventsInfo.AsQueryable();
            switch (orderById)
            {
                case Constants.OrderByFieldTypes.EVENT_START_DATE:
                    eventsQuery = eventsQuery.OrderByDescending(e => e.StartDate, new CustomComparer.StartDate());
                    break;
                case Constants.OrderByFieldTypes.RISK_OF_EXPORTATION:
                    eventsQuery = eventsQuery.OrderByDescending(e => e.ExportationInfectedTravellersMin);
                    break;
                case Constants.OrderByFieldTypes.CASE_COUNT:
                    eventsQuery = eventsQuery.OrderByDescending(e => e.RepCases);
                    break;
                case Constants.OrderByFieldTypes.DEATH_COUNT:
                    eventsQuery = eventsQuery.OrderByDescending(e => e.Deaths);
                    break;
                case Constants.OrderByFieldTypes.RISK_OF_IMPORTATION:
                    eventsQuery = eventsInfo.GroupBy(e => e.LocalSpread)
                        .OrderByDescending(g => g.Key) // 1 = local spread, 0 = non-local, local comes before non-local
                        .SelectMany(g =>
                        {
                            return g.Key
                                ? 
                                g.OrderByDescending(e => e.RepCases).ThenBy(e => e.EventTitle)
                                :
                                g.OrderByDescending(e => e.ImportationProbabilityMax);
                        }).AsQueryable();
                    break;
                default:
                    eventsQuery = eventsQuery.OrderByDescending(e => e.LastUpdatedDate);
                    break;
            }

            return eventsQuery.ToList();
        }
    }


    public static class CustomComparer
    {
        public class StartDate : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                var retVal = 0;

                if (x != y)
                {
                    if (x.ToLower() == "unknown")
                    {
                        retVal = -1;
                    }

                    if (y.ToLower() == "unknown")
                    {
                        retVal = 1;
                    }

                    if (retVal == 0)
                    {
                        if (Convert.ToDateTime(x) > Convert.ToDateTime(y))
                        {
                            retVal = 1;
                        }
                        else
                        {
                            retVal = -1;
                        }
                    }
                }
                else
                {
                    retVal = 0; //0 is for both values equal
                }

                return retVal;
            }
        }
    }
}