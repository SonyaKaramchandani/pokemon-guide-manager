using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Zebra.Api.Controllers
{
    /// <summary>
    /// Identity Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("[controller]")]
    [Authorize]
    public class IdentityController : Controller
    {
        /// <summary>
        /// Get method which will be used to retirive the user’s claim.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}