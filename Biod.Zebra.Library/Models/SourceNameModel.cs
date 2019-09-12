using Biod.Zebra.Library.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Zebra.Library.Models
{
    public class SourceNameModel
    {
        public int SeqId { get; set; }
        public string DisplayName { get; set; }
        public List<string> FullNameList { get; set; }
    }
}