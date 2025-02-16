namespace Elden_Ring_Build_Planner.Models
{
    public class AshOfWar : Item
    {
        public string Affinity { get; set; }
        public string WeaponType { get; set; }

        public AshOfWar() : base("Unknown Ash of War", ItemType.AshOfWar, "Unknown Location")
        {
            Affinity = "None";
            WeaponType = "Various";
        }

        public AshOfWar(string name, string affinity, string weaponType, string location)
            : base(name, ItemType.AshOfWar, location)
        {
            Affinity = affinity ?? "None";
            WeaponType = weaponType ?? "Various";
        }

        public override string GetDescription()
        {
            return $"{Name} - Affinity: {Affinity}, Usable on: {WeaponType}, Found at: {Location}.";
        }
    }
}
