using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraLocalFeedLocationAutocompleteController : ApiController
    {
        /// <summary>Gets local feed locations.</summary>
        /// <returns>List of LocationKeyValuePairModel.</returns>
        public List<LocationKeyValueAndTypePairModel> Get(string term)
        {
            BiodZebraEntities _zebraContext = new BiodZebraEntities();
            var items = _zebraContext.usp_SearchGeonames(term).ToList();
            var filteredItems = items.Where(item => item.DisplayName.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0)
                .Select(x => new LocationKeyValueAndTypePairModel
                {
                    key = x.GeonameId,
                    value = x.DisplayName,
                    type = x.LocationType
                }).ToList();

            return filteredItems;
        }
    }
}