using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Biod.Surveillance.Api
{
    /// <summary>
    /// This is the Value Api
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Gets this instance.
        /// <![CDATA[GET api/<controller> e.g. /api/values]]>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets the specified identifier.
        /// <![CDATA[GET api/<controller>/5 e.g. /api/values/5]]>
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Posts the specified value.
        /// <![CDATA[POST api/<controller> e.g. /api/values]]>
        /// </summary>
        /// <param name="value">The value.</param>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Puts the specified identifier.
        /// <![CDATA[PUT api/<controller>/5 e.g. /values/5]]>
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// <![CDATA[DELETE api/<controller>/5 e.g. /values/5]]>
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}