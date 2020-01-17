using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Infrastructures.Authentication
{
    public class JwtRefreshToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; } = "Bearer";//TODO: use constant
    }
    /// <summary>
    /// JWT and Refresh Token Service
    /// </summary>
    public class JwtRefreshTokenProvider
    {
        private static readonly int expireMinutes = Convert.ToInt32(ConfigurationManager.AppSettings.Get("JwtTokenExpireMinutes"));
        private readonly ITokenFactory _tokenFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public JwtRefreshTokenProvider(UserManager<ApplicationUser> userManager, ITokenFactory tokenFactory)
        {
            _userManager = userManager;
            _tokenFactory = tokenFactory;
        }
        /// <summary>
        /// Generate JWT and Refresh token for the identity user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>the token</returns>
        public async Task<JwtRefreshToken> GenerateUserTokensAsync(string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            var token = new JwtRefreshToken()
            {
                access_token = LoginApiToken.GenerateZebraJwt(currentUser),
                refresh_token = _tokenFactory.GenerateToken(),
                expires_in = expireMinutes * 60
            };

            currentUser.RefreshToken = token.refresh_token;
            currentUser.RefreshTokenCreatedDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(currentUser);

            return token;
        }
        /// <summary>
        /// Revoke the identity user JWT and refresh tokens
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>Success or failure</returns>
        public async Task<IdentityResult> RevokeUserTokensAsync(string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);

            currentUser.RefreshToken = null;
            currentUser.RefreshTokenCreatedDate = null;
            return await _userManager.UpdateAsync(currentUser);
        }
        /// <summary>
        /// Refresh the JWT using the Refresh Token
        /// </summary>
        /// <param name="refreshToken">the Refresh Token</param>
        /// <returns>The new token or UnauthorizedAccessException</returns>
        public async Task<JwtRefreshToken> RefreshJwtTokenAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new UnauthorizedAccessException("Missing refresh token!");
            }
            var user = await Task.FromResult(_userManager
                             .Users
                             .FirstOrDefault(u => u.RefreshToken.Equals(refreshToken))
                             );
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token!");
            }
            return new JwtRefreshToken()
            {
                access_token = LoginApiToken.GenerateZebraJwt(user),
                expires_in = expireMinutes * 60
            };//TODO: fill other properties if needed
        }
    }
    /// <summary>
    /// A factory class to generate secured refresh tokens
    /// </summary>
    public sealed class TokenFactory : ITokenFactory
    {
        /// <summary>
        /// Generate random secured token
        /// </summary>
        /// <param name="size">The byte array size</param>
        /// <returns></returns>
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }

    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}
