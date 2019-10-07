using System;
using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Models;

namespace Biod.Zebra.Library.Infrastructures.Geoname
{
    public static class GeonameSearchHelper
    {
        public static IEnumerable<LocationKeyValueAndTypePairModel> SearchCityNames(BiodZebraEntities dbContext, string term)
        {
            return dbContext.usp_GetGeonameCities(term)
                .Where(item => item.DisplayName.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0)
                .Select(x => new LocationKeyValueAndTypePairModel
                {
                    key = x.GeonameId,
                    value = x.DisplayName,
                    type = "City"
                })
                .AsEnumerable();
        }
    }
}