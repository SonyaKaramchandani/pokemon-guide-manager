using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.EntityModels.Zebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Zebra.Library.Models
{
    public class EventLocationsOutbreakPotentialModel : usp_ZebraDashboardGetOutbreakPotentialCategories_Result
    {
        public int GeonameId  { get; set; }
        public string LocationType { get; set; }
        public string LocationDisplayName { get; set; }
        public int ProvinceGeonameId { get; set; }
        public int CountryGeonameId { get; set; }
        public string ShapeAsText { get; set; }
        public int AoiCount { get; set; }
    }
}