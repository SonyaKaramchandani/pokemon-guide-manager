using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System.Net.Mail;
using MailKit.Security;
using System.Collections.Generic;
using MimeKit;
using System.Linq;

namespace Biod.Zebra.Notification
{
    public class EmailClient : IEmailClient
    {
        public IAppSettingProvider AppSettingProvider { get; set; }
        public string SmtpHostName { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpFrom { get; set; }

        public EmailClient(IAppSettingProvider appSettingProvider = null)
        {
            this.AppSettingProvider = appSettingProvider ?? new AppSettingProvider();
            this.SmtpHostName = this.AppSettingProvider.Get("SmtpHostName");
            this.SmtpPort = int.Parse(this.AppSettingProvider.Get("SmtpPort"));
            this.SmtpUserName = this.AppSettingProvider.Get("SmtpUserName");
            this.SmtpPassword = this.AppSettingProvider.Get("SmtpPassword");
            this.SmtpFrom = this.AppSettingProvider.Get("SmtpFrom");
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            var mimeMessage = TransformMessage(message);
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(this.SmtpHostName, this.SmtpPort, SecureSocketOptions.Auto);
                client.Authenticate(this.SmtpUserName, this.SmtpPassword);

                await client.SendAsync(mimeMessage);
                client.Disconnect(true);
            }
        }

        public MimeMessage TransformMessage(EmailMessage message)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.To.AddRange(message.To.Select(to => new MailboxAddress(to)));
            mimeMessage.Subject = message.Subject;
            mimeMessage.From.Add(new MailboxAddress(this.SmtpFrom));

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            return mimeMessage;
        }
    }
}
