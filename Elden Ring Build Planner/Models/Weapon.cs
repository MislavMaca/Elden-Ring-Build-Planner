namespace Elden_Ring_Build_Planner.Models
{
    public class Weapon : Item
    {
        public int AttackPower { get; set; }
        public string Scaling { get; set; }
        public string WeaponType { get; set; }

        public Weapon() : base("Unknown Weapon", ItemType.Weapon, "Unknown Location")
        {
            AttackPower = 0;
            Scaling = "Unknown";
            WeaponType = "Unknown";
        }

        public Weapon(string name, int attackPower, string scaling, string weaponType, string location)
            : base(name, ItemType.Weapon, location)
        {
            AttackPower = attackPower;
            Scaling = scaling ?? "Unknown";
            WeaponType = weaponType ?? "Unknown";
        }

        public override string GetDescription()
        {
            return $"{Name} - Type: {WeaponType}, Attack Power: {AttackPower}, Scaling: {Scaling}, Found at: {Location}.";
        }
    }
}
