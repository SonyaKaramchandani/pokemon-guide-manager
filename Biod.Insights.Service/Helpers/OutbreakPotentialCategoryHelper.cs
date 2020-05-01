using Biod.Insights.Service.Models.Disease;
using Biod.Products.Common.Constants;

namespace Biod.Insights.Service.Helpers
{
    public static class OutbreakPotentialCategoryHelper
    {
        public static OutbreakPotentialCategoryModel GetOutbreakPotentialCategory(int attributeId, bool hasMapThresholdRisk = false)
        {
            switch (attributeId)
            {
                case 1:
                case 3 when hasMapThresholdRisk:
                    return new OutbreakPotentialCategoryModel
                    {
                        Id = (int) OutbreakPotentialCategory.Sustained,
                        AttributeId = attributeId,
                        Name = "Sustained"
                    };
                case 2:
                    return new OutbreakPotentialCategoryModel
                    {
                        Id = (int) OutbreakPotentialCategory.Sporadic,
                        AttributeId = attributeId,
                        Name = "Sporadic"
                    };
                case 3:
                case 4:
                    return new OutbreakPotentialCategoryModel
                    {
                        Id = (int) OutbreakPotentialCategory.Unlikely,
                        AttributeId = attributeId,
                        Name = "Negligible or none"
                    };
                default:
                    return new OutbreakPotentialCategoryModel
                    {
                        Id = (int) OutbreakPotentialCategory.Unknown,
                        AttributeId = attributeId,
                        Name = "Unknown"
                    };
            }
        }

        public static bool IsMapNeeded(int attributeId)
        {
            // Only Attribute ID 3 needs map
            return attributeId == 3;
        }
    }
}