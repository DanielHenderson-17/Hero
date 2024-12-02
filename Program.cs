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

//Get all heroes (name, class)
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


//Get a single hero's details (name, class, level, description, their current quest's name, equipment)
app.MapGet("/api/heroes/{id}", async (int id, SuperHeroDbContext dbContext) =>
{
    var heroDetails = await dbContext.Heroes
        .Where(h => h.Id == id)
        .Select(h => new
        {
            h.Name,
            h.Level,
            h.Description,
            Class = dbContext.HeroClasses
                .Where(c => c.Id == h.HeroClassId)
                .Select(c => c.Name)
                .FirstOrDefault(),
            Quest = dbContext.Quests
                .Where(q => q.Id == h.QuestId)
                .Select(q => q.Name)
                .FirstOrDefault() ?? "No active quest",
            Equipment = dbContext.Equipment
                .Where(e => e.HeroId == h.Id)
                .Select(e => new
                {
                    e.Name,
                    e.Description,
                    Type = dbContext.EquipmentTypes
                        .Where(et => et.Id == e.TypeId)
                        .Select(et => et.Name)
                        .FirstOrDefault(),
                    e.Weight
                })
                .ToList()
        })
        .FirstOrDefaultAsync();

    if (heroDetails == null)
    {
        return Results.NotFound(new { Message = $"Hero with ID {id} not found." });
    }

    return Results.Ok(heroDetails);
});


//Get all equipment (name, description, type)
app.MapGet("/api/equipment", async (SuperHeroDbContext dbContext) =>
{
    var equipment = await dbContext.Equipment
        .Select(e => new
        {
            e.Name,
            e.Description,
            Type = dbContext.EquipmentTypes
                .Where(et => et.Id == e.TypeId)
                .Select(et => et.Name)
                .FirstOrDefault()
        })
        .ToListAsync();

    return Results.Ok(equipment);
});

//Get all quests (name, isCompleted)
app.MapGet("/api/quests", async (SuperHeroDbContext dbContext) =>
{
    var quests = await dbContext.Quests
        .Select(q => new
        {
            q.Name,
            q.IsCompleted
        })
        .ToListAsync();

    return Results.Ok(quests);
});

//Get a quest's details (name, description, isCompleted, heroes)
app.MapGet("/api/quests/{id}", async (int id, SuperHeroDbContext dbContext) =>
{
    var questDetails = await dbContext.Quests
        .Where(q => q.Id == id)
        .Select(q => new
        {
            q.Name,
            q.Description,
            q.IsCompleted,
            Heroes = dbContext.Heroes
                .Where(h => h.QuestId == q.Id)
                .Select(h => h.Name)
                .ToList()
        })
        .FirstOrDefaultAsync();

    return Results.Ok(questDetails);
});


app.UseHttpsRedirection();


app.Run();
