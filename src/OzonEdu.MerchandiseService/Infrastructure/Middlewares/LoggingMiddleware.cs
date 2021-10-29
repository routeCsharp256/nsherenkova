using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseService.Infrastructure.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            await _next(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();
                
                    var buffer = new byte[context.Request.ContentLength.Value];
                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    
                    _logger.LogInformation("Request logged");
                    _logger.LogInformation(bodyAsText);
                    _logger.LogInformation($"Request URL: {Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");
                    var headerDictionary = context.Request.Headers;
                    foreach (var header in headerDictionary)
                    {
                        _logger.LogInformation($"{header.Key} {header.Value.ToString()}");
                    }
                    context.Request.Body.Position = 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request body");
            }
        }
        private async Task LogResponse(HttpContext context)
        {
            try
            {
                var headerDictionary = context.Response.Headers;
                _logger.LogInformation("Response logged");
                foreach (var header in headerDictionary)
                {
                   _logger.LogInformation($"{header.Key} {header.Value.ToString()}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log Response");
            }
        }
    }
}