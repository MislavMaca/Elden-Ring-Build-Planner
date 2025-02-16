using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Elden_Ring_Build_Planner.Models;

namespace Elden_Ring_Build_Planner.Data
{
    public static class DataFetcher
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "items.json");

        private static readonly JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true
        };

        public static async Task FetchAndSaveDataAsync()
        {
            var allItems = new List<Item>();

            allItems.AddRange(await FetchWeapons());
            allItems.AddRange(await FetchArmors());
            allItems.AddRange(await FetchTalismans());
            allItems.AddRange(await FetchSpells());
            allItems.AddRange(await FetchAshesOfWar());

            string jsonString = JsonSerializer.Serialize(allItems, jsonOptions);
            await File.WriteAllTextAsync(filePath, jsonString);

            Console.WriteLine("Data successfully saved to items.json");
        }

        private static async Task<List<Item>> FetchWeapons()
        {
            string url = "https://eldenring.fanapis.com/api/weapons";
            var items = new List<Item>();

            try
            {
                string response = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<ApiResponse<WeaponData>>(response);

                if (data?.Data != null)
                {
                    foreach (var weapon in data.Data)
                    {
                        items.Add(new Weapon(
                            name: weapon.Name,
                            location: weapon.Location ?? "Unknown",
                            weaponType: weapon.Category
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weapons: {ex.Message}");
            }

            return items;
        }

        private static async Task<List<Item>> FetchArmors()
        {
            string url = "https://eldenring.fanapis.com/api/armors";
            var items = new List<Item>();

            try
            {
                string response = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<ApiResponse<ArmorData>>(response);

                if (data?.Data != null)
                {
                    foreach (var armor in data.Data)
                    {
                        items.Add(new Armor(
                            name: armor.Name,
                            location: armor.Location ?? "Unknown"
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching armor: {ex.Message}");
            }

            return items;
        }

        private static async Task<List<Item>> FetchTalismans()
        {
            string url = "https://eldenring.fanapis.com/api/talismans";
            var items = new List<Item>();

            try
            {
                string response = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<ApiResponse<TalismanData>>(response);

                if (data?.Data != null)
                {
                    foreach (var talisman in data.Data)
                    {
                        items.Add(new Talisman(
                            name: talisman.Name,
                            location: talisman.Location ?? "Unknown",
                            effect: talisman.Effect
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching talismans: {ex.Message}");
            }

            return items;
        }

        private static async Task<List<Item>> FetchSpells()
        {
            string url = "https://eldenring.fanapis.com/api/spells";
            var items = new List<Item>();

            try
            {
                string response = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<ApiResponse<SpellData>>(response);

                if (data?.Data != null)
                {
                    foreach (var spell in data.Data)
                    {
                        items.Add(new Spell(
                            name: spell.Name,
                            location: spell.Location ?? "Unknown",
                            spellType: spell.Category
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching spells: {ex.Message}");
            }

            return items;
        }

        private static async Task<List<Item>> FetchAshesOfWar()
        {
            string url = "https://eldenring.fanapis.com/api/ashesofwar";
            var items = new List<Item>();

            try
            {
                string response = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<ApiResponse<AshOfWarData>>(response);

                if (data?.Data != null)
                {
                    foreach (var ash in data.Data)
                    {
                        items.Add(new AshOfWar(
                            name: ash.Name,
                            location: ash.Location ?? "Unknown",
                            effect: ash.Effect
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ashes of war: {ex.Message}");
            }

            return items;
        }
    }

    // API Response Models
    public class ApiResponse<T>
    {
        public List<T> Data { get; set; }
    }

    public class WeaponData { public string Name; public string Category; public string Location; }
    public class ArmorData { public string Name; public string Location; }
    public class TalismanData { public string Name; public string Effect; public string Location; }
    public class SpellData { public string Name; public string Category; public string Location; }
    public class AshOfWarData { public string Name; public string Effect; public string Location; }
}
