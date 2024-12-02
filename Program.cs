using SuperHero.Models;
using SuperHero.DTOs.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<SuperHeroDbContext>(builder.Configuration["SuperHeroDbConnectionString"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


////Get Endpoints

//Get all heroes
app.MapGet("/api/heroes", async (SuperHeroDbContext dbContext) =>
{
    var heroes = await dbContext.Heroes
        .Select(hero => new
        {
            Name = hero.Name,
            Class = hero.HeroClass.Name
        })
        .ToListAsync();

    return Results.Ok(heroes);
});

app.UseHttpsRedirection();


app.Run();
