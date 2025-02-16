namespace Elden_Ring_Build_Planner.Models
{
    public class Talisman : Item
    {
        public string Effect { get; set; }

        public Talisman() : base("Unknown Talisman", ItemType.Talisman, "Unknown Location")
        {
            Effect = "No special effect";
        }

        public Talisman(string name, string effect, string location)
            : base(name, ItemType.Talisman, location)
        {
            Effect = effect ?? "No special effect";
        }

        public override string GetDescription()
        {
            return $"{Name} - Effect: {Effect}, Found at: {Location}.";
        }
    }
}
