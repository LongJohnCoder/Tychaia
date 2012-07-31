using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tychaia.ProceduralGeneration;
using System.Xml.Serialization;
using System.IO;

namespace TychaiaWorldGenViewer
{
    public partial class MainForm : Form
    {
        Stack<Bitmap> m_RenderedImages = new Stack<Bitmap>();
        int m_Seed = 83645465;
        Brush m_UnknownAssociation;

        public MainForm()
        {
            InitializeComponent();
            RevalidateForm();

            // Associate numbers with colours so that they are easier to identify.
            this.m_UnknownAssociation = new SolidBrush(Color.FromArgb(63, 63, 63));
        }

        private void RevalidateForm()
        {
            this.c_PushButton.Enabled = true;
            this.c_PopButton.Enabled = this.c_LayersListBox.Items.Count > 0;
            this.c_RefreshImageButton.Enabled = this.c_LayersListBox.Items.Count > 0;
            this.c_SaveConfigButton.Enabled = true;
            this.c_LayerInspector.Enabled = this.c_LayersListBox.SelectedItem != null;
            this.RevalidateImage();
        }

        private Bitmap RegenerateImageForLayer(Layer l)
        {
            Bitmap b = new Bitmap(this.c_RenderBox.Width, this.c_RenderBox.Height);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            Dictionary<int, Brush> brushes = l.GetLayerColors();
            int[] data = l.GenerateData(0, 0, 256, 256);
            for (int x = 0; x < 256; x++)
                for (int y = 0; y < 256; y++)
                    if (brushes.ContainsKey(data[x + y * 256]))
                        g.FillRectangle(
                            brushes[data[x + y * 256]],
                            new Rectangle(x * 2, y * 2, 2, 2)
                            );
                    else
                        g.FillRectangle(
                            this.m_UnknownAssociation,
                            new Rectangle(x * 2, y * 2, 2, 2)
                            );
            return b;
        }

        private void RegenerateImage()
        {
            this.RevalidateForm();
            this.m_RenderedImages.Clear();
            if (this.c_LayersListBox.Items.Count == 0)
            {
                this.c_RenderBox.Image = null;
                return;
            }
            if (!this.c_ViewAtSelectedLayerCheckbox.Checked)
                this.m_RenderedImages.Push(
                    this.RegenerateImageForLayer(this.c_LayersListBox.Items[this.c_LayersListBox.Items.Count - 1] as Layer)
                );
            else
            {
                this.c_RenderProgress.Value = 0;
                this.c_RenderProgress.Visible = true;
                this.c_RenderProgress.Refresh();
                int i = 0;
                foreach (Layer l in this.c_LayersListBox.Items)
                {
                    this.m_RenderedImages.Push(this.RegenerateImageForLayer(l));
                    this.c_RenderProgress.Value = (int)(i++ / (double)this.c_LayersListBox.Items.Count * 100.0);
                    this.c_RenderProgress.Refresh();
                }
                this.c_RenderProgress.Value = 100;
                this.c_RenderProgress.Visible = false;
                this.c_RenderProgress.Refresh();
            }
            this.RevalidateImage();
        }

        private void RevalidateImage()
        {
            if (this.c_LayersListBox.Items.Count == 0 || this.m_RenderedImages.Count == 0)
            {
                this.c_RenderBox.Image = null;
                return;
            }
            if (this.c_ViewAtSelectedLayerCheckbox.Checked &&
                this.c_LayersListBox.SelectedIndex != -1 &&
                this.c_LayersListBox.SelectedIndex + 1 < this.m_RenderedImages.Count)
                this.c_RenderBox.Image = this.m_RenderedImages.ElementAt(this.m_RenderedImages.Count - (this.c_LayersListBox.SelectedIndex + 1));
            else
                this.c_RenderBox.Image = this.m_RenderedImages.Peek(); // Always the only image pushed.
        }

        #region Saving and Loading

