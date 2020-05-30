using System;
using Accord;
using Accord.Statistics.Distributions.Univariate;
using Accord.Math.Integration;

namespace Biod.Zebra.Api.Api.Surveillance
{
    public static class RServiceLogic
    {
        public static int[] GetMinMaxCasesCount(int cases)
        {
            if (cases == 0)
            {
                return new[] {0, 0};
            }

            const double x = 0.005;

            var poissonDistribution = new PoissonDistribution(cases);
            var minCases = poissonDistribution.InverseDistributionFunction(x);
            var maxCases = poissonDistribution.InverseDistributionFunction(1 - x);

            return new[] {minCases, maxCases};
        }

        public static double[] GetMinMaxPrevalence(
            double minEventCasesOverPopSize,
            double maxEventCasesOverPopSize,
            decimal diseaseIncubation,
            decimal diseaseSymptomatic,
            DateTime startDate,
            DateTime endDate)
        {
            const int expDays = 30;
            const int expYear = 365;
            const double expTime = (double) expDays / expYear;

            var mDur = (double) diseaseIncubation + (double) diseaseSymptomatic;
            var diseaseDur = Math.Max(mDur, 1);

            var infRate = expYear / diseaseDur;
            var eventDur = (endDate - startDate).Days;
            var eventTimes = eventDur / expDays;
            var lowerVal = eventTimes * expDays / (double) expYear;
            var upperVal = (eventTimes + 1) * expDays / (double) expYear;

            var lambdaMinEventCasesOverPopSize = -Math.Log(1 - minEventCasesOverPopSize) / (eventDur / (double) expYear);
            var lambdaMaxEventCasesOverPopSize = -Math.Log(1 - maxEventCasesOverPopSize) / (eventDur / (double) expYear);

            var minIntegration = new RombergMethod(t =>
            {
                var dExp = new ExponentialDistribution(lambdaMinEventCasesOverPopSize).ProbabilityDensityFunction(t);
                var pExp = new ExponentialDistribution(infRate).DistributionFunction(expTime - t);
                return dExp * (1 - pExp);
            })
            {
                Range = new DoubleRange(lowerVal, upperVal),
            };
            minIntegration.Compute();

            var maxIntegration = new RombergMethod(t =>
            {
                var dExp = new ExponentialDistribution(lambdaMaxEventCasesOverPopSize).ProbabilityDensityFunction(t);
                var pExp = new ExponentialDistribution(infRate).DistributionFunction(expTime - t);
                return dExp * (1 - pExp);
            })
            {
                Range = new DoubleRange(lowerVal, upperVal)
            };
            maxIntegration.Compute();

            return new[] {minIntegration.Area, maxIntegration.Area};
        }
    }
}