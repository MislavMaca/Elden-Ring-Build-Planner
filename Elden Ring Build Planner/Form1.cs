using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Elden_Ring_Build_Planner.Data;
using Elden_Ring_Build_Planner.Models;

namespace Elden_Ring_Build_Planner
{
    public partial class Form1 : Form
    {
        private Character character;
        private List<Item> items = new List<Item>();
        private static readonly SemaphoreSlim semaphore = new(1, 1); // Prevents race conditions

        public Form1()
        {
            InitializeComponent();
            character = new Character("Tarnished");
            this.KeyPreview = true;
            this.MainMenuStrip = menuStrip;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Form1_Load started", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Console.WriteLine("Form1_Load started");

            items = await DataHandler.LoadItemsAsync();
            Console.WriteLine($"Items loaded in Form1: {items.Count}");

            lstItems.DataSource = items;
            lstItems.DisplayMember = "Name"; // Display item names in the ListBox

            cmbItemTypeFilter.DataSource = Enum.GetValues(typeof(ItemType));
            cmbItemTypeFilter.SelectedIndexChanged += cmbItemTypeFilter_SelectedIndexChanged;

            if (lstItems.Items.Count > 0)
            {
                lstItems.SelectedIndex = 0;
            }

            character.ItemAdded += Character_ItemAdded;
            character.ItemRemoved += Character_ItemRemoved;

            await LoadSavedBuilds(); // Loads available builds into ComboBox asynchronously

            UpdateCharacterInfo();
        }

        /// <summary>
        /// Filters the ListBox based on selected Item Type
        /// </summary>
        private void cmbItemTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbItemTypeFilter.SelectedItem is ItemType selectedType)
            {
                lstItems.DataSource = items.Where(item => item.Type == selectedType).ToList();
            }
        }

        /// <summary>
        /// Adds the selected item to the character build
        /// </summary>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedItem is Item selectedItem)
            {
                character.AddItem(selectedItem);
                UpdateCharacterInfo();
            }
        }

        /// <summary>
        /// Removes the selected item from the character build
        /// </summary>
        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstItems.SelectedItem is Item selectedItem)
                {
                    character.RemoveItem(selectedItem);
                    UpdateCharacterInfo();
                }
            }
            catch (ItemNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the current character build with a specified name asynchronously
        /// </summary>
        private async void btnSaveBuild_Click(object sender, EventArgs e)
        {
            string buildName = txtBuildName.Text.Trim();

            if (string.IsNullOrWhiteSpace(buildName))
            {
                MessageBox.Show("Enter a name for your build before saving!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await BuildManager.SaveBuildAsync(character, buildName);
                    Invoke(new Action(() => MessageBox.Show($"Build '{buildName}' saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                    await LoadSavedBuilds();
                }
                finally
                {
                    semaphore.Release();
                }
            });
        }

        /// <summary>
        /// Loads the selected saved build asynchronously
        /// </summary>
        private async void btnLoadBuild_Click(object sender, EventArgs e)
        {
            if (cmbSavedBuilds.SelectedItem is string selectedBuild)
            {
                await Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        character = await BuildManager.LoadBuildAsync(selectedBuild);
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show($"Loaded Build: {character.Name}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateCharacterInfo();
                        }));
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });
            }
            else
            {
                MessageBox.Show("Select a build to load!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Populates the saved builds ComboBox asynchronously
        /// </summary>
        private async Task LoadSavedBuilds()
        {
            var builds = await Task.Run(async () => await BuildManager.GetSavedBuildsAsync());
            Invoke(new Action(() => cmbSavedBuilds.DataSource = builds));
        }

        /// <summary>
        /// Shows the selected item's location
        /// </summary>
        private void btnShowLocation_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedItem is Item selectedItem)
            {
                MessageBox.Show($"Location: {selectedItem.Location}", selectedItem.Name);
            }
        }

        /// <summary>
        /// Updates the UI with the character's equipped items
        /// </summary>
        private void UpdateCharacterInfo()
        {
            string equippedItemsText = string.Join(", ", character.GetEquippedItems().Select(i => i.Name));

            lblCharacterInfo.Text = $"Character: {character.Name}\n" +
                                    $"Equipped Items: {character.GetEquippedItems().Count}\n" +
                                    $"Items: {(equippedItemsText.Length > 0 ? equippedItemsText : "None")}";
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle item selection changes here
        }

        private void Character_ItemAdded(Item item)
        {
            MessageBox.Show($"Item Added: {item.Name}", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateCharacterInfo();
        }

        private void Character_ItemRemoved(Item item)
        {
            MessageBox.Show($"Item Removed: {item.Name}", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateCharacterInfo();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Closes the application when "Exit" is clicked.
        /// </summary>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Shows an "About" message box when "About" is clicked.
        /// </summary>
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Elden Ring Build Planner v1.0\nDeveloped by Mislav Maca.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
