namespace Biod.Insights.Api.Models.Disease
{
    public class OutbreakPotentialCategoryModel
    {
        public int Id { get; set; }
        
        public int DiseaseId { get; set; }
        
        public string Name { get; set; }
    }
}