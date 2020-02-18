using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost("RefreshToken")]
        public ActionResult RefreshToken()
        {
            // TODO: Implement when migrating auth.
            // This is added to allow development mode to not require running zebra locally to use the application
            return Ok();
        }
    }
}