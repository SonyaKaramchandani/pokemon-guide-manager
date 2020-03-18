

namespace Biod.Zebra.Library.Models.Disease
{
    public class DiseaseAcquisitionModeClass
    {
        public int acquisitionModeId { get; set; }
        public int diseaseId { get; set; }
        public string disease { get; set; }
        public int speciesId { get; set; }
        public string speciesName { get; set; }
        public int acquisitionModeRank { get; set; }
        public string acquisitionModeLabel { get; set; }
        public int? multiplier { get; set; }
    }
}
