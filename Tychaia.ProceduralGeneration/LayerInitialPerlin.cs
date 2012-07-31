using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using Protogame.Noise;

namespace Tychaia.ProceduralGeneration
{
    /// <summary>
    /// Generates a layer from perlin noise.
    /// </summary>
    [DataContract]
    public class LayerInitialPerlin : Layer
    {
        [DataMember]
        [DefaultValue(100)]
        [Description("The scale of the perlin noise map.")]
        public double Scale
        {
            get;
            set;
        }

        [DataMember]
        [DefaultValue(0)]
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
        [DefaultValue(100)]
        [Description("The maximum integer value in the resulting layer.")]
        public int MaxValue
        {
            get;
            set;
        }

        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public LayerInitialPerlin()
        {
        }

        public LayerInitialPerlin(int seed)
            : base(seed)
        {
            // Set defaults.
            this.Scale = 100;
            this.Modifier = new Random().Next();
            this.MinValue = 0;
            this.MaxValue = 100;
        }

        public override int[] GenerateData(int x, int y, int width, int height)
        {
            int[] data = new int[width * height];
            PerlinNoise perlin = new PerlinNoise(this.GetPerlinRNG());

            for (int a = 0; a < width; a++)
                for (int b = 0; b < height; b++)
                {
                    double noise = perlin.Noise((x + a) / this.Scale, (y + b) / this.Scale, 0) / 2.0 + 0.5;
                    data[a + b * width] = (int)((noise * (this.MaxValue - this.MinValue)) + this.MinValue);
                }

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
            return LayerColors.GetGradientBrushes(this.MinValue, this.MaxValue);
        }

        public override string[] GetParentsRequired()
        {
            return new string[] { };
        }

        public override string ToString()
        {
            return "Initial Perlin";
        }
    }
}
