using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Biod.Insights.Api.Helpers
{
    public static class ClaimsHelper
    {
        private const string ENV_USER_ID = "DEV_USER_ID";
        
        public static string GetUserId(IEnumerable<Claim> claims)
        {
            return claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Environment.GetEnvironmentVariable(ENV_USER_ID);
        }
    }
}