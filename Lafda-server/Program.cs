using System.Text.Json.Serialization;
using Lafda;

var builder = WebApplication.CreateBuilder(args);

// for enum -> string in json response with addition of controller
// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.Converters.Add(
//             new JsonStringEnumConverter()
//         );
//     });
// ;
builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// di injection
builder.Services.ConfigureExtensions(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();


// auths middlewares
app.UseAuthentication();   // validates JWT
app.UseAuthorization();    // enforces [Authorize]

// map controllers for route
app.MapControllers();

app.Run();

