using System.Threading.Tasks;
using Biod.Insights.Notification.Engine.Models;

namespace Biod.Insights.Notification.Engine.Services.EmailRendering
{
    public interface IEmailRenderingApiService
    {
        Task<string> RenderEmail(EmailRenderingModel emailRenderingModel);
    }
}