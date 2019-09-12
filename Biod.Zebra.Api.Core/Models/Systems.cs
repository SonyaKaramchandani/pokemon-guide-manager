using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class Systems
    {
        public Systems()
        {
            Symptoms = new HashSet<Symptoms>();
        }

        public int SystemId { get; set; }
        public string System { get; set; }
        public string Notes { get; set; }

        public ICollection<Symptoms> Symptoms { get; set; }
    }
}
