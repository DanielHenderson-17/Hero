namespace SuperHero.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public List<HeroQuest> HeroQuests { get; set; }
        public List<QuestEquipment> QuestEquipments { get; set; } = new List<QuestEquipment>();
    }
}