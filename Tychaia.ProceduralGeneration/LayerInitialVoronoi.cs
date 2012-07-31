using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using Protogame.Noise;
using BenTools.Mathematics;

namespace Tychaia.ProceduralGeneration
{
    /// <summary>
    /// Generates a layer from Voronoi tessellation.
    /// </summary>
    [DataContract]
    public class LayerInitialVoronoi : Layer
    {
        [DataMember]
        [DefaultValue(100)]
        [Description("The value which determines the chance that a point value will occur on a cell.")]
        public int PointValue
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue(VoronoiResult.EdgesAndOriginals)]
        [Description("The type of data to return from the voronoi calculations.")]
        public VoronoiResult Result
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue(15)]
        [Description("The edge sampling range used in order to provide a consistant result across the infinite range.")]
        public int EdgeSampling
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue(100)]
        [Description("The seed modifier value to apply to this perlin map.")]
        public long Modifier
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue(0)]
        [Description("The minimum integer value in the resulting layer.")]
        public int MinValue
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue(0)]
        [Description("The maximum integer value in the resulting layer.")]
        public int MaxValue
        {
            get;
            set;
        }

        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public LayerInitialVoronoi()
        {
        }

        public LayerInitialVoronoi(int seed)
            : base(seed)
        {
            // Set defaults.
            this.PointValue = 100;
            this.EdgeSampling = 15;
            this.MinValue = 0;
            this.MaxValue = 100;
            this.Modifier = new Random().Next();
            this.Result = VoronoiResult.EdgesAndOriginals;
        }

        public override int[] GenerateData(int x, int y, int width, int height)
        {
            int[] data = new int[width * height];

            // Determine output values.
            int noneOutput = 0;
            int originalOutput = 1;
            int centerOutput = (this.Result == VoronoiResult.AllValues) ? 2 : 1;
            int edgeOutput = (this.Result == VoronoiResult.AllValues) ? 3 : (this.Result == VoronoiResult.EdgesAndOriginals) ? 2 : 1;

            // Scan through the size of the array, randomly creating points.
            List<Vector> points = new List<Vector>();
            for (int i = -this.EdgeSampling; i < width + this.EdgeSampling; i++)
                for (int j = -this.EdgeSampling; j < height + this.EdgeSampling; j++)
                {
                    Random r = this.GetCellRNG(x + i, y + j);
                    if (r.Next(this.PointValue) == 0)
                    {
                        points.Add(new Vector(new double[] { i, j }));
                        if (i >= 0 && i < width &&
                            j >= 0 && j < height)
                            if (this.Result == VoronoiResult.AllValues ||
                                this.Result == VoronoiResult.EdgesAndOriginals ||
                                this.Result == VoronoiResult.OriginalOnly)
                                data[i + j * width] = originalOutput;
                    }
                }

            // Skip computations if we are only outputting original scatter values.
            if (this.Result == VoronoiResult.OriginalOnly)
                return data;
            
            // Compute the Voronoi diagram.
            VoronoiGraph graph = Fortune.ComputeVoronoiGraph(points);

            // Output the edges if needed.
            if (this.Result == VoronoiResult.AllValues ||
                this.Result == VoronoiResult.EdgesAndOriginals ||
                this.Result == VoronoiResult.EdgeOnly)
            {
                foreach (VoronoiEdge v in graph.Edges)
                {
                    Vector a = v.VVertexA;
                    Vector b = v.VVertexB;

                    // Normalize vector between two points.
                    double cx = 0, cy = 0;
                    double sx = b[0] < a[0] ? b[0] : a[0];
                    double sy = b[0] < a[0] ? b[1] : a[1];
                    double mx = b[0] > a[0] ? b[0] : a[0];
                    double my = b[0] > a[0] ? b[1] : a[1];
                    double tx = b[0] > a[0] ? b[0] - a[0] : a[0] - b[0];
                    double ty = b[0] > a[0] ? b[1] - a[1] : a[1] - b[1];
                    double length = Math.Sqrt(Math.Pow(tx, 2) + Math.Pow(ty, 2));
                    tx /= length;
                    ty /= length;

                    // Iterate until we reach the target.
                    while (sx + cx < mx)// && sy + cy < my)
                    {
                        if ((int)(sx + cx) >= 0 && (int)(sx + cx) < width &&
                            (int)(sy + cy) >= 0 && (int)(sy + cy) < height &&
                            data[(int)(sx + cx) + (int)(sy + cy) * width] == noneOutput)
                            data[(int)(sx + cx) + (int)(sy + cy) * width] = edgeOutput;

                        cx += tx; // b[0] > a[0] ? tx : -tx;
                        cy += ty;// b[1] > a[1] ? ty : -ty;
                    }
                }
            }

            // Output the center points if needed.
            if (this.Result == VoronoiResult.AllValues ||
                this.Result == VoronoiResult.CenterOnly)
            {
                foreach (Vector v in graph.Vertizes)
                {
                    if ((int)v[0] >= 0 && (int)v[0] < width &&
                        (int)v[1] >= 0 && (int)v[1] < height)
                        data[(int)v[0] + (int)v[1] * width] = centerOutput;
                }
            }

            // Return the result.
            return data;
        }

        private int GetPerlinRNG()
        {
            long seed = this.Seed;
            seed += this.Modifier;
            seed *= this.Modifier;
            seed += this.Modifier;
            seed *= this.Modifier;
            seed += this.Modifier;
            seed *= this.Modifier;
            return (int)seed;
        }

        public override Dictionary<int, System.Drawing.Brush> GetLayerColors()
        {
            return LayerColors.VoronoiBrushes;
        }

        public override string[] GetParentsRequired()
        {
            return new string[] { };
        }

        public override string ToString()
        {
            return "Initial Voronoi";
        }
    }

    public enum VoronoiResult
    {
        AllValues,
        EdgesAndOriginals,
        OriginalOnly,
        CenterOnly,
        EdgeOnly,
    }
}
