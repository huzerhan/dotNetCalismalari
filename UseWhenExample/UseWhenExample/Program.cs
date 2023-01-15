var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//UseWhen metotu iki parametre alır.
//ilk parametre, boolean değer döndürecek bir lambda ifadesidir
//ikinci parametre ise şart true döndüğünde işlenecek koddur
app.UseWhen(
    context => context.Request.Query.ContainsKey("username"),
//context=>true,
    app => {
        app.Use(async (context,next) =>
        {
            await context.Response.WriteAsync("\n\nhello from mw branch");
            await next();
        });
    });

app.Run(async context =>
{
    await context.Response.WriteAsync("\n\nhello from mw at main chain");
});
app.Run();
