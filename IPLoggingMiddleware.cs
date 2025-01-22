using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middlewares.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IPLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public IPLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
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
