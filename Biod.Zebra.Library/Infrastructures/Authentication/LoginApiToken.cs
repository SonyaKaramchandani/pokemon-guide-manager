using Biod.Zebra.Library.Infrastructures.Log;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Biod.Zebra.Library.Infrastructures.Authentication
{
    public class LoginApiToken
    {
        private static readonly ILogger _logger = Logger.GetLogger(typeof(LoginApiToken).GetType().ToString());
        private static readonly string secretKey = ConfigurationManager.AppSettings.Get("JwtSecretKey");
        private static readonly int expireMinutes = Convert.ToInt32(ConfigurationManager.AppSettings.Get("JwtTokenExpireMinutes"));

        public static string GenerateToken(IdentityUser user, string fcmDeviceId)
        {
            var symmetricKey = Convert.FromBase64String(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user?.UserName ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, user?.Id ?? ""),
                    new Claim(Constants.LoginHeader.FIREBASE_DEVICE_ID, fcmDeviceId)
                }),
                Expires = now.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static bool ValidateToken(string token, bool allowNoUser, out IEnumerable<Claim> claims)
        {
            claims = null;
            try
            {
                var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey))
                }, out var securityToken);

                claims = claimsPrincipal.Claims;

                var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!allowNoUser 
                    && (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userId)))
                {
                    // Required user is empty in the token
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Warning("Token validation failed", ex);
                return false;
            }
        }
    }
}
