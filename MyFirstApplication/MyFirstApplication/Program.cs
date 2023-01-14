using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

#region

/*
 HTTP Request Methods

GET         :request to retrieve info
POST        :sends an entity object to server, generaly it will be inserted to a db
PUT         :sends an entity object to sserver, generally updates all propertiesin the db
PATCH       :like post but updates some properties, partial update
DELETE      :requests to delete an entity in db

GET ve POST requestlerinde server'a bilgi gönderilir ancak sadece POST requestlerinde server'dan bilgi alınır.
 */

#endregion
app.Run(async (HttpContext context) =>
{
    //Request header'ını okumak için yöntem
    context.Response.Headers["Content-Type"] = "text/html";
    if (context.Request.Headers.ContainsKey("User-Agent"))
    {
        string userAgent = context.Request.Headers["User-Agent"];
        await context.Response.WriteAsync($"<p>{userAgent}</p>");
    }
    //aşağıdaki request postman'de de yapılmıştır.ancak browserda web sayfası olmadıkça istek atamazken postman ile serbestçe istek atıp sonucunu direkt olarak görebiliriz.
    if (context.Request.Headers.ContainsKey("Authorization"))
    {
        string auth = context.Request.Headers["Authorization"];
        await context.Response.WriteAsync($"<p>{auth}</p>");
    }

    StreamReader reader = new StreamReader(context.Request.Body);
    string body=await reader.ReadToEndAsync();

    // aşağıda Dictionary<string, StringValues> kısmında StringValues yerine sadeec string'de kullanabilirdik. Ancak query sting üzerinde "firstName=erhan&age=28&age=30" bir key değerine ait iki farklı value gelebilir. bu yüzden aşağıdaki formu ile kullanabiliriz.

    Dictionary<string, StringValues> queryDict= Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    //postman aracılığı ile bir üstteki yorumda bulunan queryStringi yolladığımızda firstName için bir değer, ve age için iki değeri görebiliriz. Bu da bize StringValues veri tipinin sağladığı bir faydadır.

    if (queryDict.ContainsKey("firstName")) 
    {
        string firstName = queryDict["firstName"][0];
        await context.Response.WriteAsync($"<p>{firstName}</p>");
    }
    // normalde bir istek sonucunda dönen data bu şekilde okunmayacaktır. gerçek uygulamalarda model binding denilen yöntem kullanılması gerekir.


    if (queryDict.ContainsKey("age"))
    {
        string[] age = queryDict["age"];
        foreach (string i  in age )
        {
        await context.Response.WriteAsync($"<p>{i}</p>");

        }
    }
    

});

app.Run();
