global using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TicketsAPI.Auth;
using TicketsAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<BugsContext>(options =>
    {
        options.UseInMemoryDatabase("Bugs");
    });
}

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader("x-API-Version");
});

builder.Services.AddVersionedApiExplorer(options => 
{
    options.GroupNameFormat = "'v'VVV";
});
builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1", Version = "v1" });
options.SwaggerDoc("v2", new OpenApiInfo { Title = "API v2", Version = "v2" });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5073/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
//builder.Services.AddSingleton<ICustomTokenManager, CustomTokenManager>();
builder.Services.AddSingleton<ICustomTokenManager, JwtTokenManager>();
builder.Services.AddSingleton<ICustomUserManager, CustomUserManager>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.CreateDb();

}

//Configure openAPI
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
});

app.MapControllers();

app.Run();

public partial class Program { }
