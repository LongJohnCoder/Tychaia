namespace TychaiaWorldGenViewer
{
    partial class FlowForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowForm));
            this.c_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.c_ZoomStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.c_LayerInspector = new System.Windows.Forms.PropertyGrid();
            this.c_ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.c_GeneralMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddInitialPerlinMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddInitialVoronoiMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddMixVoronoiMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddZoomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddSmoothMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddRemapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddInvertMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddNormalizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_GeneralAddDenormalizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_Seperator2MenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.c_GeneralAddStoreResultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_LandMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_LandAddInitialLandMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_LandAddExtendLandMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_BiomeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_BiomeAddScatterBiomeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_RainfallMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_RainfallAddInitialRainfallMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_RainfallAddMixRainfallWithBiomeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_TemperatureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_TemperatureAddInitialTemperature = new System.Windows.Forms.ToolStripMenuItem();
            this.c_TemperatureAddMixTemperatureWithBiomeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_RiversMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_RiversAddSimulateFlowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_RiversAddPoolLakesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_RiversAddPoolOceanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_TownsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_TownsAddScatterTownsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_TownsAddDetermineViabilityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_TownsAddMixTownsWithViabilityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_TownsAddSimulateRundownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_FamilyTreesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.c_ToolStrip = new System.Windows.Forms.ToolStrip();
            this.c_LoadConfigurationButton = new System.Windows.Forms.ToolStripButton();
            this.c_SaveConfigurationButton = new System.Windows.Forms.ToolStripButton();
            this.c_SaveConfigurationAsButton = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.c_YLabel = new System.Windows.Forms.Label();
            this.c_XLabel = new System.Windows.Forms.Label();
            this.c_XNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.c_YNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.c_FlowInterfaceControl = new TychaiaWorldGenViewer.Flow.FlowInterfaceControl();
            this.c_StatusStrip.SuspendLayout();
            this.c_ContextMenuStrip.SuspendLayout();
            this.c_ToolStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c_XNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_YNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // c_StatusStrip
            // 
            this.c_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_ZoomStatus});
            this.c_StatusStrip.Location = new System.Drawing.Point(0, 475);
            this.c_StatusStrip.Name = "c_StatusStrip";
            this.c_StatusStrip.Size = new System.Drawing.Size(890, 22);
            this.c_StatusStrip.TabIndex = 2;
            this.c_StatusStrip.Text = "statusStrip1";
            // 
            // c_ZoomStatus
            // 
            this.c_ZoomStatus.Name = "c_ZoomStatus";
            this.c_ZoomStatus.Size = new System.Drawing.Size(35, 17);
            this.c_ZoomStatus.Text = "100%";
            // 
            // c_LayerInspector
            // 
            this.c_LayerInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_LayerInspector.Location = new System.Drawing.Point(3, 30);
            this.c_LayerInspector.Name = "c_LayerInspector";
            this.c_LayerInspector.Size = new System.Drawing.Size(194, 417);
            this.c_LayerInspector.TabIndex = 3;
            this.c_LayerInspector.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.c_LayerInspector_PropertyValueChanged);
            // 
            // c_ContextMenuStrip
            // 
            this.c_ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_GeneralMenuItem,
            this.c_LandMenuItem,
            this.c_BiomeMenuItem,
            this.c_RainfallMenuItem,
            this.c_TemperatureMenuItem,
            this.c_RiversMenuItem,
            this.c_TownsMenuItem,
            this.c_FamilyTreesMenuItem});
            this.c_ContextMenuStrip.Name = "contextMenuStrip1";
            this.c_ContextMenuStrip.Size = new System.Drawing.Size(153, 202);
            // 
            // c_GeneralMenuItem
            // 
            this.c_GeneralMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_GeneralAddInitialPerlinMenuItem,
            this.c_GeneralAddInitialVoronoiMenuItem,
            this.c_GeneralAddMixVoronoiMenuItem,
            this.c_GeneralAddZoomMenuItem,
            this.c_GeneralAddSmoothMenuItem,
            this.c_GeneralAddRemapMenuItem,
            this.c_GeneralAddInvertMenuItem,
            this.c_GeneralAddNormalizeMenuItem,
            this.c_GeneralAddDenormalizeMenuItem,
            this.c_Seperator2MenuItem,
            this.c_GeneralAddStoreResultMenuItem});
            this.c_GeneralMenuItem.Name = "c_GeneralMenuItem";
            this.c_GeneralMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralMenuItem.Text = "General";
            // 
            // c_GeneralAddInitialPerlinMenuItem
            // 
            this.c_GeneralAddInitialPerlinMenuItem.Name = "c_GeneralAddInitialPerlinMenuItem";
            this.c_GeneralAddInitialPerlinMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddInitialPerlinMenuItem.Text = "Initial Perlin";
            this.c_GeneralAddInitialPerlinMenuItem.Click += new System.EventHandler(this.c_GeneralAddInitialPerlinMenuItem_Click);
            // 
            // c_GeneralAddInitialVoronoiMenuItem
            // 
            this.c_GeneralAddInitialVoronoiMenuItem.Name = "c_GeneralAddInitialVoronoiMenuItem";
            this.c_GeneralAddInitialVoronoiMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddInitialVoronoiMenuItem.Text = "Initial Voronoi";
            this.c_GeneralAddInitialVoronoiMenuItem.Click += new System.EventHandler(this.c_GeneralAddInitialVoronoiMenuItem_Click);
            // 
            // c_GeneralAddMixVoronoiMenuItem
            // 
            this.c_GeneralAddMixVoronoiMenuItem.Name = "c_GeneralAddMixVoronoiMenuItem";
            this.c_GeneralAddMixVoronoiMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddMixVoronoiMenuItem.Text = "Mix Voronoi";
            this.c_GeneralAddMixVoronoiMenuItem.Click += new System.EventHandler(this.c_GeneralAddMixVoronoiMenuItem_Click);
            // 
            // c_GeneralAddZoomMenuItem
            // 
            this.c_GeneralAddZoomMenuItem.Name = "c_GeneralAddZoomMenuItem";
            this.c_GeneralAddZoomMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddZoomMenuItem.Text = "Zoom";
            this.c_GeneralAddZoomMenuItem.Click += new System.EventHandler(this.c_GeneralAddZoomMenuItem_Click);
            // 
            // c_GeneralAddSmoothMenuItem
            // 
            this.c_GeneralAddSmoothMenuItem.Enabled = false;
            this.c_GeneralAddSmoothMenuItem.Name = "c_GeneralAddSmoothMenuItem";
            this.c_GeneralAddSmoothMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddSmoothMenuItem.Text = "Smooth";
            // 
            // c_GeneralAddRemapMenuItem
            // 
            this.c_GeneralAddRemapMenuItem.Enabled = false;
            this.c_GeneralAddRemapMenuItem.Name = "c_GeneralAddRemapMenuItem";
            this.c_GeneralAddRemapMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddRemapMenuItem.Text = "Remap";
            // 
            // c_GeneralAddInvertMenuItem
            // 
            this.c_GeneralAddInvertMenuItem.Name = "c_GeneralAddInvertMenuItem";
            this.c_GeneralAddInvertMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddInvertMenuItem.Text = "Invert";
            this.c_GeneralAddInvertMenuItem.Click += new System.EventHandler(this.c_GeneralAddInvertMenuItem_Click);
            // 
            // c_GeneralAddNormalizeMenuItem
            // 
            this.c_GeneralAddNormalizeMenuItem.Enabled = false;
            this.c_GeneralAddNormalizeMenuItem.Name = "c_GeneralAddNormalizeMenuItem";
            this.c_GeneralAddNormalizeMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddNormalizeMenuItem.Text = "Normalize";
            // 
            // c_GeneralAddDenormalizeMenuItem
            // 
            this.c_GeneralAddDenormalizeMenuItem.Enabled = false;
            this.c_GeneralAddDenormalizeMenuItem.Name = "c_GeneralAddDenormalizeMenuItem";
            this.c_GeneralAddDenormalizeMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddDenormalizeMenuItem.Text = "Denormalize";
            // 
            // c_Seperator2MenuItem
            // 
            this.c_Seperator2MenuItem.Name = "c_Seperator2MenuItem";
            this.c_Seperator2MenuItem.Size = new System.Drawing.Size(149, 6);
            // 
            // c_GeneralAddStoreResultMenuItem
            // 
            this.c_GeneralAddStoreResultMenuItem.Name = "c_GeneralAddStoreResultMenuItem";
            this.c_GeneralAddStoreResultMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_GeneralAddStoreResultMenuItem.Text = "Store Result";
            this.c_GeneralAddStoreResultMenuItem.Click += new System.EventHandler(this.c_GeneralAddStoreResultMenuItem_Click);
            // 
            // c_LandMenuItem
            // 
            this.c_LandMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_LandAddInitialLandMenuItem,
            this.c_LandAddExtendLandMenuItem});
            this.c_LandMenuItem.Name = "c_LandMenuItem";
            this.c_LandMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_LandMenuItem.Text = "Land";
            // 
            // c_LandAddInitialLandMenuItem
            // 
            this.c_LandAddInitialLandMenuItem.Name = "c_LandAddInitialLandMenuItem";
            this.c_LandAddInitialLandMenuItem.Size = new System.Drawing.Size(138, 22);
            this.c_LandAddInitialLandMenuItem.Text = "Initial Land";
            this.c_LandAddInitialLandMenuItem.Click += new System.EventHandler(this.c_LandAddInitialLandMenuItem_Click);
            // 
            // c_LandAddExtendLandMenuItem
            // 
            this.c_LandAddExtendLandMenuItem.Name = "c_LandAddExtendLandMenuItem";
            this.c_LandAddExtendLandMenuItem.Size = new System.Drawing.Size(138, 22);
            this.c_LandAddExtendLandMenuItem.Text = "Extend Land";
            this.c_LandAddExtendLandMenuItem.Click += new System.EventHandler(this.c_LandAddExtendLandMenuItem_Click);
            // 
            // c_BiomeMenuItem
            // 
            this.c_BiomeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_BiomeAddScatterBiomeMenuItem});
            this.c_BiomeMenuItem.Name = "c_BiomeMenuItem";
            this.c_BiomeMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_BiomeMenuItem.Text = "Biome";
            // 
            // c_BiomeAddScatterBiomeMenuItem
            // 
            this.c_BiomeAddScatterBiomeMenuItem.Name = "c_BiomeAddScatterBiomeMenuItem";
            this.c_BiomeAddScatterBiomeMenuItem.Size = new System.Drawing.Size(147, 22);
            this.c_BiomeAddScatterBiomeMenuItem.Text = "Scatter Biome";
            this.c_BiomeAddScatterBiomeMenuItem.Click += new System.EventHandler(this.c_BiomeAddScatterBiomeMenuItem_Click);
            // 
            // c_RainfallMenuItem
            // 
            this.c_RainfallMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_RainfallAddInitialRainfallMenuItem,
            this.c_RainfallAddMixRainfallWithBiomeMenuItem});
            this.c_RainfallMenuItem.Enabled = false;
            this.c_RainfallMenuItem.Name = "c_RainfallMenuItem";
            this.c_RainfallMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_RainfallMenuItem.Text = "Rainfall";
            // 
            // c_RainfallAddInitialRainfallMenuItem
            // 
            this.c_RainfallAddInitialRainfallMenuItem.Name = "c_RainfallAddInitialRainfallMenuItem";
            this.c_RainfallAddInitialRainfallMenuItem.Size = new System.Drawing.Size(198, 22);
            this.c_RainfallAddInitialRainfallMenuItem.Text = "Initial Rainfall";
            // 
            // c_RainfallAddMixRainfallWithBiomeMenuItem
            // 
            this.c_RainfallAddMixRainfallWithBiomeMenuItem.Name = "c_RainfallAddMixRainfallWithBiomeMenuItem";
            this.c_RainfallAddMixRainfallWithBiomeMenuItem.Size = new System.Drawing.Size(198, 22);
            this.c_RainfallAddMixRainfallWithBiomeMenuItem.Text = "Mix Rainfall with Biome";
            // 
            // c_TemperatureMenuItem
            // 
            this.c_TemperatureMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_TemperatureAddInitialTemperature,
            this.c_TemperatureAddMixTemperatureWithBiomeMenuItem});
            this.c_TemperatureMenuItem.Enabled = false;
            this.c_TemperatureMenuItem.Name = "c_TemperatureMenuItem";
            this.c_TemperatureMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_TemperatureMenuItem.Text = "Temperature";
            // 
            // c_TemperatureAddInitialTemperature
            // 
            this.c_TemperatureAddInitialTemperature.Name = "c_TemperatureAddInitialTemperature";
            this.c_TemperatureAddInitialTemperature.Size = new System.Drawing.Size(227, 22);
            this.c_TemperatureAddInitialTemperature.Text = "Initial Temperature";
            // 
            // c_TemperatureAddMixTemperatureWithBiomeMenuItem
            // 
            this.c_TemperatureAddMixTemperatureWithBiomeMenuItem.Name = "c_TemperatureAddMixTemperatureWithBiomeMenuItem";
            this.c_TemperatureAddMixTemperatureWithBiomeMenuItem.Size = new System.Drawing.Size(227, 22);
            this.c_TemperatureAddMixTemperatureWithBiomeMenuItem.Text = "Mix Temperature with Biome";
            // 
            // c_RiversMenuItem
            // 
            this.c_RiversMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_RiversAddSimulateFlowMenuItem,
            this.c_RiversAddPoolLakesMenuItem,
            this.c_RiversAddPoolOceanMenuItem});
            this.c_RiversMenuItem.Enabled = false;
            this.c_RiversMenuItem.Name = "c_RiversMenuItem";
            this.c_RiversMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_RiversMenuItem.Text = "Rivers";
            // 
            // c_RiversAddSimulateFlowMenuItem
            // 
            this.c_RiversAddSimulateFlowMenuItem.Name = "c_RiversAddSimulateFlowMenuItem";
            this.c_RiversAddSimulateFlowMenuItem.Size = new System.Drawing.Size(148, 22);
            this.c_RiversAddSimulateFlowMenuItem.Text = "Simulate Flow";
            // 
            // c_RiversAddPoolLakesMenuItem
            // 
            this.c_RiversAddPoolLakesMenuItem.Name = "c_RiversAddPoolLakesMenuItem";
            this.c_RiversAddPoolLakesMenuItem.Size = new System.Drawing.Size(148, 22);
            this.c_RiversAddPoolLakesMenuItem.Text = "Pool Lakes";
            // 
            // c_RiversAddPoolOceanMenuItem
            // 
            this.c_RiversAddPoolOceanMenuItem.Name = "c_RiversAddPoolOceanMenuItem";
            this.c_RiversAddPoolOceanMenuItem.Size = new System.Drawing.Size(148, 22);
            this.c_RiversAddPoolOceanMenuItem.Text = "Pool Ocean";
            // 
            // c_TownsMenuItem
            // 
            this.c_TownsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_TownsAddScatterTownsMenuItem,
            this.c_TownsAddDetermineViabilityMenuItem,
            this.c_TownsAddMixTownsWithViabilityMenuItem,
            this.c_TownsAddSimulateRundownMenuItem});
            this.c_TownsMenuItem.Enabled = false;
            this.c_TownsMenuItem.Name = "c_TownsMenuItem";
            this.c_TownsMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_TownsMenuItem.Text = "Towns";
            // 
            // c_TownsAddScatterTownsMenuItem
            // 
            this.c_TownsAddScatterTownsMenuItem.Name = "c_TownsAddScatterTownsMenuItem";
            this.c_TownsAddScatterTownsMenuItem.Size = new System.Drawing.Size(202, 22);
            this.c_TownsAddScatterTownsMenuItem.Text = "Scatter Towns";
            // 
            // c_TownsAddDetermineViabilityMenuItem
            // 
            this.c_TownsAddDetermineViabilityMenuItem.Name = "c_TownsAddDetermineViabilityMenuItem";
            this.c_TownsAddDetermineViabilityMenuItem.Size = new System.Drawing.Size(202, 22);
            this.c_TownsAddDetermineViabilityMenuItem.Text = "Determine Viability";
            // 
            // c_TownsAddMixTownsWithViabilityMenuItem
            // 
            this.c_TownsAddMixTownsWithViabilityMenuItem.Name = "c_TownsAddMixTownsWithViabilityMenuItem";
            this.c_TownsAddMixTownsWithViabilityMenuItem.Size = new System.Drawing.Size(202, 22);
            this.c_TownsAddMixTownsWithViabilityMenuItem.Text = "Mix Towns with Viability";
            // 
            // c_TownsAddSimulateRundownMenuItem
            // 
            this.c_TownsAddSimulateRundownMenuItem.Name = "c_TownsAddSimulateRundownMenuItem";
            this.c_TownsAddSimulateRundownMenuItem.Size = new System.Drawing.Size(202, 22);
            this.c_TownsAddSimulateRundownMenuItem.Text = "Simulate Rundown";
            // 
            // c_FamilyTreesMenuItem
            // 
            this.c_FamilyTreesMenuItem.Enabled = false;
            this.c_FamilyTreesMenuItem.Name = "c_FamilyTreesMenuItem";
            this.c_FamilyTreesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.c_FamilyTreesMenuItem.Text = "Family Trees";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(6, 6);
            // 
            // c_ToolStrip
            // 
            this.c_ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_LoadConfigurationButton,
            this.c_SaveConfigurationButton,
            this.c_SaveConfigurationAsButton});
            this.c_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.c_ToolStrip.Name = "c_ToolStrip";
            this.c_ToolStrip.Size = new System.Drawing.Size(890, 25);
            this.c_ToolStrip.TabIndex = 4;
            this.c_ToolStrip.Text = "toolStrip1";
            // 
            // c_LoadConfigurationButton
            // 
            this.c_LoadConfigurationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_LoadConfigurationButton.Image = ((System.Drawing.Image)(resources.GetObject("c_LoadConfigurationButton.Image")));
            this.c_LoadConfigurationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_LoadConfigurationButton.Name = "c_LoadConfigurationButton";
            this.c_LoadConfigurationButton.Size = new System.Drawing.Size(23, 22);
            this.c_LoadConfigurationButton.Text = "Load Configuration";
            this.c_LoadConfigurationButton.Click += new System.EventHandler(this.c_LoadConfigurationButton_Click);
            // 
            // c_SaveConfigurationButton
            // 
            this.c_SaveConfigurationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_SaveConfigurationButton.Enabled = false;
            this.c_SaveConfigurationButton.Image = ((System.Drawing.Image)(resources.GetObject("c_SaveConfigurationButton.Image")));
            this.c_SaveConfigurationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_SaveConfigurationButton.Name = "c_SaveConfigurationButton";
            this.c_SaveConfigurationButton.Size = new System.Drawing.Size(23, 22);
            this.c_SaveConfigurationButton.Text = "Save Configuration";
            this.c_SaveConfigurationButton.Click += new System.EventHandler(this.c_SaveConfigurationButton_Click);
            // 
            // c_SaveConfigurationAsButton
            // 
            this.c_SaveConfigurationAsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_SaveConfigurationAsButton.Image = ((System.Drawing.Image)(resources.GetObject("c_SaveConfigurationAsButton.Image")));
            this.c_SaveConfigurationAsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_SaveConfigurationAsButton.Name = "c_SaveConfigurationAsButton";
            this.c_SaveConfigurationAsButton.Size = new System.Drawing.Size(23, 22);
            this.c_SaveConfigurationAsButton.Text = "Save Configuration As...";
            this.c_SaveConfigurationAsButton.Click += new System.EventHandler(this.c_SaveConfigurationAsButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.c_LayerInspector, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(690, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 450);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.c_YLabel, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.c_XLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.c_XNumericUpDown, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.c_YNumericUpDown, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 27);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // c_YLabel
            // 
            this.c_YLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_YLabel.Location = new System.Drawing.Point(103, 5);
            this.c_YLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.c_YLabel.Name = "c_YLabel";
            this.c_YLabel.Size = new System.Drawing.Size(19, 19);
            this.c_YLabel.TabIndex = 3;
            this.c_YLabel.Text = "Y:";
            // 
            // c_XLabel
            // 
            this.c_XLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_XLabel.Location = new System.Drawing.Point(3, 5);
            this.c_XLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.c_XLabel.Name = "c_XLabel";
            this.c_XLabel.Size = new System.Drawing.Size(19, 19);
            this.c_XLabel.TabIndex = 0;
            this.c_XLabel.Text = "X:";
            // 
            // c_XNumericUpDown
            // 
            this.c_XNumericUpDown.Location = new System.Drawing.Point(28, 3);
            this.c_XNumericUpDown.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.c_XNumericUpDown.Minimum = new decimal(new int[] {
            6000,
            0,
            0,
            -2147483648});
            this.c_XNumericUpDown.Name = "c_XNumericUpDown";
            this.c_XNumericUpDown.Size = new System.Drawing.Size(69, 20);
            this.c_XNumericUpDown.TabIndex = 1;
            this.c_XNumericUpDown.ValueChanged += new System.EventHandler(this.c_XNumericUpDown_ValueChanged);
            // 
            // c_YNumericUpDown
            // 
            this.c_YNumericUpDown.Location = new System.Drawing.Point(128, 3);
            this.c_YNumericUpDown.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.c_YNumericUpDown.Minimum = new decimal(new int[] {
            6000,
            0,
            0,
            -2147483648});
            this.c_YNumericUpDown.Name = "c_YNumericUpDown";
            this.c_YNumericUpDown.Size = new System.Drawing.Size(69, 20);
            this.c_YNumericUpDown.TabIndex = 2;
            this.c_YNumericUpDown.ValueChanged += new System.EventHandler(this.c_YNumericUpDown_ValueChanged);
            // 
            // c_FlowInterfaceControl
            // 
            this.c_FlowInterfaceControl.ContextMenuStrip = this.c_ContextMenuStrip;
            this.c_FlowInterfaceControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_FlowInterfaceControl.Location = new System.Drawing.Point(0, 25);
            this.c_FlowInterfaceControl.Name = "c_FlowInterfaceControl";
            this.c_FlowInterfaceControl.SelectedElement = null;
            this.c_FlowInterfaceControl.Size = new System.Drawing.Size(890, 472);
            this.c_FlowInterfaceControl.TabIndex = 0;
            this.c_FlowInterfaceControl.Zoom = 1F;
            this.c_FlowInterfaceControl.SelectedElementChanged += new System.EventHandler(this.c_FlowInterfaceControl_SelectedElementChanged);
            this.c_FlowInterfaceControl.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.c_FlowInterfaceControl_MouseWheel);
            // 
            // FlowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 497);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.c_StatusStrip);
            this.Controls.Add(this.c_FlowInterfaceControl);
            this.Controls.Add(this.c_ToolStrip);
            this.Name = "FlowForm";
            this.Text = "Tychaia World Experimentation Tool";
            this.c_StatusStrip.ResumeLayout(false);
            this.c_StatusStrip.PerformLayout();
            this.c_ContextMenuStrip.ResumeLayout(false);
            this.c_ToolStrip.ResumeLayout(false);
            this.c_ToolStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c_XNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_YNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Flow.FlowInterfaceControl c_FlowInterfaceControl;
        private System.Windows.Forms.StatusStrip c_StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel c_ZoomStatus;
        private System.Windows.Forms.PropertyGrid c_LayerInspector;
        private System.Windows.Forms.ContextMenuStrip c_ContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddZoomMenuItem;
        private System.Windows.Forms.ToolStripSeparator c_Seperator2MenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddStoreResultMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_BiomeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_BiomeAddScatterBiomeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_RainfallMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_RainfallAddInitialRainfallMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_RainfallAddMixRainfallWithBiomeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_TemperatureMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_TemperatureAddInitialTemperature;
        private System.Windows.Forms.ToolStripMenuItem c_TemperatureAddMixTemperatureWithBiomeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_TownsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_TownsAddScatterTownsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_TownsAddDetermineViabilityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_TownsAddMixTownsWithViabilityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_TownsAddSimulateRundownMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_FamilyTreesMenuItem;
        private System.Windows.Forms.ToolStrip c_ToolStrip;
        private System.Windows.Forms.ToolStripButton c_LoadConfigurationButton;
        private System.Windows.Forms.ToolStripButton c_SaveConfigurationButton;
        private System.Windows.Forms.ToolStripButton c_SaveConfigurationAsButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label c_YLabel;
        private System.Windows.Forms.Label c_XLabel;
        private System.Windows.Forms.NumericUpDown c_XNumericUpDown;
        private System.Windows.Forms.NumericUpDown c_YNumericUpDown;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddSmoothMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddInitialPerlinMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_RiversMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_RiversAddSimulateFlowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_RiversAddPoolLakesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_RiversAddPoolOceanMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddInitialVoronoiMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_LandMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_LandAddInitialLandMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_LandAddExtendLandMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddRemapMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddInvertMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddNormalizeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddDenormalizeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_GeneralAddMixVoronoiMenuItem;
    }
}