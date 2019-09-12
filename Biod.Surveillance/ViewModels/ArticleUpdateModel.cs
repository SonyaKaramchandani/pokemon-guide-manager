using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class ArticleUpdateModel
    {

        public string ArticleID { get; set; }
        public string HamTypeId { get; set; }
        public string IsCompleted { get; set; }
        public string IsImportant { get; set; }
        public string IsRead { get; set; }
        public string ClusterID { get; set; }
        public string SelectedEventIds { get; set; }
        public string Notes { get; set; }
        public string DiseaseObject { get; set; }

    }
}