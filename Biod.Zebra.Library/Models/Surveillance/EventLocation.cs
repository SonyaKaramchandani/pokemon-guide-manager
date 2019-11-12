using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Models.Surveillance
{
    public class EventLocation
    {
        public int GeonameId { get; set; }
        public DateTime EventDate { get; set; }
        public int SuspCases { get; set; }
        public int ConfCases { get; set; }
        public int RepCases { get; set; }
        public int Deaths { get; set; }
    }
}
