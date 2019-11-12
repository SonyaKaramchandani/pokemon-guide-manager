
using System;

namespace Biod.Zebra.Library.Models.Disease
{
    public class Symptoms
    {
        public SymptomClass[] SymptomList { get; set; }
    }

    public class SymptomClass
    {
        public int symptomId { get; set; }
        public int systemId { get; set; }
        public string definition { get; set; }
        public string symptom { get; set; }
        public string definitionSource { get; set; }
        public string system { get; set; }
        public DateTime lastModified { get; set; }
    }
}