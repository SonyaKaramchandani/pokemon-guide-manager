using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Biod.Products.Common;

namespace Biod.Insights.Service.Helpers
{
    public static class ClaimsHelper
    {
        private const string ENV_USER_ID = "DEV_USER_ID";
        
        public static string GetUserId(IEnumerable<Claim> claims, IEnvironmentVariables environmentVariables)
        {
            return claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value?.DefaultIfWhiteSpace() ?? environmentVariables.GetEnvironmentVariable(ENV_USER_ID);
        }
    }
}