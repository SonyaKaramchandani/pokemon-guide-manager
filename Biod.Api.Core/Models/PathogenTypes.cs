using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class PathogenTypes
    {
        public PathogenTypes()
        {
            Pathogens = new HashSet<Pathogens>();
        }

        public int PathogenTypeId { get; set; }
        public string PathogenType { get; set; }

        public ICollection<Pathogens> Pathogens { get; set; }
    }
}
