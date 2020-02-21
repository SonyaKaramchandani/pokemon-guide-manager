﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Biod.Insights.Notification.Engine
{
    //sample reference: http://ahmed-develop.net/2017/11/SMTP-Email-Sender-Service-for-NET-Standard-2-0/
    public class AuthMessageSenderOptions
    {
        public string Domain { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Key { get; set; }
        public bool UseSsl { get; set; }
        public bool RequiresAuthentication { get; set; } = true;
        public string DefaultSenderEmail { get; set; }
        public string DefaultSenderDisplayName { get; set; }
        public bool UseHtml { get; set; }
    }
}
