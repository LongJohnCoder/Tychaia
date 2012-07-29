namespace TychaiaWorldGenViewer
{
    partial class MainForm
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
            this.c_RenderBox = new System.Windows.Forms.PictureBox();
            this.c_LayersListBox = new System.Windows.Forms.ListBox();
            this.c_LayerInspector = new System.Windows.Forms.PropertyGrid();
            this.c_PushButton = new System.Windows.Forms.Button();
            this.c_PopButton = new System.Windows.Forms.Button();
            this.c_PushLayerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.c_PushGenerateContinentsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.c_CancelPushMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_RefreshImageButton = new System.Windows.Forms.Button();
            this.c_SaveConfigButton = new System.Windows.Forms.Button();
            this.c_LoadConfigButton = new System.Windows.Forms.Button();
            this.c_PushZoomIterationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_ViewAtSelectedLayerCheckbox = new System.Windows.Forms.CheckBox();
            this.c_RenderProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.c_RenderBox)).BeginInit();
            this.c_PushLayerMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_RenderBox
            // 
            this.c_RenderBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_RenderBox.Location = new System.Drawing.Point(12, 12);
            this.c_RenderBox.Name = "c_RenderBox";
            this.c_RenderBox.Size = new System.Drawing.Size(512, 512);
            this.c_RenderBox.TabIndex = 0;
            this.c_RenderBox.TabStop = false;
            // 
            // c_LayersListBox
            // 
            this.c_LayersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_LayersListBox.FormattingEnabled = true;
            this.c_LayersListBox.IntegralHeight = false;
            this.c_LayersListBox.Location = new System.Drawing.Point(530, 12);
            this.c_LayersListBox.Name = "c_LayersListBox";
            this.c_LayersListBox.Size = new System.Drawing.Size(209, 415);
            this.c_LayersListBox.TabIndex = 1;
            this.c_LayersListBox.SelectedValueChanged += new System.EventHandler(this.c_LayersListBox_SelectedValueChanged);
            // 
            // c_LayerInspector
            // 
            this.c_LayerInspector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_LayerInspector.Enabled = false;
            this.c_LayerInspector.Location = new System.Drawing.Point(745, 12);
            this.c_LayerInspector.Name = "c_LayerInspector";
            this.c_LayerInspector.Size = new System.Drawing.Size(201, 512);
            this.c_LayerInspector.TabIndex = 2;
            this.c_LayerInspector.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.c_LayerInspector_PropertyValueChanged);
            // 
            // c_PushButton
            // 
            this.c_PushButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_PushButton.Location = new System.Drawing.Point(530, 456);
            this.c_PushButton.Name = "c_PushButton";
            this.c_PushButton.Size = new System.Drawing.Size(104, 31);
            this.c_PushButton.TabIndex = 5;
            this.c_PushButton.Text = "Push";
            this.c_PushButton.UseVisualStyleBackColor = true;
            this.c_PushButton.Click += new System.EventHandler(this.c_PushButton_Click);
            // 
            // c_PopButton
            // 
            this.c_PopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_PopButton.Location = new System.Drawing.Point(640, 456);
            this.c_PopButton.Name = "c_PopButton";
            this.c_PopButton.Size = new System.Drawing.Size(99, 31);
            this.c_PopButton.TabIndex = 6;
            this.c_PopButton.Text = "Pop";
            this.c_PopButton.UseVisualStyleBackColor = true;
            this.c_PopButton.Click += new System.EventHandler(this.c_PopButton_Click);
            // 
            // c_PushLayerMenu
            // 
            this.c_PushLayerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_PushGenerateContinentsMenuItem,
            this.c_PushZoomIterationsMenuItem,
            this.toolStripMenuItem1,
            this.c_CancelPushMenuItem});
            this.c_PushLayerMenu.Name = "c_AddLayerMenu";
            this.c_PushLayerMenu.Size = new System.Drawing.Size(183, 76);
            // 
            // c_PushGenerateContinentsMenuItem
            // 
            this.c_PushGenerateContinentsMenuItem.Name = "c_PushGenerateContinentsMenuItem";
            this.c_PushGenerateContinentsMenuItem.Size = new System.Drawing.Size(182, 22);
            this.c_PushGenerateContinentsMenuItem.Text = "Generate Continents";
            this.c_PushGenerateContinentsMenuItem.Click += new System.EventHandler(this.c_PushGenerateContinentsMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(179, 6);
            // 
            // c_CancelPushMenuItem
            // 
            this.c_CancelPushMenuItem.Name = "c_CancelPushMenuItem";
            this.c_CancelPushMenuItem.Size = new System.Drawing.Size(182, 22);
            this.c_CancelPushMenuItem.Text = "Cancel";
            this.c_CancelPushMenuItem.Click += new System.EventHandler(this.c_CancelPushMenuItem_Click);
            // 
            // c_RefreshImageButton
            // 
            this.c_RefreshImageButton.Location = new System.Drawing.Point(530, 493);
            this.c_RefreshImageButton.Name = "c_RefreshImageButton";
            this.c_RefreshImageButton.Size = new System.Drawing.Size(104, 31);
            this.c_RefreshImageButton.TabIndex = 8;
            this.c_RefreshImageButton.Text = "Refresh";
            this.c_RefreshImageButton.UseVisualStyleBackColor = true;
            // 
            // c_SaveConfigButton
            // 
            this.c_SaveConfigButton.Location = new System.Drawing.Point(640, 493);
            this.c_SaveConfigButton.Name = "c_SaveConfigButton";
            this.c_SaveConfigButton.Size = new System.Drawing.Size(49, 31);
            this.c_SaveConfigButton.TabIndex = 9;
            this.c_SaveConfigButton.Text = "Save";
            this.c_SaveConfigButton.UseVisualStyleBackColor = true;
            this.c_SaveConfigButton.Click += new System.EventHandler(this.c_SaveConfigButton_Click);
            // 
            // c_LoadConfigButton
            // 
            this.c_LoadConfigButton.Location = new System.Drawing.Point(690, 493);
            this.c_LoadConfigButton.Name = "c_LoadConfigButton";
            this.c_LoadConfigButton.Size = new System.Drawing.Size(49, 31);
            this.c_LoadConfigButton.TabIndex = 10;
            this.c_LoadConfigButton.Text = "Load";
            this.c_LoadConfigButton.UseVisualStyleBackColor = true;
            this.c_LoadConfigButton.Click += new System.EventHandler(this.c_LoadConfigButton_Click);
            // 
            // c_PushZoomIterationsMenuItem
            // 
            this.c_PushZoomIterationsMenuItem.Name = "c_PushZoomIterationsMenuItem";
            this.c_PushZoomIterationsMenuItem.Size = new System.Drawing.Size(182, 22);
            this.c_PushZoomIterationsMenuItem.Text = "Zoom Iterations";
            this.c_PushZoomIterationsMenuItem.Click += new System.EventHandler(this.c_PushZoomIterationsMenuItem_Click);
            // 
            // c_ViewAtSelectedLayerCheckbox
            // 
            this.c_ViewAtSelectedLayerCheckbox.AutoSize = true;
            this.c_ViewAtSelectedLayerCheckbox.Location = new System.Drawing.Point(530, 433);
            this.c_ViewAtSelectedLayerCheckbox.Name = "c_ViewAtSelectedLayerCheckbox";
            this.c_ViewAtSelectedLayerCheckbox.Size = new System.Drawing.Size(191, 17);
            this.c_ViewAtSelectedLayerCheckbox.TabIndex = 11;
            this.c_ViewAtSelectedLayerCheckbox.Text = "Show only up to the selected layer.";
            this.c_ViewAtSelectedLayerCheckbox.UseVisualStyleBackColor = true;
            this.c_ViewAtSelectedLayerCheckbox.CheckedChanged += new System.EventHandler(this.c_ViewAtSelectedLayerCheckbox_CheckedChanged);
            // 
            // c_RenderProgress
            // 
            this.c_RenderProgress.Location = new System.Drawing.Point(189, 258);
            this.c_RenderProgress.Name = "c_RenderProgress";
            this.c_RenderProgress.Size = new System.Drawing.Size(158, 21);
            this.c_RenderProgress.TabIndex = 12;
            this.c_RenderProgress.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 536);
            this.Controls.Add(this.c_LoadConfigButton);
            this.Controls.Add(this.c_SaveConfigButton);
            this.Controls.Add(this.c_RenderProgress);
            this.Controls.Add(this.c_ViewAtSelectedLayerCheckbox);
            this.Controls.Add(this.c_PushButton);
            this.Controls.Add(this.c_RefreshImageButton);
            this.Controls.Add(this.c_PopButton);
            this.Controls.Add(this.c_LayerInspector);
            this.Controls.Add(this.c_LayersListBox);
            this.Controls.Add(this.c_RenderBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Tychaia World Generation Experimentation";
            ((System.ComponentModel.ISupportInitialize)(this.c_RenderBox)).EndInit();
            this.c_PushLayerMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox c_RenderBox;
        private System.Windows.Forms.ListBox c_LayersListBox;
        private System.Windows.Forms.PropertyGrid c_LayerInspector;
        private System.Windows.Forms.Button c_PushButton;
        private System.Windows.Forms.Button c_PopButton;
        private System.Windows.Forms.ContextMenuStrip c_PushLayerMenu;
        private System.Windows.Forms.ToolStripMenuItem c_PushGenerateContinentsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem c_CancelPushMenuItem;
        private System.Windows.Forms.Button c_RefreshImageButton;
        private System.Windows.Forms.Button c_SaveConfigButton;
        private System.Windows.Forms.Button c_LoadConfigButton;
        private System.Windows.Forms.ToolStripMenuItem c_PushZoomIterationsMenuItem;
        private System.Windows.Forms.CheckBox c_ViewAtSelectedLayerCheckbox;
        private System.Windows.Forms.ProgressBar c_RenderProgress;
    }
}

