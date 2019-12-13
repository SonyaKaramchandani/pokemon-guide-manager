using System.Collections.Generic;
using Biod.Insights.Api.Models.Geoname;

namespace Biod.Insights.Api.Models.User
{
    public class GetUserLocationModel
    {
        public IEnumerable<GetGeonameModel> Geonames { get; set; }
    }
}