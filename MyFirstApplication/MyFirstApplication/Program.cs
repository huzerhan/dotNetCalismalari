var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

// duruma bağlı status code bu şekilde gönderilecek

app.Run(async (HttpContext context) =>
{
    //Request header'ını okumak için yöntem
    context.Response.Headers["Content-Type"] = "text/html";    
        if (context.Request.Headers.ContainsKey("User-Agent"))
        {
            string userAgent = context.Request.Headers["User-Agent"];   
            await context.Response.WriteAsync($"<p>{userAgent}</p>");
        }
    

});

app.Run();
