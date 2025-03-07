using Microsoft.EntityFrameworkCore;
using SuperHero.Models;

public class SuperHeroDbContext : DbContext
{

    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<HeroClass> HeroClasses { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<HeroQuest> HeroQuests { get; set; }
    public DbSet<QuestEquipment> QuestEquipments { get; set; }

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
        new Hero { Id = 1, Name = "Aragorn", Description = "A brave warrior", HeroClassId = 1, Level = 10},
        new Hero { Id = 2, Name = "Gandalf", Description = "A wise mage", HeroClassId = 2, Level = 15},
        new Hero { Id = 3, Name = "Legolas", Description = "A skilled archer", HeroClassId = 3, Level = 12}
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
        new Equipment { Id = 1, Name = "Steel Sword", Description = "A sharp blade", TypeId = 1, Weight = 3.5f, HeroId = 1, isAvailable = true },
        new Equipment { Id = 2, Name = "Wizard Staff", Description = "Channel your magic power", TypeId = 1, Weight = 2.0f, HeroId = 2, isAvailable = true },
        new Equipment { Id = 3, Name = "Leather Armor", Description = "Light but protective", TypeId = 2, Weight = 5.0f, HeroId = null, isAvailable = false },
        new Equipment { Id = 4, Name = "Health Potion", Description = "Restores 50 HP", TypeId = 4, Weight = 0.5f, HeroId = null, isAvailable = false }
        });

        //Configure many-to-many relationship between Hero and Quest
        modelBuilder.Entity<HeroQuest>().HasKey(hq => new { hq.HeroId, hq.QuestId });

        // Seed HeroQuest
        modelBuilder.Entity<HeroQuest>().HasData(new HeroQuest[]
        {
            new HeroQuest { HeroId = 1, QuestId = 1 },
            new HeroQuest { HeroId = 2, QuestId = 2 }
        });

        //Configure many-to-many relationship between Quest and Equipment
        modelBuilder.Entity<QuestEquipment>().HasKey(qe => new { qe.QuestId, qe.EquipmentId });

        // Seed QuestEquipment
        modelBuilder.Entity<QuestEquipment>().HasData(new QuestEquipment[]
        {
            new QuestEquipment { QuestId = 1, EquipmentId = 3 },
            new QuestEquipment { QuestId = 2, EquipmentId = 4 }
        });
    }

}