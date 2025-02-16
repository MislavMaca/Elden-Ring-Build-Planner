namespace Elden_Ring_Build_Planner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lstItems = new ListBox();
            cmbItemTypeFilter = new ComboBox();
            btnAddItem = new Button();
            btnRemoveItem = new Button();
            btnSaveBuild = new Button();
            btnLoadBuild = new Button();
            btnShowLocation = new Button();
            lblCharacterInfo = new Label();
            txtBuildName = new TextBox();
            cmbSavedBuilds = new ComboBox();
            menuStrip = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            buildMenu = new ToolStripMenuItem();
            helpMenu = new ToolStripMenuItem();

            menuStrip.SuspendLayout();
            SuspendLayout();

            // 
            // lstItems
            // 
            lstItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstItems.FormattingEnabled = true;
            lstItems.ItemHeight = 15;
            lstItems.Location = new Point(8, 56);
            lstItems.Name = "lstItems";
            lstItems.Size = new Size(352, 334);
            lstItems.TabIndex = 0;
            lstItems.SelectedIndexChanged += lstItems_SelectedIndexChanged;
            // 
            // cmbItemTypeFilter
            // 
            cmbItemTypeFilter.FormattingEnabled = true;
            cmbItemTypeFilter.Location = new Point(8, 27);
            cmbItemTypeFilter.Name = "cmbItemTypeFilter";
            cmbItemTypeFilter.Size = new Size(564, 23);
            cmbItemTypeFilter.TabIndex = 1;
            cmbItemTypeFilter.SelectedIndexChanged += cmbItemTypeFilter_SelectedIndexChanged;
            // 
            // btnAddItem
            // 
            btnAddItem.Font = new Font("Segoe UI", 10F);
            btnAddItem.Location = new Point(419, 56);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(120, 30);
            btnAddItem.TabIndex = 2;
            btnAddItem.Text = "Add Item";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click += btnAddItem_Click;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.Font = new Font("Segoe UI", 10F);
            btnRemoveItem.Location = new Point(419, 92);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(120, 30);
            btnRemoveItem.TabIndex = 3;
            btnRemoveItem.Text = "Remove Item";
            btnRemoveItem.UseVisualStyleBackColor = true;
            btnRemoveItem.Click += btnRemoveItem_Click;
            // 
            // btnSaveBuild
            // 
            btnSaveBuild.Font = new Font("Segoe UI", 10F);
            btnSaveBuild.Location = new Point(419, 324);
            btnSaveBuild.Name = "btnSaveBuild";
            btnSaveBuild.Size = new Size(120, 30);
            btnSaveBuild.TabIndex = 4;
            btnSaveBuild.Text = "Save Build";
            btnSaveBuild.UseVisualStyleBackColor = true;
            btnSaveBuild.Click += btnSaveBuild_Click;
            // 
            // btnLoadBuild
            // 
            btnLoadBuild.Font = new Font("Segoe UI", 10F);
            btnLoadBuild.Location = new Point(419, 360);
            btnLoadBuild.Name = "btnLoadBuild";
            btnLoadBuild.Size = new Size(120, 30);
            btnLoadBuild.TabIndex = 5;
            btnLoadBuild.Text = "Load Build";
            btnLoadBuild.UseVisualStyleBackColor = true;
            btnLoadBuild.Click += btnLoadBuild_Click;
            // 
            // btnShowLocation
            // 
            btnShowLocation.Font = new Font("Segoe UI", 10F);
            btnShowLocation.Location = new Point(419, 128);
            btnShowLocation.Name = "btnShowLocation";
            btnShowLocation.Size = new Size(120, 30);
            btnShowLocation.TabIndex = 6;
            btnShowLocation.Text = "Show Location";
            btnShowLocation.UseVisualStyleBackColor = true;
            btnShowLocation.Click += btnShowLocation_Click;
            // 
            // lblCharacterInfo
            // 
            lblCharacterInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCharacterInfo.AutoSize = true;
            lblCharacterInfo.Location = new Point(8, 393);
            lblCharacterInfo.Name = "lblCharacterInfo";
            lblCharacterInfo.Size = new Size(98, 15);
            lblCharacterInfo.TabIndex = 7;
            lblCharacterInfo.Text = "Character Info:";
            // 
            // txtBuildName
            // 
            txtBuildName.Location = new Point(372, 395);
            txtBuildName.Name = "txtBuildName";
            txtBuildName.Size = new Size(200, 23);
            txtBuildName.TabIndex = 8;
            // 
            // cmbSavedBuilds
            // 
            cmbSavedBuilds.FormattingEnabled = true;
            cmbSavedBuilds.Location = new Point(372, 426);
            cmbSavedBuilds.Name = "cmbSavedBuilds";
            cmbSavedBuilds.Size = new Size(200, 23);
            cmbSavedBuilds.TabIndex = 9;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, buildMenu, helpMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(584, 24);
            menuStrip.TabIndex = 10;
            // 
            // fileMenu
            // 
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(37, 20);
            fileMenu.Text = "File";
            fileMenu.DropDownItems.Add(new ToolStripMenuItem("Exit", null, ExitMenuItem_Click));
            // 
            // buildMenu
            // 
            buildMenu.Name = "buildMenu";
            buildMenu.Size = new Size(46, 20);
            buildMenu.Text = "Build";
            buildMenu.DropDownItems.Add(new ToolStripMenuItem("Save Build", null, btnSaveBuild_Click));
            buildMenu.DropDownItems.Add(new ToolStripMenuItem("Load Build", null, btnLoadBuild_Click));
            // 
            // helpMenu
            // 
            helpMenu.Name = "helpMenu";
            helpMenu.Size = new Size(44, 20);
            helpMenu.Text = "Help";
            helpMenu.DropDownItems.Add(new ToolStripMenuItem("About", null, AboutMenuItem_Click));
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 461);
            Controls.Add(menuStrip);
            Controls.Add(lblCharacterInfo);
            Controls.Add(btnShowLocation);
            Controls.Add(btnLoadBuild);
            Controls.Add(btnSaveBuild);
            Controls.Add(btnRemoveItem);
            Controls.Add(btnAddItem);
            Controls.Add(cmbItemTypeFilter);
            Controls.Add(lstItems);
            Controls.Add(txtBuildName);
            Controls.Add(cmbSavedBuilds);
            MainMenuStrip = menuStrip;
            Name = "Form1";
            Text = "Elden Ring Build Planner";
            Load += Form1_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstItems;
        private ComboBox cmbItemTypeFilter;
        private Button btnAddItem, btnRemoveItem, btnSaveBuild, btnLoadBuild, btnShowLocation;
        private Label lblCharacterInfo;
        private TextBox txtBuildName;
        private ComboBox cmbSavedBuilds;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu, buildMenu, helpMenu;
    }
}
