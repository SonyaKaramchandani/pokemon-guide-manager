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

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraUserAoiGeonameIdsController : BaseApiController
    {
        public class UserAoiModel
        {
            public string GeonameIds { get; set; }
        }

        /// <summary>Get geonameIds for user AOI</summary>
        /// <param name="userId"></param>
        /// <returns>string</returns>

        public List<UserAoiModel> Get(string userId)
        {
            var sqlCommand = $"Select [AoiGeonameIds] as GeonameIds FROM [dbo].[AspNetUsers] WHERE Id = '{userId}'";
            var results = DbContext.Database.SqlQuery<UserAoiModel>(sqlCommand, new SqlParameter("@UserId", userId)).ToList();
            Logger.Info($"Successfully returned geonameIds for User {userId} AOI");
            return results;
        }
    }
}
