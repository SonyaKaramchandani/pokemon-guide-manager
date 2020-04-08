using System.Collections.Generic;
using System.Security.Claims;
using Biod.Insights.Common;
using Biod.Insights.Service.Helpers;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class ClaimsHelperTest
    {
        #region GetUserId

        [Theory]
        [MemberData(nameof(ClaimsHelperTestData.DefaultUserIds), MemberType = typeof(ClaimsHelperTestData))]
        public void GetUserId_Default(IEnumerable<Claim> claims, IEnvironmentVariables environmentVariables)
        {
            var result = ClaimsHelper.GetUserId(claims, environmentVariables);
            Assert.Equal(ClaimsHelperTestData.DefaultUserId, result);
        }

        [Fact]
        public void GetUserId_Claims()
        {
            const string id = "sad0f8uqwfujf98s0afn";
            var result = ClaimsHelper.GetUserId(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id)
                },
                ClaimsHelperTestData.DEFAULT_ENVIRONMENT_VARIABLES);
            Assert.Equal(id, result);
        }

        #endregion
    }
}