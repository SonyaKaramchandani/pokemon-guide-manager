﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Infrastructures.Notification
{
    public interface IEmailClient
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
