namespace SuperHero.Models.DTOs
{
    public class EquipmentCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public float Weight { get; set; }
    }
}