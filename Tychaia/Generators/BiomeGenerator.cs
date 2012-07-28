using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;
using System.Reflection;
using Tychaia.Generators.Biomes;

namespace Tychaia.Generators
{
    public class BiomeGenerator : IGenerator
    {
        private static List<Biome> BiomeList = null;

        private void InitializeBiomes()
        {
            BiomeList = new List<Biome>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type t in a.GetTypes())
                    if (typeof(Biome).IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null)
                        BiomeList.Add(t.GetConstructor(Type.EmptyTypes).Invoke(null) as Biome);
        }

        private T GetAverageValue<T>(T[,] arr)
        {
            uint v = 0;
            for (int a = 0; a < arr.GetLength(0); a++)
                for (int b = 0; b < arr.GetLength(1); b++)
                    v += (dynamic)arr[a, b];
            return (T)(v / (dynamic)arr.Length);
        }

        public void Generate(Block[, ,] blocks, ChunkInfo info)
        {
            if (BiomeList == null)
                this.InitializeBiomes();

            // Get all information required to determine appropriate biome.
            HeightmapInformation heightmap = info.Objects.First(val => val is HeightmapInformation) as HeightmapInformation;
            RainfallInformation rainfall = info.Objects.First(val => val is RainfallInformation) as RainfallInformation;
            TemperatureInformation temperature = info.Objects.First(val => val is TemperatureInformation) as TemperatureInformation;

            // Determine appropriate biome for this chunk.
            byte heightAvg = this.GetAverageValue(heightmap.Heightmap);
            float rainAvg = this.GetAverageValue(rainfall.Rainfall);
            float tempAvg = this.GetAverageValue(temperature.Temperature);
            BiomeData bd = new BiomeData
            {
                ChunkInfo = info, 
                Heightmap = heightmap,
                Rainfall = rainfall,
                Temperature = temperature
            };
            foreach (Biome b in BiomeList)
            {
                //if (heightAvg >= b.MinHeight && heightAvg <= b.MaxHeight &&
                //    rainAvg >= b.MinRainfall && rainAvg <= b.MaxRainfall &&
                //    tempAvg >= b.MinTemperature && tempAvg <= b.MaxTemperature)
                //{
                    b.ReplaceGround(bd, blocks);
                    b.InsertTrees(bd, blocks);
                //}
            }

            // Add to information list.
            info.Objects.Add(temperature);
        }
    }
}
