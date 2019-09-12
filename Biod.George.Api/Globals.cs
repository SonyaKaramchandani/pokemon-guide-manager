using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlueDot.DiseasesAPI.Models;


namespace BlueDot.DiseasesAPI
{
    /// <summary>
    /// Globals
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// The conditions idstart
        /// </summary>
        public const int CONDITIONS_IDSTART = 10000;
        /// <summary>
        /// The preventions idstart
        /// </summary>
        public const int PREVENTIONS_IDSTART = 20000;

        /// <summary>
        /// The reference datum
        /// </summary>
        public static readonly DateTime referenceDatum = new DateTime(2001, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// Gets the cache tag.
        /// </summary>
        /// <param name="dbcontext">The dbcontext.</param>
        /// <returns></returns>
        public static string GetCacheTag(DiseasesModel dbcontext)
        {
            // Don't cache this as DB version might change asynchronously while REST API is running...
            return dbcontext.VersionInfos.FirstOrDefault<VersionInfo>().modelVersion.ToString();
        }
    }
}
