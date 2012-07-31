using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Tychaia.ProceduralGeneration
{
    /// <summary>
    /// Replaces all of the specified integer IDs with a random selection of other
    /// integer IDs (where the integers often represent biomes).
    /// </summary>
    [DataContract()]
    public class LayerRandomBiome : Layer
    {
        [DataMember]
        [DefaultValue(1)]
        [Description("The first integer ID that can be placed as a biome.")]
        public int BiomeIDFirst
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue(4)]
        [Description("The last integer ID that can be placed as a biome.")]
        public int BiomeIDLast
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue(1)]
        [Description("The integer ID that is to be replaced by a random biome.")]
        public int BiomeReplace
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue("5, 4, 1, 1")]
        [Description("A comma-seperated list of integers that determines the favourance towards a particular biome.")]
        public string BiomeFavourance
        {
            get;
            set;
        }

        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public LayerRandomBiome()
        {
        }

        public LayerRandomBiome(Layer parent)
            : base(parent)
        {
            // Set defaults.
            this.BiomeIDFirst = 1;
            this.BiomeIDLast = 4;
            this.BiomeReplace = 1;
            this.BiomeFavourance = "5, 4, 1, 1";
        }

        private int[] ParseFavourance()
        {
            if (this.BiomeFavourance == null)
                this.BiomeFavourance = string.Join(",", new int[this.BiomeIDLast - this.BiomeIDFirst + 1].Select(v => 1));
            string[] values = this.BiomeFavourance.Split(',');
            return values.Select<string, int>(v => Convert.ToInt32(v.Trim())).ToArray();
        }

        public override int[] GenerateData(int x, int y, int width, int height)
        {
            if (this.Parents.Length < 1 || this.Parents[0] == null)
                return new int[width * height];
            int[] parent = this.Parents[0].GenerateData(x, y, width, height);
            int[] data = new int[width * height];

            // Work out favourance values.
            int[] raw = this.ParseFavourance();
            int total = Math.Max(0, Math.Min(100, raw.Sum()));
            int[] favourance = new int[total];
            int i = 0, c = 0;
            for (int k = 0; k < total; k++)
            {
                favourance[k] = this.BiomeIDFirst + i;
                c++;
                if (c >= raw[i])
                {
                    i++;
                    c = 0;
                }
            }

            for (int a = 0; a < width; a++)
                for (int b = 0; b < height; b++)
                {
                    Random r = this.GetCellRNG(x + a, y + b);
                    if (parent[a + b * width] == this.BiomeReplace)
                        data[a + b * width] = favourance[r.Next(0, total)];
                    else
                        data[a + b * width] = parent[a + b * width];
                }

            return data;
        }

        public override Dictionary<int, System.Drawing.Brush> GetLayerColors()
        {
            return LayerColors.BiomeBrushes;
        }

        public override string ToString()
        {
            return "Biomize";
        }
    }
}
