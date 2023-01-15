using System.Runtime.CompilerServices;

namespace MiddlewareExample.Classes
{
    public class MyCustomMiddlewareClass : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("\n\nMy custom mw - starts");
            await next(context);
            await context.Response.WriteAsync("\n\nMy custom mw - ends");

        }
    }
    // middleware' tek ve basit bir şekilde çağırabilmek için kullanılan extension method 
    public  static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app) //middleware classında extension metotun başına use kullanmak sadece bir conventiondur. mecbur değiliz
        {
           return app.UseMiddleware<MyCustomMiddlewareClass>();
        }
            
    }
}
