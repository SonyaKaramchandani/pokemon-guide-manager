using Biod.Insights.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Biod.Insights.Api.Builders
{
    public static class AuthenticationBuilder
    {
        /// <summary>
        /// Adds the authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAuth = new JwtAuth();//TODO: consider using Azure key vault or Secret manager to store app secrets see https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets 
            configuration.GetSection("JwtAuth").Bind(jwtAuth);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //TODO: use configurations for all these
                    options.RequireHttpsMetadata = jwtAuth.RequiresHttps;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,//TODO: configure these as well
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuth.SecurityKey))
                    };
                });

            return services;
        }
    }
}
