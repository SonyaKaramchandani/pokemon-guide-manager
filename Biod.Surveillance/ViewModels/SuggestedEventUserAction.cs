using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class SuggestedEventUserAction
    {
        public int userAction { get; set; }
        public List<string> reasonsForRejection { get; set; }
        public int? eventId { get; set; }
    }
}