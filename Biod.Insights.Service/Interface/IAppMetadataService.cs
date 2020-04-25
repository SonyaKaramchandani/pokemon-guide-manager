using Biod.Insights.Service.Models;

namespace Biod.Insights.Service.Interface
{
    public interface IAppMetadataService
    {
        ApplicationMetadataModel GetMetadata();
    }
}