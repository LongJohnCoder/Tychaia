using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Tychaia.ProceduralGeneration
{
    /// <summary>
    /// Adds land to the existing land in the generator
    /// </summary>
    [DataContract()]
    public class LayerExtendLand : Layer
    {
        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public LayerExtendLand()
        {
        }

        public LayerExtendLand(Layer parent)
            : base(parent)
        {
        }

        public override int[] GenerateData(int x, int y, int width, int height)
        {
            if (this.Parents.Length < 1 || this.Parents[0] == null)
                return new int[width * height];

            int ox = 1;
            int oy = 1;
            int rw = width + ox * 2;
            int[] parent = this.Parents[0].GenerateData(x - ox, y - oy, rw, height + oy * 2);
            int[] data = new int[width * height];

            for (int i =0; i < width; i++)
                for (int j =0; j <height; j++)
                {
                    if (parent[(i + ox) + (j + oy) * rw] == 0)
                    {
                        Random r = this.GetCellRNG(x + i, y + j);
                        int selected = r.Next(0, 4);

                        switch (selected)
                        {
                            case 0:
                                data[i + j * width] = parent[(i + ox + 1) + (j + oy + 1) * rw];
                                break;
                            case 1:
                                data[i + j * width] = parent[(i + ox - 1) + (j + oy - 1) * rw];
                                break;
                            case 2:
                                data[i + j * width] = parent[(i + ox - 1) + (j + oy + 1) * rw];
                                break;
                            case 3:
                                data[i + j * width] = parent[(i + ox + 1) + (j + oy - 1) * rw];
                                break;
                        }
                    }
                    else
                        data[i + j * width] = parent[(i + ox) + (j + oy) * rw];
                }

            return data;
        }

        public override Dictionary<int, System.Drawing.Brush> GetLayerColors()
        {
            return LayerColors.BiomeBrushes;
        }

        public override string ToString()
        {
            return "Extend Land";
        }
    }
}
