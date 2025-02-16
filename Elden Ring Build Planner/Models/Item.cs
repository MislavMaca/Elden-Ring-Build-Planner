namespace Elden_Ring_Build_Planner.Models
{
    public abstract class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Location { get; set; }

        protected Item() { } // ?? Needed for JSON deserialization

        protected Item(string name, ItemType type, string location)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Location = location ?? "Unknown";
        }

        public virtual string GetDescription()
        {
            return $"{Name} ({Type}) - Found at {Location}.";
        }
    }

    public enum ItemType
    {
        Weapon = 0,
        Armor = 1,
        Talisman = 2,
        Spell = 3,
        AshOfWar = 4
    }

}
