using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;

namespace Tychaia.Generators
{
    public class TemperatureGenerator : IGenerator
    {
        private const float PERLIN_COARSE_SCALE = 4f;//320f;
        private const float PERLIN_MEDIUM_SCALE = 160f;
        private const float PERLIN_FINE_SCALE = 80f;

        public void Generate(Block[,,] blocks, ChunkInfo info)
        {
            PerlinNoise coarse = new PerlinNoise(info.Seed + PerlinAllocations.TEMPERATURE_COARSE);
            PerlinNoise medium = new PerlinNoise(info.Seed + PerlinAllocations.TEMPERATURE_MEDIUM);
            PerlinNoise fine = new PerlinNoise(info.Seed + PerlinAllocations.TEMPERATURE_FINE);
            TemperatureInformation temperature = new TemperatureInformation();

            // Generate temperature.
            int ox = info.Bounds.Width / 2;
            int oy = info.Bounds.Height / 2;
            for (int x = -info.Bounds.Width / 2; x < info.Bounds.Width * 1.5; x++)
                for (int y = -info.Bounds.Height / 2; y < info.Bounds.Height * 1.5; y++)
                {
                    double noise = coarse.Noise((double)(info.Bounds.X + x) / PERLIN_COARSE_SCALE,
                        (double)(info.Bounds.Y + y) / PERLIN_COARSE_SCALE,
                        0);
                    noise += PerlinNoise.OFFSET;
                    if (x >= 0 && y >= 0 && x < info.Bounds.Width && y < info.Bounds.Height)
                        temperature.Temperature[x, y] = (float)noise;
                    temperature.NeighbouringTemperature[x + ox, y + oy] = (float)noise;
                }

            // Add to information list.
            info.Objects.Add(temperature);
        }
    }

    public class TemperatureInformation
    {
        public float[,] Temperature = new float[Chunk.Width, Chunk.Height];
        public float[,] NeighbouringTemperature = new float[Chunk.Width * 2, Chunk.Height * 2];
    }
}
