using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;
using Tychaia.Generators.Biomes.TreeTypes;

namespace Tychaia.Generators.Biomes
{
    public class RainforestBiome : Biome
    {
        public RainforestBiome()
        {
            this.MinRainfall = 0.75f;
            this.MaxRainfall = 1.00f;
            this.MinTemperature = 0.75f;
            this.MaxTemperature = 1.00f;
            this.TreeTypes = new TreeType[] { new ForestTreeType() };
            this.TreeSpawnOn = new Block[] { Block.GrassBlock };
            this.TreeSpawnChance = 0.75f;
        }
    }
}
