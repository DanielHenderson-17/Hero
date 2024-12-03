
namespace SuperHero.Models
{
    public class QuestEquipment
    {
        public int QuestId { get; set; }
        public Quest Quest { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
    }
}