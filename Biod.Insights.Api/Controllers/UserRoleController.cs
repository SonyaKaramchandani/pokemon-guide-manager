using System.Threading.Tasks;
using Biod.Insights.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(ILogger<UserRoleController> logger, IUserRoleService userRoleService)
        {
            _logger = logger;
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            var result = await _userRoleService.GetPublicUserRoles();
            return Ok(result);
        } 
    }
}