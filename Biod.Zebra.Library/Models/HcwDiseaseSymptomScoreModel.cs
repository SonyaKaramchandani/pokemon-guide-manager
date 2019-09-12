using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Zebra.Library.Models
{
    public class HcwSymptomScoreModel
    {
        public int SymptomId { get; set; }
        public string Symptom { get; set; }
        public int AssociationScore { get; set; }

    }

    public class HcwDiseaseSymptomScoreModel
    {
        public int DiseaseId { get; set; }
        public List<HcwSymptomScoreModel> SymptomScore { get; set; }


    }
}