using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Tychaia.ProceduralGeneration
{
    /// <summary>
    /// "Zooms" in on the 1/4 center region of the parent layer, predicting the most likely
    /// values for the data that needs to be filled in (since resolution is being doubled).
    /// </summary>
    [Serializable]
    public class LayerZoom : Layer
    {
        [DefaultValue(false)]
        [Description("Whether edges between cells should be smoothed when zooming.")]
        public bool SmoothEdges
        {
            get;
            set;
        }

        [DefaultValue(1)]
        [Description("The number of zoom iterations to perform.")]
        public int Iterations
        {
            get;
            set;
        }

        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public LayerZoom()
        {
        }

        public LayerZoom(Layer parent)
            : base(parent)
        {
            this.SmoothEdges = false;
            this.Iterations = 1;
        }

        private int[] GenerateDataIterate(int iter, int x, int y, int width, int height)
        {
            int rx = x + width / 4 - 1;
            int ry = y + height / 4 - 1;
            int rw = width / 2 + 2;
            int rh = height / 2 + 2;
            // For smoothing to work, we need to know the cells that are actually
            // beyond the edge of the center.
            int[] parent = null;
            if (iter == this.Iterations)
                parent = this.Parent.GenerateData(rx, ry, rw, rh);
            else
                parent = this.GenerateDataIterate(iter + 1, rx, ry, rw, rh);
            int[] data = new int[width * height];

            // Copy all of the data across, doing a straight 2x zoom.
            for (int i = 0; i < width - 1; i += 2)
                for (int j = 0; j < height - 1; j += 2)
                {
                    // Adjust the i / j values to consider the edge data for
                    // the parent data position.
                    int px = i / 2 + 1;
                    int py = j / 2 + 1;

                    // Calculate the parent cells above, below and to the left / right
                    // of the current cell.
                    int current = parent[px + py * rw];
                    int north = parent[px + (py - 1) * rw];
                    int south = parent[px + (py + 1) * rw];
                    int east = parent[(px - 1) + py * rw];
                    int west = parent[(px + 1) + py * rw];

                    // Use the smoothing algorithm to determine the content of the filled area.
                    int topLeft, topRight, bottomLeft, bottomRight;
                    if (this.SmoothEdges)
                    {
                        topLeft = this.Smooth(i, j, north, current, east, current);
                        topRight = this.Smooth(i, j + 1, north, current, current, west);
                        bottomLeft = this.Smooth(i + 1, j, current, south, east, current);
                        bottomRight = this.Smooth(i + 1, j + 1, current, south, current, west);
                    }
                    else
                        topLeft = topRight = bottomLeft = bottomRight = current;

                    // Fill in the data set with selected values.
                    data[i + j * width] = topLeft;
                    data[i + (j + 1) * width] = bottomLeft;
                    data[(i + 1) + j * width] = topRight;
                    data[(i + 1) + (j + 1) * width] = bottomRight;
                }
                  
            return data;
        }

        public override int[] GenerateData(int x, int y, int width, int height)
        {
            if (this.Iterations > 0)
                return this.GenerateDataIterate(1, x, y, width, height);
            else
                return this.Parent.GenerateData(x, y, width, height);
        }

        private int Smooth(int x, int y, int northValue, int southValue, int eastValue, int westValue)
        {
            // Select one of the four options.
            Random r = this.GetCellRNG(x, y);
            int selected = r.Next(0, 4);

            switch (selected)
            {
                case 0:
                    return northValue;
                case 1:
                    return southValue;
                case 2:
                    return eastValue;
                case 3:
                    return westValue;
            }

            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return "Zoom Iterations";
        }
    }
}
