using System.Collections.Generic;
using Biod.Insights.Service.Models.Geoname;

namespace Biod.Insights.Service.Models.User
{
    public class GetUserLocationModel
    {
        public IEnumerable<GetGeonameModel> Geonames { get; set; }
    }
}