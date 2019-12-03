using System.Collections.Generic;

namespace Biod.Insights.Api.Models
{
    public class GetUserLocationModel
    {
        public IEnumerable<GetGeonameModel> Geonames { get; set; }
    }
}