using Lafda;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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

