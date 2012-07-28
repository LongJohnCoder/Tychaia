using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protogame.Noise
{
    public class OctaveNoise
    {
        private Random m_Random = null;
        private PerlinNoise[] m_Perlin = null;

        public OctaveNoise(int seed, int octaves)
        {
            this.m_Random = new Random(seed);
            this.m_Perlin = new PerlinNoise[octaves];
            for (int i = 0; i < octaves; i++)
                this.m_Perlin[i] = new PerlinNoise(this.m_Random);
        }

        public double Noise(double x, double y, double z)
        {
            double scale = 1;
            double value = 0;

            for (int i = 0; i < this.m_Perlin.Length; i++)
            {
                double scaledX = x * scale;
                double scaledY = y * scale;
                double scaledZ = z * scale;

                value += this.m_Perlin[i].Noise(scaledX, scaledY, scaledZ) * scale;
                scale /= 2;
            }

            return value;
        }

        public double Noise2D(double x, double y)
        {
            return this.Noise(x, y, 20);
        }
    }
}
