global using Core.Models;
using DataStore.EF;
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

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.CreateDb();
}

app.MapControllers();

app.Run();
