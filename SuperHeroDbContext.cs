using Microsoft.EntityFrameworkCore;
using SuperHero.Models;

public class SuperHeroDbContext : DbContext
{

    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<HeroClass> HeroClasses { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }


    public SuperHeroDbContext(DbContextOptions<SuperHeroDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed HeroClass (lookup table)
        modelBuilder.Entity<HeroClass>().HasData(new HeroClass[]
        {
        new HeroClass { Id = 1, Name = "Warrior" },
        new HeroClass { Id = 2, Name = "Mage" },
        new HeroClass { Id = 3, Name = "Archer" },
        new HeroClass { Id = 4, Name = "Rogue" }
        });

        // Seed EquipmentType (lookup table)
        modelBuilder.Entity<EquipmentType>().HasData(new EquipmentType[]
        {
        new EquipmentType { Id = 1, Name = "Weapon" },
        new EquipmentType { Id = 2, Name = "Armor" },
        new EquipmentType { Id = 3, Name = "Accessory" },
        new EquipmentType { Id = 4, Name = "Consumable" }
        });

        // Seed Heroes
        modelBuilder.Entity<Hero>().HasData(new Hero[]
        {
        new Hero { Id = 1, Name = "Aragorn", Description = "A brave warrior", HeroClassId = 1, Level = 10, QuestId = 1 },
        new Hero { Id = 2, Name = "Gandalf", Description = "A wise mage", HeroClassId = 2, Level = 15, QuestId = 2 },
        new Hero { Id = 3, Name = "Legolas", Description = "A skilled archer", HeroClassId = 3, Level = 12, QuestId = null }
        });

        // Seed Quests
        modelBuilder.Entity<Quest>().HasData(new Quest[]
        {
        new Quest { Id = 1, Name = "Defend the Castle", Description = "Protect the castle from invaders", IsCompleted = false },
        new Quest { Id = 2, Name = "Retrieve the Ancient Artifact", Description = "Find the lost artifact in the ruins", IsCompleted = false }
        });

        // Seed Equipment
        modelBuilder.Entity<Equipment>().HasData(new Equipment[]
        {
        new Equipment { Id = 1, Name = "Steel Sword", Description = "A sharp blade", TypeId = 1, Weight = 3.5f, HeroId = 1 },
        new Equipment { Id = 2, Name = "Wizard Staff", Description = "Channel your magic power", TypeId = 1, Weight = 2.0f, HeroId = 2 },
        new Equipment { Id = 3, Name = "Leather Armor", Description = "Light but protective", TypeId = 2, Weight = 5.0f, HeroId = 3 },
        new Equipment { Id = 4, Name = "Health Potion", Description = "Restores 50 HP", TypeId = 4, Weight = 0.5f, HeroId = null }
        });
    }

}