        private void c_SaveConfigButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "XML Files|*.xml",
                CheckPathExists = true
            };
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlSerializer x = new XmlSerializer(typeof(SerializedLayers), new Type[]
                    {
                        typeof(LayerInitialLand),
                        typeof(LayerZoom),
                        typeof(LayerRandomBiome)
                    });
                SerializedLayers config = new SerializedLayers();
                foreach (Layer l in this.c_LayersListBox.Items)
                    config.Add(l);
                using (StreamWriter writer = new StreamWriter(sfd.FileName))
                    x.Serialize(writer, config);
                MessageBox.Show(this, "Save successful.", "Configuration saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void c_LoadConfigButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                this,
                "Are you sure you want to load a file?  You will lose the current configuration!",
                "Are you sure?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                return;
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "XML Files|*.xml",
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.c_LayersListBox.Items.Clear();
                this.RegenerateImage();
                XmlSerializer x = new XmlSerializer(typeof(SerializedLayers), new Type[]
                    {
                        typeof(LayerInitialLand),
                        typeof(LayerZoom),
                        typeof(LayerRandomBiome)
                    });
                SerializedLayers config = null;
                using (StreamReader reader = new StreamReader(ofd.FileName))
                    config = x.Deserialize(reader) as SerializedLayers;
                if (config == null)
                {
                    MessageBox.Show(this, "Unable to load configuration file.", "Configuration invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                for (int i = 0; i < config.Count; i++)
                    config[i].SetValues(i == 0 ? null : config[i - 1], this.m_Seed);
                foreach (Layer l in config)
                    this.c_LayersListBox.Items.Add(l);
                this.RegenerateImage();
            }
        }

        #endregion

        #region General Controls

        private void c_CancelPushMenuItem_Click(object sender, EventArgs e)
        {
            this.c_PushLayerMenu.Hide();
        }

        private void c_PopButton_Click(object sender, EventArgs e)
        {
            if (this.c_LayersListBox.Items.Count == 0)
                return;
            this.c_LayersListBox.Items.RemoveAt(this.c_LayersListBox.Items.Count - 1);
            this.RegenerateImage();
        }

        private void c_LayersListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.c_LayerInspector.SelectedObject = this.c_LayersListBox.SelectedItem;
            this.RevalidateForm();
        }

        private void c_LayerInspector_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            // This layer has been changed, so regenerate the image.
            this.RegenerateImage();
        }

        private void c_ViewAtSelectedLayerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // This toggle has been changed, so regenerate the image.
            this.RegenerateImage();
        }

        #endregion

        #region Add Layers

        private void c_PushButton_Click(object sender, EventArgs e)
        {
            this.c_PushLayerMenu.Show(this.c_PushButton, 0, this.c_PushButton.Height);

            // Enable / disable layers depending on whether they require a parent layer.
            this.c_PushGenerateContinentsMenuItem.Enabled = this.c_LayersListBox.Items.Count == 0;
            this.c_PushZoomIterationsMenuItem.Enabled = this.c_LayersListBox.Items.Count > 0;
            this.c_PushRandomBiomeMenuItem.Enabled = this.c_LayersListBox.Items.Count > 0;
        }

        private Layer GetParent()
        {
            return this.c_LayersListBox.Items[this.c_LayersListBox.Items.Count - 1] as Layer;
        }

        private void c_PushGenerateContinentsMenuItem_Click(object sender, EventArgs e)
        {
            this.c_LayersListBox.Items.Add(new LayerInitialLand(this.m_Seed));
            this.RegenerateImage();
        }

        private void c_PushZoomIterationsMenuItem_Click(object sender, EventArgs e)
        {
            this.c_LayersListBox.Items.Add(new LayerZoom(this.GetParent()));
            this.RegenerateImage();
        }

        private void c_PushRandomBiomeMenuItem_Click(object sender, EventArgs e)
        {
            this.c_LayersListBox.Items.Add(new LayerRandomBiome(this.GetParent()));
            this.RegenerateImage();
        }

        #endregion
    }

    [Serializable]
    [System.Xml.Serialization.XmlRoot("SerializedLayers")]
    public class SerializedLayers : List<Layer>
    {
    }
}
