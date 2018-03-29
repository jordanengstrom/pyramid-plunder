using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public Dictionary<string, Room> Directions { get; set; }

        public void UseItem(Item item)
        {
            //TBD
        }

        public Room(string name, string desc)
        {
            Name = name;
            Description = desc;
            Directions = new Dictionary<string, Room>();
            Items = new List<Item>();
        }


        
    }
}