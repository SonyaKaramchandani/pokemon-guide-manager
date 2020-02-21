using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Insights.Notification.Engine
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        void SendEmail(string email, string subject, string message);
    }
}
