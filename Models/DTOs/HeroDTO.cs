namespace SuperHero.DTOs.Models;

public class HeroDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int HeroClassId { get; set; }
    public int Level { get; set; }
    public int? QuestId { get; set; }
}
