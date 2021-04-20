using System.Collections.Generic;
using YANMFA.Games.Alex.SpiderFighter.Models.Mobs;

namespace YANMFA.Games.Alex.SpiderFighter.Models
{
    public class Level
    {
        public Level()
        {
            Items = new List<Item>();
            Mobs = new List<Mob>();
        }

        public (int, int) Index { get; set; }

        public List<Item> Items { get; set; }
        public List<Mob> Mobs { get; set; }
    }
}