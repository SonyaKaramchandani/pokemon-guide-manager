using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class CustomGroups
    {
        public CustomGroups()
        {
            XtblDiseaseCustomGroup = new HashSet<XtblDiseaseCustomGroup>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<XtblDiseaseCustomGroup> XtblDiseaseCustomGroup { get; set; }
    }
}
