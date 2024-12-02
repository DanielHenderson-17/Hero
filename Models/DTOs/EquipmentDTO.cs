namespace SuperHero.DTOs.Models;
public class EquipmentDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int TypeId { get; set; }
    public float Weight { get; set; }
    public int? HeroId { get; set; }
}


