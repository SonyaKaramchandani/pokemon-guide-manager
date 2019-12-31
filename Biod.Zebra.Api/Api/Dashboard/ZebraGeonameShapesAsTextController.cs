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
using System.Data.SqlClient;
using System.Threading.Tasks;
using Constants = Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraGeonameShapesAsTextController : BaseApiController
    {
        public class ShapeAsTextModel
        {
            public int GeonameId { get; set; }
            public string Shape { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int LocationType { get; set; }
            public string LocationTypeName { get; set; }
            public string DisplayName { get; set; }
        }

        /// <summary>Get geoname shape as text, along with centriod longitude, latitude and locationtype.</summary>
        /// <param name="GeonameIds"></param>
        /// <returns>string</returns>
        public List<ShapeAsTextModel> Get(string GeonameIds = "")
        {
            var geonameIdsParam = GeonameIds.Split(new char[] { ',' }).Select(g => int.Parse(g)).Select(g => $"{g}").Aggregate((x, y) => $"{x},{y}");
            var sqlCommand =
                $@"Select ag.GeonameId, ag.DisplayName, ag.CountryName, ag.LocationType, 
                (Case 
                When ag.LocationType=6 Then {Constants.LocationTypeDescription.COUNTRY}
                When ag.LocationType=4 Then {Constants.LocationTypeDescription.PROVINCE}
                Else {Constants.LocationTypeDescription.CITY}
                END) 
                as LocationTypeName, 
                cps.SimplifiedShape.STAsText() as Shape, 
                ag.Longitude, ag.Latitude 
                From [place].[CountryProvinceShapes] cps right JOIN place.ActiveGeonames ag 
                ON ag.GeonameId = cps.GeonameId 
                WHERE ag.GeonameId IN ({geonameIdsParam})";
            var results = DbContext.Database.SqlQuery<ShapeAsTextModel>(sqlCommand, new SqlParameter("@GeonameIds", GeonameIds)).ToList();
 
            Logger.Info($"Successfully returned shapes as text for geoname IDs {GeonameIds}");
            return results;
        }
    }
}