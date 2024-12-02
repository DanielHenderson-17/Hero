using System.Runtime.InteropServices;

namespace SuperHero.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public float Weight { get; set; }
        public int? HeroId { get; set; }
    }
}

