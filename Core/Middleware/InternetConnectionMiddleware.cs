using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Core.Middleware
{
    public class InternetConnectionMiddleware
    {
        private readonly RequestDelegate _next;
        private bool _redirected = false;

        public InternetConnectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var host = context.Request.Host.Host.ToLower();

            if (!_redirected && host != "localhost" && host != "127.0.0.1" && !IsInternetAvailable())
            {
                _redirected = true;
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                return;
            }

            await _next(context);
        }

        private bool IsInternetAvailable()
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = ping.Send("www.google.com");
                    return reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    return false;
                }
            }
        }
    }

    public static class InternetConnectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseInternetConnectionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<InternetConnectionMiddleware>();
        }
    }
}
