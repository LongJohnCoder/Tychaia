using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;

namespace Tychaia.Generators
{
    public class RainfallGenerator : IGenerator
    {
        private const float PERLIN_COARSE_SCALE = 4f;//320f;
        private const float PERLIN_MEDIUM_SCALE = 160f;
        private const float PERLIN_FINE_SCALE = 80f;

        public static double ApplyModifier(double rainfall, double temperature)
        {
            // See the BiomeRainfallModifier.grf file for more detail on what's going on here.
            double x = temperature * 100.0;
            double y = x + 27.0 * Math.Sin((x + (12.5 * Math.Sin(x * Math.PI / 100.0))) * Math.PI / 100.0);
            return rainfall * (y / 100.0);
        }

        public void Generate(Block[,,] blocks, ChunkInfo info)
        {
            PerlinNoise coarse = new PerlinNoise(info.Seed + PerlinAllocations.RAINFALL_COARSE);
            PerlinNoise medium = new PerlinNoise(info.Seed + PerlinAllocations.RAINFALL_MEDIUM);
            PerlinNoise fine = new PerlinNoise(info.Seed + PerlinAllocations.RAINFALL_FINE);
            TemperatureInformation temperature = info.Objects.First(val => val is TemperatureInformation) as TemperatureInformation;
            RainfallInformation rainfall = new RainfallInformation();

            // Generate rainfall.
            int ox = info.Bounds.Width / 2;
            int oy = info.Bounds.Height / 2;
            for (int x = -info.Bounds.Width / 2; x < info.Bounds.Width * 1.5; x++)
                for (int y = -info.Bounds.Height / 2; y < info.Bounds.Height * 1.5; y++)
                {
                    double noise = coarse.Noise((double)(info.Bounds.X + x) / PERLIN_COARSE_SCALE,
                        (double)(info.Bounds.Y + y) / PERLIN_COARSE_SCALE,
                        0);
                    noise += PerlinNoise.OFFSET;
                    noise = RainfallGenerator.ApplyModifier(noise, temperature.NeighbouringTemperature[x + ox, y + oy]);
                    if (x >= 0 && y >= 0 && x < info.Bounds.Width && y < info.Bounds.Height)
                        rainfall.Rainfall[x, y] = (float)noise;
                    rainfall.NeighbouringRainfall[x + ox, y + oy] = (float)noise;
                }

            // Add to information list.
            info.Objects.Add(rainfall);
        }
    }

    public class RainfallInformation
    {
        public float[,] Rainfall = new float[Chunk.Width, Chunk.Height];
        public float[,] NeighbouringRainfall = new float[Chunk.Width * 2, Chunk.Height * 2];
    }
}
