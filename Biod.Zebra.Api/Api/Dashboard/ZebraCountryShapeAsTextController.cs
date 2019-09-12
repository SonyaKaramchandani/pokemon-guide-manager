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
using System.Web.Http;
using System.Data.Entity.Core.Objects;
using Biod.Zebra.Api.Api;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraCountryShapeAsTextController : BaseApiController
    {
        /// <summary>Get country shape as text.</summary>
        /// <param name="GeonameId"></param>
        /// <returns>string</returns>
        public string Get(int GeonameId = 0)
        {
            var results = DbContext.usp_ZebraPlaceGetCountryShapeByGeonameId(GeonameId).FirstOrDefault();

            Logger.Info($"Successfully returned country shape as text for geoname ID {GeonameId}");
            return results;
        }
    }
}