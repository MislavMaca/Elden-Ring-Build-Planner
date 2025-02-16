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
    public static class DataHandler
    {
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "items.json");


        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new ItemJsonConverter() } // Enables polymorphic serialization
        };

        /// <summary>
        /// Saves all item data into a JSON file asynchronously.
        /// </summary>
        public static async Task SaveItemsAsync(List<Item> items)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(items, JsonOptions);
                await File.WriteAllTextAsync(FilePath, jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving items: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads item data from a JSON file asynchronously.
        /// </summary>
        public static async Task<List<Item>> LoadItemsAsync()
        {
            MessageBox.Show($"Checking items.json at: {FilePath}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Console.WriteLine($"Checking items.json at: {FilePath}");

            if (!File.Exists(FilePath))
            {
                MessageBox.Show("Warning: items.json NOT FOUND!", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await File.WriteAllTextAsync(FilePath, "[]"); // Creates an empty JSON file
                return new List<Item>();
            }


            try
            {
                string jsonString = await File.ReadAllTextAsync(FilePath);

                MessageBox.Show($"Raw JSON Loaded:\n{jsonString}", "Debug JSON", MessageBoxButtons.OK, MessageBoxIcon.Information);

                List<Item> items = JsonSerializer.Deserialize<List<Item>>(jsonString, JsonOptions) ?? new List<Item>();

                MessageBox.Show($"Successfully parsed {items.Count} items!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show($"Loaded {items.Count} items from items.json.", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return items;
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON parse error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Item>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Item>();
            }
        }





    }

    /// <summary>
    /// Custom JSON converter to handle polymorphic serialization of Item subclasses.
    /// </summary>
    public class ItemJsonConverter : JsonConverter<Item>
    {
        public override Item? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                if (!root.TryGetProperty("Type", out var typeProp))
                    throw new JsonException("Missing Type property in JSON.");

                if (!typeProp.TryGetInt32(out int typeValue) || !Enum.IsDefined(typeof(ItemType), typeValue))
                    throw new JsonException($"Invalid item type value: {typeValue}");

                ItemType itemType = (ItemType)typeValue;

                return itemType switch
                {
                    ItemType.Weapon => JsonSerializer.Deserialize<Weapon>(root.GetRawText(), options),
                    ItemType.Armor => JsonSerializer.Deserialize<Armor>(root.GetRawText(), options),
                    ItemType.Talisman => JsonSerializer.Deserialize<Talisman>(root.GetRawText(), options),
                    ItemType.Spell => JsonSerializer.Deserialize<Spell>(root.GetRawText(), options),
                    ItemType.AshOfWar => JsonSerializer.Deserialize<AshOfWar>(root.GetRawText(), options),
                    _ => throw new JsonException($"Unknown item type: {itemType}")
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            IncludeFields = true, //  Allows fields to be deserialized
            Converters = { new ItemJsonConverter() } //  Enables polymorphic serialization
        };

    }


}
