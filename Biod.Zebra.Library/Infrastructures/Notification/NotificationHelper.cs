﻿using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Notification;
using Biod.Zebra.Library.Models.Notification.Email;
using Biod.Zebra.Library.Models.Notification.Push;
using Microsoft.AspNet.Identity;
using Task = System.Threading.Tasks.Task;

namespace Biod.Zebra.Library.Infrastructures.Notification
{
    public class NotificationHelper
    {
        private readonly EmailHelper _emailHelper;
        private readonly PushHelper _pushHelper;

        public NotificationHelper(BiodZebraEntities dbContext, UserManager<ApplicationUser> userManager = null)
        {
            _emailHelper = new EmailHelper(dbContext, userManager);
            _pushHelper = new PushHelper();
        }

        public async Task SendZebraNotifications(IEnumerable<NotificationViewModel> emailViewModels)
        {
            await Task.WhenAll(emailViewModels.Select(SendZebraNotification).ToArray());
        }

        public async Task SendZebraNotification(INotificationViewModel viewModel)
        {
            var dbEmailId = -1;
            if (viewModel is IEmailViewModel emailViewModel)
            {
                dbEmailId = await _emailHelper.SendZebraEmail(emailViewModel);
            }

            if (dbEmailId > 0 && viewModel is IPushViewModel pushViewModel)
            {
                pushViewModel.NotificationId = dbEmailId;  // set the ID to the matching email DB row to allow subsequent query for HTML body
                await _pushHelper.SendZebraPushNotifications(pushViewModel);
            }
        }
    }
}