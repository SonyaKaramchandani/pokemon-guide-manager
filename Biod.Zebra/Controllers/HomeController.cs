using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Library.Infrastructures;
using Newtonsoft.Json;

namespace Biod.Zebra.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult ConfirmationEmail()
        {
            ViewBag.Message = "Zebra email confirmation page.";
            return View();
        }

        [Authorize]
        public ActionResult ResetPasswordEmail()
        {
            ViewBag.Message = "Zebra reset password email page.";
            return View();
        }

        [AllowAnonymous]
        public ActionResult WelcomeEmail()
        {
            ViewBag.Message = "Zebra welcome email page.";
            return View();
        }

        [AllowAnonymous]
        public ActionResult PrivacyPolicy()
        {
            ViewBag.Message = "Zebra privacy policy page.";

            Logger.Info("User navigated to Privacy Policy page");
            return View();
        }

        [AllowAnonymous]
        public ActionResult TermsOfService()
        {
            ViewBag.Message = "Zebra terms of service page.";

            Logger.Info("User navigated to Terms of Service page");
            return View();
        }

        [AllowAnonymous]
        public ActionResult LocationAutocomplete(string term)
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
             ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
             "/api/ZebraCitiesLocationAutocomplete?term=" + term,
             ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
             ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;
            List<LocationKeyValueAndTypePairModel> filteredItems = JsonConvert.DeserializeObject<List<LocationKeyValueAndTypePairModel>>(result);
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetAoiLocationAutocomplete(string aoiGeonameIds)
        {
            string result = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraAoiLocationAutocomplete?aoiGeonameIds=" + aoiGeonameIds,
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;
            List<LocationKeyValueAndTypePairModel> existingAois = JsonConvert.DeserializeObject<List<LocationKeyValueAndTypePairModel>>(result);


            return Json(existingAois, JsonRequestBehavior.AllowGet);
        }
    }
}