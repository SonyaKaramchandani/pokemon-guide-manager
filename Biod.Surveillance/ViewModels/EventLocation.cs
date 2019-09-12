using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
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