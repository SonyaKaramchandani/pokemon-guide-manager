namespace Biod.Insights.Service.Models.Disease
{
    public class AcquisitionModeModel
    {
        public int Id { get; set; }

        public int RankId { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public int VectorId { get; set; }

        public string VectorName { get; set; }

        public int ModalityId { get; set; }

        public string ModalityName { get; set; }
    }
}