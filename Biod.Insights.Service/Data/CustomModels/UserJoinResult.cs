using System.Collections.Generic;
using Biod.Insights.Data.EntityModels;

namespace Biod.Insights.Service.Data.CustomModels
{
    public class UserJoinResult
    {
        public AspNetUsers User { get; set; }
        
        public IEnumerable<AspNetRoles> Roles { get; set; }
    }
}