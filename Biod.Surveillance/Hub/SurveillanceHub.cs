using System;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace SignalRSurveillance
{
    public class ChatHub : Hub
    {
        public void Send(string userName, string htmlElementId, string articleId)
        {
            // Call the broadcastMessage method to update clients.
            if (!String.IsNullOrEmpty(userName))
                Clients.All.broadcastMessage(userName, htmlElementId, articleId);
        }
    }
}