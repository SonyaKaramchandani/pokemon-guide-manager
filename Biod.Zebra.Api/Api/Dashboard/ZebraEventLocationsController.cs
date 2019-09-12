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
using System.Net.Http;
using System.Net;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraGetEventLocationsController : BaseApiController
    {
        /// <summary>Get location shape by event identifier.</summary>
        /// <param name="EventId"></param>
        /// <param name="LocationType"></param>
        /// <returns>string</returns>
        public HttpResponseMessage Get(int EventId, string LocationType)
        {
            try
            {
                var query = DbContext.usp_ZebraApiGetEventLocationShapesByEventid(EventId).AsQueryable();
                if (LocationType == "City")
                {
                    query = query.Where(x => x.LocationType == LocationType);
                }
                else
                {
                    query = query.Where(x => x.LocationType == "Province" || x.LocationType == "Country");
                }

                var result = query.ToList();

                Logger.Info($"Successfully returned the location shape for event ID {EventId} and location type {LocationType}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return the location shape for event ID {EventId} and location type {LocationType}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}