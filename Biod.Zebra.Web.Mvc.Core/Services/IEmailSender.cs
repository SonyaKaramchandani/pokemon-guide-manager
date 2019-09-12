using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biod.Zebra.Web.Mvc.Core.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
