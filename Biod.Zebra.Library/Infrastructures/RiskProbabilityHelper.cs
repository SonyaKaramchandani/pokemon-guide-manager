namespace Biod.Zebra.Library.Infrastructures
{
    public static class RiskProbabilityHelper
    {
        public static string GetProbabilityName(decimal? maxProb)
        {
            switch (GetRiskLevel(maxProb))
            {
                case 0:
                    return "None";
                case 1:
                    return "Low";
                case 2:
                    return "Medium";
                case 3:
                    return "High";
                default:
                    return "NotAvailable";
            }
        } 

        public static int GetRiskLevel(decimal? maxProb)
        {
            if (maxProb != null && maxProb >= 0)
            {
                if (maxProb < 0.01m && maxProb >= 0)
                {
                    return 0;
                }
                if (maxProb < 0.2m)
                {
                    return 1;
                }
                if (maxProb >= 0.2m && maxProb <= 0.7m)
                {
                    return 2;
                }
                if (maxProb > 0.7m)
                {
                    return 3;
                }
            }
            return -1;
        }
    }
}