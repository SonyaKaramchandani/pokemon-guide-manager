using System;

namespace Biod.Insights.Common
{
    public class EnvironmentVariables : IEnvironmentVariables
    {
        public string GetEnvironmentVariable(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName);
        }
    }
}