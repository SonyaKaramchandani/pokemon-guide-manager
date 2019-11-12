using System;
using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Infrastructures.Geoname
{
    public static class GeonameInsertHelper
    {
        public static void InsertActiveGeonames(BiodZebraEntities dbContext, IEnumerable<int> geonameIds)
        {
            if (geonameIds != null && geonameIds.Any())
                dbContext.usp_InsertActiveGeonamesByGeonameIds(String.Join(",", geonameIds));
        }

        public static void InsertEventActiveGeonames(BiodZebraEntities dbContext, Event currentEvent)
        {
            if (currentEvent != null)
            {
                InsertActiveGeonames(dbContext, currentEvent.Xtbl_Event_Location.Select(x => x.GeonameId));
                foreach (var articles in currentEvent.ProcessedArticles)
                {
                    InsertActiveGeonames(dbContext, articles.Xtbl_Article_Location_Disease.Select(x => x.LocationGeoNameId));
                }
            }
        }
    }
}