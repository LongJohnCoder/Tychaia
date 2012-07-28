using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;
using Tychaia.Generators.Biomes.TreeTypes;

namespace Tychaia.Generators.Biomes
{
    public class ForestBiome : Biome
    {
        public ForestBiome()
        {
            this.MinRainfall = 0.5f;
            this.MaxRainfall = 0.75f;
            this.MinTemperature = 0.5f;
            this.MaxTemperature = 0.75f;
            this.TreeTypes = new TreeType[] { new ForestTreeType() };
            this.TreeSpawnOn = new Block[] { Block.GrassBlock };
            this.TreeSpawnChance = 0.4f;
        }
    }
}
