namespace Elden_Ring_Build_Planner.Models
{
    public class Armor : Item
    {
        public int Defense { get; set; }
        public int Poise { get; set; }
        public int Weight { get; set; }

        public Armor() : base("Unknown Armor", ItemType.Armor, "Unknown Location")
        {
            Defense = 0;
            Poise = 0;
            Weight = 0;
        }

        public Armor(string name, int defense, int poise, int weight, string location)
            : base(name, ItemType.Armor, location)
        {
            Defense = defense;
            Poise = poise;
            Weight = weight;
        }

        public override string GetDescription()
        {
            return $"{Name} - Defense: {Defense}, Poise: {Poise}, Weight: {Weight}, Found at: {Location}.";
        }
    }
}
