

namespace SuperHero.Models
{
    public class HeroQuest
    {
        public int HeroId { get; set; }
        public Hero Hero { get; set; }
        public int QuestId { get; set; }
        public Quest Quest { get; set; }
    }
}