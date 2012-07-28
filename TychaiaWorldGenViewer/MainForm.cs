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
        Bitmap m_RenderedImage = null;
        int m_Seed = 83645465;
        Brush[] BrushAssociations;

        public MainForm()
        {
            InitializeComponent();
            RevalidateForm();

            // Associate numbers with colours so that they are easier to identify.
            BrushAssociations = new Brush[]
            {
                /*   0 */ new SolidBrush(Color.FromArgb(0,0,255)),
                /*   1 */ new SolidBrush(Color.FromArgb(0,255,0))
            };
        }

        private void RevalidateForm()
        {
            this.c_PushButton.Enabled = true;
            this.c_PopButton.Enabled = this.c_LayersListBox.Items.Count > 0;
            this.c_RefreshImageButton.Enabled = this.c_LayersListBox.Items.Count > 0;
            this.c_SaveConfigButton.Enabled = true;
            this.c_LayerInspector.Enabled = this.c_LayersListBox.SelectedItem != null;
        }

        private void RegenerateImage()
        {
            this.RevalidateForm();
            this.m_RenderedImage = new Bitmap(this.c_RenderBox.Width, this.c_RenderBox.Height);
            this.c_RenderBox.Image = this.m_RenderedImage;
            if (this.c_LayersListBox.Items.Count == 0)
                return;
            Graphics g = Graphics.FromImage(this.m_RenderedImage);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            Layer l = this.c_LayersListBox.Items[this.c_LayersListBox.Items.Count - 1] as Layer;
            int[] data = l.GenerateData(0, 0, 256, 256);
            for (int x = 0; x < 256; x++)
                for (int y = 0; y < 256; y++)
                    g.FillRectangle(
                        BrushAssociations[data[x + y * 256]],
                        new Rectangle(x * 2, y * 2, 2, 2)
                        );
                    /*g.DrawString(
                        data[x + y * 256].ToString(),
                        new Font(FontFamily.GenericSansSerif, 8),
                        Brushes.Black,
                        new PointF(x * 2, y * 2)
                        );*/
        }

        private void c_PushButton_Click(object sender, EventArgs e)
        {
            this.c_PushLayerMenu.Show(this.c_PushButton, 0, this.c_PushButton.Height);

            // Enable / disable layers depending on whether they require a parent layer.
            this.c_PushGenerateContinentsMenuItem.Enabled = this.c_LayersListBox.Items.Count == 0;
        }

        private void c_CancelPushMenuItem_Click(object sender, EventArgs e)
        {
            this.c_PushLayerMenu.Hide();
        }

        #region Add Layers

        private void c_PushGenerateContinentsMenuItem_Click(object sender, EventArgs e)
        {
            this.c_LayersListBox.Items.Add(new LayerContinent(83645465));
            this.RegenerateImage();
        }

        #endregion

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
                        typeof(LayerContinent)
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
                        typeof(LayerContinent)
                    });
                SerializedLayers config = null;
                using (StreamReader reader = new StreamReader(ofd.FileName))
                    config = x.Deserialize(reader) as SerializedLayers;
                if (config == null)
                {
                    MessageBox.Show(this, "Unable to load configuration file.", "Configuration invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (Layer l in config)
                    this.c_LayersListBox.Items.Add(l);
                this.RegenerateImage();
            }
        }
    }

    [Serializable]
    [System.Xml.Serialization.XmlRoot("SerializedLayers")]
    public class SerializedLayers : List<Layer>
    {
    }
}
