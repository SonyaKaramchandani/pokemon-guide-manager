using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Biod.Zebra.Notification
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var mail = new EmailMessage();
            var emailClient = new EmailClient();
            mail.To.Add(message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            await emailClient.SendEmailAsync(mail);
        }
    }
}
