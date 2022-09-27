global using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
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
    //options.ApiVersionReader = new HeaderApiVersionReader("x-API-Version");

});

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.CreateDb();
}

app.MapControllers();

app.Run();
