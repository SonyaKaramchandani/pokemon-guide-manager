using BdDataApi.EntityModels;
using BdDataApi.Infrastructures;
using BdDataApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Westwind.Web.Mvc;

namespace Biod.Zebra.Api.V2
{
    /// <summary>
    /// This API is to email Zebra users about a threat of an event that is highly connected to their location.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEmailUsersLocatedInEventDestinationAreaV2Controller : ApiController
    {
        // GET api/values
        /// <summary>
        /// Find the users that are in the event area with the event information and send them email with the event details and a link to the event page
        /// </summary>
        /// <param name="EventId">The event identifier.</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Get(int EventId)
        {
            try
            {
                ZebraEntities zebraDbContext = new ZebraEntities();
                zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

                //get all articles related to the event
                List<usp_GetZebraEventArticlesByEventId_Result> eventArticles = zebraDbContext.usp_GetZebraEventArticlesByEventId(EventId).ToList();
                //get all users emails with their event information
                List<usp_GetZebraEventInfoByEventId_Result> zebraEventsInfos = zebraDbContext.usp_GetZebraEventInfoByEventId(EventId, Convert.ToDecimal(ConfigurationManager.AppSettings.Get("EventDistanceBuffer")), ConfigurationManager.AppSettings.Get("ZebraVersion")).ToList();
                //find the users that are in the event area with the event information and send them email with the event details and a link to the event
                zebraEventsInfos = (from r in zebraEventsInfos where r.UserLocationName != null && r.UserLocationName != "-" select r).ToList();
                foreach (var zebraEventInfo in zebraEventsInfos)
                {
                    //3-send email to the user if it's GeonameId (long, lat) matches the Event Hexagon (GridId)
                    if (zebraEventInfo != null)
                    {
                        EventInfoModel eventEmailInfoModel = new EventInfoModel()
                        {
                            EventId = zebraEventInfo.EventId.Value,
                            DiseaseName = zebraEventInfo.DiseaseName,
                            LocationName = zebraEventInfo.EventLocationName,
                            EventTitle = zebraEventInfo.EventTitle,
                            Brief = zebraEventInfo.Brief,
                            PriorityTitle = zebraEventInfo.PriorityTitle,
                            Reasons = zebraEventInfo.Reasons,
                            TransmittedBy = zebraEventInfo.TransmittedBy,
                            Email = zebraEventInfo.Email,
                            EventArticles = eventArticles
                        };

                        var subject = eventEmailInfoModel.EventTitle; //eventEmailInfoModel.DiseaseName + " Outbreak in " + eventEmailInfoModel.LocationName;
                        var htmlBody = ViewRenderer.RenderView("~/Views/Home/EventEmail.cshtml", eventEmailInfoModel);
                        htmlBody = htmlBody.Remove(0, htmlBody.IndexOf("<content>")).Replace("<content>", "");
                        htmlBody = htmlBody.Remove(htmlBody.IndexOf("</content>"), htmlBody.Length - htmlBody.IndexOf("</content>")).Replace("</content>", "");

                        //userManager.SendEmailAsync(userId, subject, htmlBody);


                        var client = new SmtpClient();
                        var mail = new MailMessage
                        {
                            From = new MailAddress(ConfigurationManager.AppSettings.Get("NoreplyEmail"), ConfigurationManager.AppSettings.Get("NoreplyEmailAlias"))
                        };
                        mail.To.Add(zebraEventInfo.Email);
                        mail.Subject = subject;
                        mail.IsBodyHtml = true;
                        mail.Body = htmlBody;
                        client.SendMailAsync(mail);
                    }
                }

                return CustomResponseHandler.GetHttpResponse(true, "Success");
            }

            catch (SmtpException e)
            {
                return CustomResponseHandler.GetHttpResponse(false, e.Message + Environment.NewLine + e.InnerException);
            }
            catch (Exception e)
            {
                return CustomResponseHandler.GetHttpResponse(false, e.Message + Environment.NewLine + e.InnerException);
            }
        }
    }
}