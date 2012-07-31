using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Tychaia.ProceduralGeneration
{
    /// <summary>
    /// Generates a layer of initial procedural generation data where each cell
    /// indicates either landmass or ocean.
    /// </summary>
    [DataContract]
    public class LayerInitialLand : Layer
    {
        [DataMember]
        [DefaultValue(0.9)]
        [Description("The value between 0.0 and 1.0 above which the cell is treated as land.")]
        public double LandLimit
        {
            get;
            set;
        }

        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public LayerInitialLand()
        {
        }

        public LayerInitialLand(int seed)
            : base(seed)
        {
            // Set defaults.
            this.LandLimit = 0.9;
        }

        public override int[] GenerateData(int x, int y, int width, int height)
        {
            int[] data = new int[width * height];

            for (int a = 0; a < width; a++)
                for (int b = 0; b < height; b++)
                {
                    Random r = this.GetCellRNG(x + a, y + b);
                    if (r.NextDouble() > this.LandLimit)
                        data[a + b * width] = 1;
                    else
                        data[a + b * width] = 0;
                }

            return data;
        }

        public override Dictionary<int, System.Drawing.Brush> GetLayerColors()
        {
            return LayerColors.ContinentBrushes;
        }

        public override string[] GetParentsRequired()
        {
            return new string[] { };
        }

        public override string ToString()
        {
            return "Initial Land";
        }
    }
}
