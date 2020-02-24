using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Models;
using Biod.Insights.Common.Filters;

namespace Biod.Insights.Api.Middleware
{
    /// <summary>
    /// This middleware will handle all errors not handled by MVC exception filters
    /// </summary>
    public class GlobalExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionsMiddleware> _logger;

        public GlobalExceptionsMiddleware(RequestDelegate next, ILogger<GlobalExceptionsMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exception");
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the GlobalExceptionsMiddleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = @"application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponseModel
                {
                    Errors = new []{"Uh Oh.. something went wrong..."}
                }));
            }
        }
    }

    public static class GlobalExceptionsMiddlewareExtensions
    {
        /// <summary>
        /// Extension method used to add the middleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="builder">the builder</param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalExceptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionsMiddleware>();
        }
    }
}