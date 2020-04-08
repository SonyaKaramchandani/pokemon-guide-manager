namespace Biod.Insights.Common
{
    public interface IEnvironmentVariables
    {
        string GetEnvironmentVariable(string variableName);
    }
}