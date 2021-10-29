using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseService.Infrastructure.Middlewares
{
    public class OkMiddleware
    {
        public OkMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cod = context.Response.StatusCode;
            await context.Response.WriteAsync($"{cod} Ok");
        }
    }
}