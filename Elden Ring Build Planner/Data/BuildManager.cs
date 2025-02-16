using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elden_Ring_Build_Planner.Models;

namespace Elden_Ring_Build_Planner.Data
{
    public static class BuildManager
    {
        private static readonly string BuildDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SavedBuilds");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new ItemJsonConverter() }
        };

        /// <summary>
        /// Asynchronously saves the character build with a specified name.
        /// </summary>
        public static async Task SaveBuildAsync(Character character, string buildName)
        {
            if (character == null || string.IsNullOrWhiteSpace(buildName))
            {
                Debug.WriteLine("Invalid build or build name.");
                return;
            }

            try
            {
                if (!Directory.Exists(BuildDirectory))
                    Directory.CreateDirectory(BuildDirectory);

                string buildFilePath = Path.Combine(BuildDirectory, $"{buildName}.json");

                var buildData = new BuildData
                {
                    Name = buildName,
                    EquippedItems = new List<Item>(character.CharacterInventory.GetItems())
                };

                string jsonString = JsonSerializer.Serialize(buildData, JsonOptions);
                await File.WriteAllTextAsync(buildFilePath, jsonString);

                Debug.WriteLine($"Build '{buildName}' saved successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving build: {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously loads a character build by name.
        /// </summary>
        public static async Task<Character> LoadBuildAsync(string buildName)
        {
            string buildFilePath = Path.Combine(BuildDirectory, $"{buildName}.json");

            if (!File.Exists(buildFilePath))
            {
                Debug.WriteLine($"Build '{buildName}' not found. Returning default character.");
                return new Character("Tarnished");
            }

            try
            {
                string jsonString = await File.ReadAllTextAsync(buildFilePath);
                var buildData = JsonSerializer.Deserialize<BuildData>(jsonString, JsonOptions);

                if (buildData == null)
                {
                    Debug.WriteLine("Error: Loaded build data is null. Returning default character.");
                    return new Character("Tarnished");
                }

                Character character = new Character(buildData.Name);
                foreach (var item in buildData.EquippedItems)
                {
                    character.AddItem(item);
                }

                Debug.WriteLine($"Build '{buildName}' loaded successfully.");
                return character;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading build: {ex.Message}");
                return new Character("Tarnished");
            }
        }

        /// <summary>
        /// Gets a list of all saved builds.
        /// </summary>
        public static List<string> GetSavedBuilds()
        {
            if (!Directory.Exists(BuildDirectory))
                return new List<string>();

            List<string> builds = new();
            foreach (var file in Directory.GetFiles(BuildDirectory, "*.json"))
            {
                builds.Add(Path.GetFileNameWithoutExtension(file));
            }

            return builds;
        }
    }

    /// <summary>
    /// Helper class to structure the saved character build data.
    /// </summary>
    public class BuildData
    {
        public string Name { get; set; } = "Tarnished";
        public List<Item> EquippedItems { get; set; } = new();
    }
}
