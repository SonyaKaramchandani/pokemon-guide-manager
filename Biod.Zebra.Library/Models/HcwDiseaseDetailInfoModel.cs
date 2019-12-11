using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Zebra.Library.Models
{
    public class HcwDiseaseDetailInfoModel
    {
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string DiseaseIntroduction { get; set; }
        public string Agents { get; set; }
        public string AgentTypes { get; set; }
        public string TransmissionMode { get; set; }
        public string Incubation { get; set; }
        public string Vaccination { get; set; }
        public List<HcwSymptomScoreModel> SymptomScore { get; set; }
    }
}