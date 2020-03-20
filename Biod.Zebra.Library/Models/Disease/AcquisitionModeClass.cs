

namespace Biod.Zebra.Library.Models.Disease
{
    public class AcquisitionModeClass
    {
        public int acquisitionModeId { get; set; }
        public string acquisitionModeLabel { get; set; }
        public string acquisitionModeDefinitionLabel { get; set; }
        public int modalityId { get; set; }
        public string modalityName { get; set; }
        public int diseaseVectorId { get; set; }
        public string diseaseVector { get; set; }
        public int? multiplier { get; set; }
    }

}
