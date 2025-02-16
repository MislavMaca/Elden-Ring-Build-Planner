namespace Elden_Ring_Build_Planner.Models
{
    public class Spell : Item
    {
        public int FP_Cost { get; set; }
        public string Scaling { get; set; }
        public string SpellType { get; set; }

        // Add a parameterless constructor for JSON deserialization
        public Spell() : base("Unknown Spell", ItemType.Spell, "Unknown Location")
        {
            FP_Cost = 0;
            Scaling = "Unknown";
            SpellType = "Unknown";
        }

        public Spell(string name, int fpCost, string scaling, string spellType, string location)
            : base(name, ItemType.Spell, location)
        {
            FP_Cost = fpCost;
            Scaling = scaling ?? "Unknown";
            SpellType = spellType ?? "Unknown";
        }

        public override string GetDescription()
        {
            return $"{Name} - Type: {SpellType}, FP Cost: {FP_Cost}, Scaling: {Scaling}, Found at: {Location}.";
        }
    }
}
