using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Surveillance.SyncConsole.Client
{
    class Mailer
    {
        //*******************************************************
        /// <summary>
        /// Send email to sender.
        /// </summary>
        /// <param name="mailRecipientList">The mail recipient list.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static bool SendMail(string[] mailRecipientList, string subject, string message)
        {
            try
            {
                var mail = new MailMessage();
                var currier = new SmtpClient();

                foreach (string recipient in mailRecipientList)
                {
                    mail.To.Add(recipient);
                }
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                currier.Send(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
