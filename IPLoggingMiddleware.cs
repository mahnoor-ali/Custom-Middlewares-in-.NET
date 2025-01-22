using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middlewares.Middlewares
{
    public class IPLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public IPLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var requestPath = context.Request.Path;
            logInformation(ipAddress, requestPath);

            return _next(context);
        }

        // Simulating logger for simplicity
        public void logInformation(string ipAddress, string requestPath)
        {
            Console.WriteLine($"Request from IP: {ipAddress}, Path: {requestPath}, Time: {DateTime.UtcNow}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    // Like other middlewares in Program.cs use app.UseSomeXyz() those are also passing IApplicationBuilder object which is 'app'
    public static class IPLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseIPLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IPLoggingMiddleware>();
        }
    }
}
