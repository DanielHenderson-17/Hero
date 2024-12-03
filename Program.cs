using SuperHero.Models;
using SuperHero.DTOs.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            Quest = dbContext.HeroQuests
                .Where(hq => hq.HeroId == h.Id)
                .Select(hq => new
                {
                    hq.Quest.Id,
                    hq.Quest.Name,
                    hq.Quest.Description,
                    hq.Quest.IsCompleted
                })
                .ToList(),
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

// Get a quest's details (name, description, isCompleted, heroes)
app.MapGet("/api/quests/{id}", async (int id, SuperHeroDbContext dbContext) =>
{
    var questDetails = await dbContext.Quests
        .Where(q => q.Id == id)
        .Select(q => new
        {
            q.Name,
            q.Description,
            q.IsCompleted,
            Heroes = dbContext.HeroQuests
                .Select(hq => hq.Hero.Name)
                .Distinct()
                .ToList()
        })
        .FirstOrDefaultAsync();

    if (questDetails == null)
    {
        return Results.NotFound(new { Message = $"Quest {id} not found" });
    }

    return Results.Ok(questDetails);
});


////Post Endpoints

//Create a quest(name, description)
app.MapPost("/api/quests", async (QuestCreateDTO questCreateDTO, SuperHeroDbContext dbContext) =>
{
    var quest = new Quest
    {
        Name = questCreateDTO.Name,
        Description = questCreateDTO.Description
    };

    dbContext.Quests.Add(quest);
    await dbContext.SaveChangesAsync();

    return Results.Ok(quest);
});

//Assign a hero to a quest
app.MapPost("/api/assign-hero", async (HeroQuestDTO heroQuestDTO, SuperHeroDbContext dbContext) =>
{
    var hero = await dbContext.Heroes
        .Where(h => h.Id == heroQuestDTO.HeroId)
        .FirstOrDefaultAsync();

    if (hero == null)
    {
        return Results.NotFound(new { Message = $"Hero with ID {heroQuestDTO.HeroId} not found." });
    }

    var quest = await dbContext.Quests
        .Where(q => q.Id == heroQuestDTO.QuestId)
        .FirstOrDefaultAsync();
    if (quest == null)
    {
        return Results.NotFound(new { Message = $"Quest with ID {heroQuestDTO.QuestId} not found." });
    }

    //Check if the hero is already assigned to the quest
    var existingAssignment = await dbContext.Set<HeroQuest>().FirstOrDefaultAsync(hq => hq.HeroId == heroQuestDTO.HeroId && hq.QuestId == heroQuestDTO.QuestId);
    if (existingAssignment != null)
    {
        return Results.BadRequest(new { Message = $"Hero {hero.Name} is already assigned to quest {quest.Name}." });
    }

    dbContext.Add(new HeroQuest
    {
        HeroId = heroQuestDTO.HeroId,
        QuestId = heroQuestDTO.QuestId
    });

    await dbContext.SaveChangesAsync();

    return Results.Ok(new { Message = $"Hero {hero.Name} assigned to quest {quest.Name}." });
});


//Complete a quest
app.MapPost("/api/complete-quest", async (QuestCompleteDTO questCompleteDTO, SuperHeroDbContext dbContext) =>
{
    var quest = await dbContext.Quests
        .Where(q => q.Id == questCompleteDTO.QuestId)
        .FirstOrDefaultAsync();

    quest.IsCompleted = true;
    await dbContext.SaveChangesAsync();

    return Results.Ok(new { Message = $"Quest {quest.Name} completed." });
});


//Create a new Equipment item (name, description, type, weight)
app.MapPost("/api/equipment", async (EquipmentCreateDTO equipmentDTO, SuperHeroDbContext dbContext) =>
{
    var equipmentType = await dbContext.EquipmentTypes.FindAsync(equipmentDTO.TypeId);

    var equipment = new Equipment
    {
        Name = equipmentDTO.Name,
        Description = equipmentDTO.Description,
        TypeId = equipmentDTO.TypeId,
        Weight = equipmentDTO.Weight
    };

    dbContext.Equipment.Add(equipment);
    await dbContext.SaveChangesAsync();

    return Results.Ok(equipment);
});

//Assign equipment to a hero
app.MapPost("/api/assign-equipment", async (EquipmentAssignDTO equipmentAssignDTO, SuperHeroDbContext dbContext) =>
{

    //assigning the hero variable to the dbContext property Heroes
    var hero = await dbContext.Heroes
        .Where(h => h.Id == equipmentAssignDTO.HeroId)
        .FirstOrDefaultAsync();

    var equipment = await dbContext.Equipment
        .Where(e => e.Id == equipmentAssignDTO.EquipmentId)
        .FirstOrDefaultAsync();

    equipment.HeroId = equipmentAssignDTO.HeroId;
    await dbContext.SaveChangesAsync();

    return Results.Ok(new { Message = $"Equipment {equipment.Name} assigned to hero {hero.Name}." });
});


////Delete Endpoints

//Delete Equipment
app.MapDelete("/api/equipment/{id}", async (int id, SuperHeroDbContext dbContext) =>
{
    var equipment = await dbContext.Equipment.FindAsync(id);

    dbContext.Equipment.Remove(equipment);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new { Message = $"Equipment {equipment.Name} deleted." });
});


//Delete Hero
app.MapDelete("/api/heroes/{id}", async (int id, SuperHeroDbContext dbContext) =>
{

    var hero = await dbContext.Heroes.FindAsync(id);
    var equipment = await dbContext.Equipment
        .Where(e => e.HeroId == id)
        .ToListAsync();

    foreach (var e in equipment)
    {
        e.HeroId = null;
    }

    dbContext.Heroes.Remove(hero);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new { Message = $"Hero {hero.Name} deleted." });
});

//Delete Quest
app.MapDelete("/api/quests/{id}", async (int id, SuperHeroDbContext dbContext) =>
{
    var quest = await dbContext.Quests.FindAsync(id);

    var heroes = await dbContext.HeroQuests
        .Where(hq => hq.QuestId == id)
        .ToListAsync();

    dbContext.Quests.Remove(quest);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new { Message = $"Quest {quest.Name} deleted." });
});








app.UseHttpsRedirection();


app.Run();
