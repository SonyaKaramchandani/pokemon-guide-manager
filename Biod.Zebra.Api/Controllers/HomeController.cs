using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biod.Zebra.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "BlueDot data application is a tool to provide services to update the client database with the recent data that are provided by BlueDot data warehouse.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "BlueDot Data Application.";

            return View();
        }
    }
}