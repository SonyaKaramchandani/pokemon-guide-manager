using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Biod.Zebra.Api.Core.Models;
using Biod.Zebra.Api.Core.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

namespace Biod.Zebra.Api.Core.Controllers
{
    /// <summary>
    /// Zebra Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class ZebraEmailController : Controller
    {
        private readonly BiodApiContext _context;
        // GET api/values
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //object graph configuration
            var appConfig = AppSettingsReader.GetAppSettings();

            var client = new SmtpClient();
            var mail = new MailMessage
            {
                From = new MailAddress("noreply@bluedot.global", "BlueDot Inc. - Zebra")
            };
            mail.To.Add("basam@bluedot.global");
            mail.Subject = "Thank you for signing up!";
            mail.IsBodyHtml = true;

            mail.Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                            "< html xmlns = 'http://www.w3.org/1999/xhtml' >" +
                            " < head > " +
                            " < meta http - equiv = 'Content-Type' content = 'text/html; charset=utf-8' />" +
                            "          < !--[if !mso]>< !-->" +
                            "           < meta http - equiv = 'X-UA-Compatible' content = 'IE=edge' />" +
                            "                < !--< ![endif]-- > " +
                            "                < meta name = 'viewport' content = 'width=device-width, initial-scale=1.0' > " +
                            "                   < title ></ title > " +
                            "                   < link rel = 'stylesheet' type = 'text/css' href = './css/styles-email-responsive.css' /> " +
                            "                        < link rel = 'stylesheet' type = 'text/css' href = './css/fonts.css' /> " +
                            "                             < !--[if (gte mso 9)| (IE)]> " +
                            "                              < style type = 'text/css' > " +
                            "                                    table { border - collapse: collapse; " +
                            "            }" +
                            "   </ style > " +
                            "    < ![endif]-- > " +
                            "</ head > " +
                            "< body > " +
                              "< center class='wrapper'>" +
                                  "<div class='webkit'>" +
                                    "<!--[if (gte mso 9)|(IE)]>" +
                                    "<table width = '600' align='center'>" +
                                    "<tr>" +
                                    "<td>" +
                                    "<![endif]-->" +
                                    "<table class='outer' align='center'>" +
                                    "  <tr>" +
                                    "    <td class='zebra-logo'>Insights</td>" +
                                    "  </tr>" +
                                    "  <tr>" +
                                    "       <td class='zebra-slogan'><p>Near real-time infectious disease alerts</p><p>to keep you updated.</p></td>" +
                                    "  </tr>" +
                                    "  <tr>" +
                                    "    <td class='foreign-location'>" +
                                    "      <table width = '100%' > " +
                                    "        < tr > " +
                                    "          < td class='connectivity'>" +
                                    "          <!--<p class='connectivity-question'>Why am I recieving this alert?</p>-->" +
                                    "            <p class='connectivity-value'>You're recieving this alert because <em>Toronto is highly connected to London</em> via air travel.</p>" +
                                    "          </td>" +
                                    "        </tr>" +
                                    "      </table>" +
                                    "    </td>" +
                                    "  </tr>" +
                                    "  <tr>" +
                                    "    <td class='one-column'>" +
                                    "        <table width = '100%' > " +
                                    "          < tr > " +
                                    "            < td class='event-priority'>" +
                                    "            <span id = 'event-priority-label' >< img src='./assets/medium-priority.png' height='10' width='10' alt=''/>  Medium Priority</span>" +
                                    "            </td>" +
                                    "          </tr>" +
                                    "            <td class='header'>" +
                                    "               <p class='h1'>Cholera Outbreak in Toronto</p>" +
                                    "                  <tr>" +
                                    "                    <td class='event-metadata'>" +
                                    "                        <p class='event-metadata-label'>Disease</p>" +
                                    "                        <p class='event-metadata-value'>Cholera</p>" +
                                    "                     </td>" +
                                    "                   </tr>" +
                                    "                    <tr>" +
                                    "                     <td class='event-metadata'>" +
                                    "                        <p class='event-metadata-label'>Reason for event</p>" +
                                    "                        <p class='event-metadata-value'>Outbreak reported, Local spread likely</p>" +
                                    "                     </td>" +
                                    "                  </tr>" +
                                    "                  <tr>" +
                                    "                     <td class='event-metadata'>" +
                                    "                        <p class='event-metadata-label'>Mode of Transmission</p>" +
                                    "                        <p class='event-metadata-value'>Food and water</p>" +
                                    "                     </td>" +
                                    "                  </tr>" +
                                    "                  <tr>" +
                                    "                     <td class='event-metadata'>" +
                                    "                        <p class='event-metadata-label'>Brief</p>" +
                                    "                        <p class='event-metadata-value'>Toronto is experiencing its largest outbreak of cholera in history.Cases reported across all neighbourhoods.Potentially expanding across Lake Ontario: Hamilton and area reporting cases of Acute Watery Diarrhea.</p>" +
                                    "                    </td>" +
                                    "                  </tr>" +
                                    "                   <tr>" +
                                    "            <table border = '0' cellpadding='0' cellspacing='0' width='100%' style='background-color:#1C1C1C; border:1px solid #353535; border-radius:50px; margin-top: 15px; margin-bottom: 5px;'>" +
                                    "              <tr>" +
                                    "                <td align = 'center' valign='middle' style='color:#FFFFFF; font-family:'Inter UI', sans-serif; font-size:12px; font-weight:500; letter-spacing:1px; line-height:150%; padding-top:15px; padding-right:30px; padding-bottom:15px; padding-left:30px;'>" +
                                    "                  <a href = 'http://www.mailchimp.com/blog/' target='_blank' style='color:#FFFFFF; text-transform: uppercase; text-decoration:none;'>Open Event Page</a>" +
                                    "                </td>" +
                                    "              </tr>" +
                                    "            </table>" +
                                    "    </table>" +
                                    "</td> " +
                                    "</tr>" +
                                    "<td class='footer'>" +
                                    "  <table width = '18%' > " +
                                    "    < tr > " +
                                    "      < td class='bluedot-logo'>" +
                                    "        <img src = './assets/BlueDot_Horizontal_Black_Pantone_Tight.svg' > " +
                                    "      </ td > " +
                                    "     </ tr > " +
                                    "  </ table > " +
                                    "  < table width='75%'>" +
                                    "      <tr>" +
                                    "        <td class='bluedot-logo'>" +
                                    "          <p class='footer'>Insights is a product of BlueDot, a certified B corporation helping decision-makers prepare for and respond to infectious diseases.</p>" +
                                    "        </td>" +
                                    "      </tr>" +
                                    "      <tr>" +
                                    "        <td class='unsubscribe'><a href = '' > Unsubscribe </ a > " +
                                    "        </ td > " +
                                    "      </ tr > " +
                                    "  </ table > " +
                                    "</ td > " +
                                    "</ div > " +
                                    "< !--[if (gte mso 9)| (IE)]>" +
                                    "</td>" +
                                    "</tr>" +
                                    "</table>" +
                                    "<![endif]-->";

            try
            {
                client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("support", "Bd_supp2016$", "BLUEDOT");
                client.Host = appConfig.EmailSmtpClientHost;
                client.Port = appConfig.EmailSmtpClientPort; //465; //995;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.EnableSsl = true;
                client.Send(mail);
                return new string[] { "Email has been sent." };
            }
            catch (SmtpException e)
            {
                //Console.ReadLine();
                LogHelper.LogExceptionMessage(e);
                return new string[] { e.Message };
                //return false;
            }
            //Console.ReadLine();
        }

        // GET api/values/5
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public List<GridStation> Get(int id)
        {
            //List<object> result = _context.GridStation.FromSql("EXEC [zebra].[usp_GetZebraGrid] @GridId = N'AA-248'").ToList();
            //using (BiodApiContext db = new BiodApiContext())
            //{
            //    db.Database.OpenConnection();
            //    DbCommand cmd = db.Database.GetDbConnection().CreateCommand();
            //    cmd.CommandText = "zebra.usp_GetZebraGrid";
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    List<ZebraGrid> zebraGrids;
            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        zebraGrids = reader.Cast<ZebraGrid>().ToList();
            //    }
            //}
            return new List<GridStation>();
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
