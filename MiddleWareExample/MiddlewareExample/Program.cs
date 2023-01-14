var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
//Middleware'ler çalışmalarının istendiği sıraya göre aşağıda dizilir.

//// aşağıda basit bir middleware örneği bulunuyor.
//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("hello");

//});

////hemen aşağıda bulunna middleware üsttekinden sonra çalışmayacaktır. çünkü app.Run() request'i kendinden sonra gelen app.Run() metoduna iletmez.
//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("hello again");
//});
////ancak middleware'lerin kullanılma amacı dolayısıyla bu isteklerin bir sonraki middleware'e de iletilmesi gerekir.
////request-->mw_1(middleware)-->mw_2-->,,,-->mw_n-->response

// ************** aşağıdaki örnekte bu sorunun çözümü işlenecek ***********************

//middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("hello");
    await next(context);
    await context.Response.WriteAsync("\n\nmw1'e geri dondum"); // buradaki gibi yazılmış mw'lerde response aynı yolu geri izler ve işleme ilk başladığı yerden çıkar

});

////istendiği takdirde bu araya da bir middleware eklenebilir. ve bu mw yine Use() metodu ile çağrılmalıdır.
//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    await context.Response.WriteAsync("\n\nhey there boiiiiii");
//    await next(context);
//});

//middleware 2
//run metotuyla kullanılan mw her zaman response contex'inin çıkış noktası olacaktır.
//ancak run metotu kullanılmadığı takdirde bir problem olmaz çünkü Use metotları  kendi içlerinde response gönderebilir.
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("\n\nhello again");
});
//mw'lerin hepsi peşpeşe geldiğinde pipeline'ı oluşturacaktır. buna aynı zamanda mw chain de denir

//custom mw class





app.Run();
//the extension method called "Run" is used to execute a terminating/short circuiting mw that doesn't forward the request to the next mw
