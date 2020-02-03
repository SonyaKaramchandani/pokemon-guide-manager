using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblDiseaseCustomGroup
    {
        public int DiseaseId { get; set; }
        public int GroupId { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual CustomGroups Group { get; set; }
    }
}
