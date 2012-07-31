using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Tychaia.ProceduralGeneration
{
    /// <summary>
    /// Mixs a Voronoi and Perlin noise map together to get scaled
    /// sections of data.
    /// </summary>
    [DataContract()]
    public class LayerVoronoiMixdown : Layer
    {
        [DataMember]
        [DefaultValue(15)]
        [Description("The edge sampling range used in order to provide a consistant result across the infinite range.")]
        public int EdgeSampling
        {
            get;
            set;
        }

        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public LayerVoronoiMixdown()
        {
        }

        public LayerVoronoiMixdown(Layer voronoi, Layer parent)
            : base(new Layer[] { voronoi, parent })
        {
            // Set defaults.
            this.EdgeSampling = 15;
        }

        private const int MAPPING_OFFSET = 10;

        public override int[] GenerateData(int x, int y, int width, int height)
        {
            if (this.Parents.Length < 2 || this.Parents[0] == null || this.Parents[1] == null)
                return new int[width * height];

            int ox = this.EdgeSampling;
            int oy = this.EdgeSampling;
            int rx = x - this.EdgeSampling;
            int ry = y - this.EdgeSampling;
            int rw = width + this.EdgeSampling * 2;
            int rh = height + this.EdgeSampling * 2;

            int[] voronoi = this.Parents[0].GenerateData(rx, ry, rw, rh);
            int[] parent = this.Parents[1].GenerateData(rx, ry, rw, rh);
            int[] tracker = new int[rw * rh];
            int[] data = new int[width * height];

            // Our Voronoi input will have empty space as 0, points as 1 and edges as 2.
            // We have to create a map of points to parent values.
            List<Point> pointMap = new List<Point>();
            List<int> valueMap = new List<int>();
            for (int px = 0; px < rw; px++)
                for (int py = 0; py < rh; py++)
                    if (voronoi[px + py * rw] == 1)
                    {
                        pointMap.Add(new Point { X = px - ox, Y = py - oy });
                        valueMap.Add(parent[px + py * rw]);
                    }

            // Apply <source point index> to the data map so that we can easily fill
            // in the information.
            try
            {
                for (int i = 0; i < valueMap.Count; i++)
                    this.RecursiveApply(tracker, voronoi, i, pointMap[i], width, height);
            }
            catch (StackOverflowException)
            {
                // The user requested too large a sampling area, so stop sampling.
            }

            // Now iterate over the data array, using the data as the index to fetch the value
            // from the parent array.
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (tracker[(i + ox) + (j + oy) * rw] >= MAPPING_OFFSET)
                        data[i + j * width] = valueMap[tracker[(i + ox) + (j + oy) * rw] - MAPPING_OFFSET];

            // Now fill in all of the blanks, using the data that is has just been created.
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (tracker[(i + ox) + (j + oy) * rw] < MAPPING_OFFSET)
                        data[i + j * width] = this.FindNeighbour(valueMap, tracker, data, x, y, i, j, width, height);

            return data;
        }

        private int FindNeighbour(List<int> valueMap, int[] tracker, int[] data, int x, int y, int i, int j, int width, int height)
        {
            Point p = new Point(i, j);
            int ox = this.EdgeSampling;
            int oy = this.EdgeSampling;
            int rw = width + this.EdgeSampling * 2;
            int rh = height + this.EdgeSampling * 2;

            Random r = this.GetCellRNG(x + i, y + j);
            switch (r.Next(4))
            {
                case 0:
                    if (tracker[(i - 1 + ox) + (j + oy) * rw] >= MAPPING_OFFSET)
                        return valueMap[tracker[(i - 1 + ox) + (j + oy) * rw] - MAPPING_OFFSET];
                    break;
                case 1:
                    if (tracker[(i + 1 + ox) + (j + oy) * rw] >= MAPPING_OFFSET)
                        return valueMap[tracker[(i + 1 + ox) + (j + oy) * rw] - MAPPING_OFFSET];
                    break;
                case 2:
                    if (tracker[(i + ox) + (j - 1 + oy) * rw] >= MAPPING_OFFSET)
                        return valueMap[tracker[(i + ox) + (j - 1 + oy) * rw] - MAPPING_OFFSET];
                    break;
                case 3:
                    if (tracker[(i + ox) + (j + 1 + oy) * rw] >= MAPPING_OFFSET)
                        return valueMap[tracker[(i + ox) + (j + 1 + oy) * rw] - MAPPING_OFFSET];
                    break;
            }

            return this.FindValueNear(tracker, data, x, y, i, j, width, height);
        }

        private int FindValueNear(int[] tracker, int[] data, int x, int y, int i, int j, int width, int height)
        {
            Point p = new Point(i, j);
            int ox = this.EdgeSampling;
            int oy = this.EdgeSampling;
            int rw = width + this.EdgeSampling * 2;
            int rh = height + this.EdgeSampling * 2;

            if (p.Inside(width, height) && tracker[(i + ox) + (j + oy) * rw] != 0)
                return data[i + j * width];
            Random r = this.GetCellRNG(x + i, y + j);
            if (p.Left.Inside(width, height) && (p.Up.Inside(width, height) || r.Next(2) == 0))
                return this.FindValueNear(tracker, data, x, y, i - 1, j, width, height);
            else if (p.Up.Inside(width, height))
                return this.FindValueNear(tracker, data, x, y, i, j - 1, width, height);
            return this.FindValueNearOpposite(tracker, data, x, y, i, j, width, height);
        }

        private int FindValueNearOpposite(int[] tracker, int[] data, int x, int y, int i, int j, int width, int height)
        {
            Point p = new Point(i, j);
            int ox = this.EdgeSampling;
            int oy = this.EdgeSampling;
            int rw = width + this.EdgeSampling * 2;
            int rh = height + this.EdgeSampling * 2;

            if (p.Inside(width, height) && tracker[(i + ox) + (j + oy) * rw] != 0)
                return data[i + j * width];
            Random r = this.GetCellRNG(x + i, y + j);
            if (p.Right.Inside(width, height) && (p.Down.Inside(width, height) || r.Next(2) == 0))
                return this.FindValueNearOpposite(tracker, data, x, y, i + 1, j, width, height);
            else if (p.Down.Inside(width, height))
                return this.FindValueNearOpposite(tracker, data, x, y, i, j + 1, width, height);
            return 0;
        }

        private void RecursiveApply(int[] tracker, int[] voronoi, int idx, Point p, int width, int height)
        {
            int ox = this.EdgeSampling;
            int oy = this.EdgeSampling;
            int rw = width + this.EdgeSampling * 2;
            int rh = height + this.EdgeSampling * 2;

            if (p.Inside(-ox, -oy, rw, rh) && voronoi[(p.X + ox) + (p.Y + oy) * rw] != 2 /* edge */ && tracker[(p.X + ox) + (p.Y + oy) * rw] == 0 /* not hit */)
                tracker[(p.X + ox) + (p.Y + oy) * rw] = idx + MAPPING_OFFSET;
            else
                return;
            this.RecursiveApply(tracker, voronoi, idx, p.Left, width, height);
            this.RecursiveApply(tracker, voronoi, idx, p.Right, width, height);
            this.RecursiveApply(tracker, voronoi, idx, p.Up, width, height);
            this.RecursiveApply(tracker, voronoi, idx, p.Down, width, height);
        }

        public override Dictionary<int, System.Drawing.Brush> GetLayerColors()
        {
            if (this.Parents.Length < 2 || this.Parents[1] == null)
                return null;
            else
                return this.Parents[1].GetLayerColors();
        }

        public override string[] GetParentsRequired()
        {
            return new string[] { "Voronoi", "Parent" };
        }

        public override string ToString()
        {
            return "Voronoi Mixdown";
        }

        #region Point Data Structure

        private struct Point
        {
            public int X;
            public int Y;

            public int GetIdx(int width)
            {
                return this.X + this.Y * width;
            }

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public Point Left
            {
                get
                {
                    return new Point(this.X - 1, this.Y);
                }
            }

            public Point Right
            {
                get
                {
                    return new Point(this.X + 1, this.Y);
                }
            }

            public Point Up
            {
                get
                {
                    return new Point(this.X, this.Y - 1);
                }
            }

            public Point Down
            {
                get
                {
                    return new Point(this.X, this.Y + 1);
                }
            }

            public bool Inside(int width, int height)
            {
                return (this.X >= 0 && this.Y >= 0 &&
                        this.X < width && this.Y < height);
            }

            public bool Inside(int x, int y, int width, int height)
            {
                return (this.X >= x && this.Y >= y &&
                        this.X < x + width && this.Y < y + height);
            }

            public override string ToString()
            {
                return "{" + this.X + ", " + this.Y + "}";
            }
        }

        #endregion
    }
}
