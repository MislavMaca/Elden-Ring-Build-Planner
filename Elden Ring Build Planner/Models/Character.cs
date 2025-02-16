using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Elden_Ring_Build_Planner.Models
{
    public delegate void ItemChangedHandler(Item item);

    public class Character
    {
        public string Name { get; private set; }  // Prevent direct modification
        public Inventory CharacterInventory { get; } = new Inventory();

        public event ItemChangedHandler? ItemAdded;
        public event ItemChangedHandler? ItemRemoved;

        public Character(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Character name cannot be empty or null.", nameof(name));

            Name = name;
        }

        public void AddItem(Item item)
        {
            CharacterInventory.AddItem(item);
            ItemAdded?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            CharacterInventory.RemoveItem(item);
            ItemRemoved?.Invoke(item);
        }

        public IReadOnlyList<Item> GetEquippedItems() => CharacterInventory.GetItems();

        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Character name cannot be empty or null.", nameof(newName));

            Name = newName;
        }

        // Inner class for Inventory Management
        public class Inventory
        {
            private readonly List<Item> _equippedItems = new();

            public IReadOnlyList<Item> GetItems() => _equippedItems.AsReadOnly();

            public void AddItem(Item item)
            {
                if (item == null) throw new ArgumentNullException(nameof(item));
                _equippedItems.Add(item);
            }

            public void RemoveItem(Item item)
            {
                if (item == null) throw new ArgumentNullException(nameof(item));
                if (!_equippedItems.Contains(item))
                    throw new ItemNotFoundException($"Item '{item.Name}' is not in the equipped list!");

                _equippedItems.Remove(item);
            }

            public void SetItems(IEnumerable<Item> items)  // NEW METHOD
            {
                if (items == null) throw new ArgumentNullException(nameof(items));
                _equippedItems.Clear();
                _equippedItems.AddRange(items);
            }
        }
    }
}
