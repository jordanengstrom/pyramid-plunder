using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Item : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Reusable { get; set; }
        public Item(string name, string desc, bool reuseable)
        {
            Name = name;
            Description = desc;
            Reusable = reuseable;
        }
    }
}