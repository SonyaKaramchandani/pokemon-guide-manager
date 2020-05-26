using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Controllers
{
    [ApiController]
    [Route("api/usertype")]
    public class UserTypeController : ControllerBase
    {
        private readonly ILogger<UserTypeController> _logger;
        private readonly IUserTypeService _userTypeService;

        public UserTypeController(ILogger<UserTypeController> logger, IUserTypeService userTypeService)
        {
            _logger = logger;
            _userTypeService = userTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTypeModel>>> GetUserTypes()
        {
            var result = await _userTypeService.GetUserTypes();
            return Ok(result);
        }
    }
}