using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Calculates the aggregated risk of at least one of the events being a risk, with assumption
        /// that the events are independent.
        ///
        /// Pre-conditions: list must be non-empty and each risk must be between 0 and 1 inclusive.
        /// </summary>
        /// <returns>the aggregated risk</returns>
        public static decimal GetAggregatedRiskOfAnyEvent(IEnumerable<decimal> riskProbability)
        {
            // Get the probability of each event NOT importing, then multiply it all together to get the
            // risk of none of the events being imported. Subtract from 1 to get the probability of any
            // event being imported.
            return 1 - riskProbability.Select(r => 1 - r).Aggregate((a, b) => a * b);
        }
    }
}