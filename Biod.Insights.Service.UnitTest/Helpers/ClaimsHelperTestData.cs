using System.Collections.Generic;
using System.Security.Claims;
using Biod.Products.Common;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class ClaimsHelperTestData
    {
        public static readonly IEnvironmentVariables DEFAULT_ENVIRONMENT_VARIABLES = new TestEnvironmentVariables();
        public const string DefaultUserId = "SOME USER ID";

        public static IEnumerable<object[]> DefaultUserIds = new[]
        {
            // Null claims
            new object[]
            {
                null,
                DEFAULT_ENVIRONMENT_VARIABLES
            },

            // No expected claim types for user id
            new object[]
            {
                new[]
                {
                    new Claim(ClaimTypes.Email, "some email")
                },
                DEFAULT_ENVIRONMENT_VARIABLES
            },

            // Expected claim types is empty string
            new object[]
            {
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "")
                },
                DEFAULT_ENVIRONMENT_VARIABLES
            }
        };

        private class TestEnvironmentVariables : IEnvironmentVariables
        {
            public string GetEnvironmentVariable(string variableName)
            {
                return DefaultUserId;
            }
        }
    }
}