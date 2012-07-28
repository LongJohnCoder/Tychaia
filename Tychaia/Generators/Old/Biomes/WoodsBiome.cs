using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;
using Tychaia.Generators.Biomes.TreeTypes;

namespace Tychaia.Generators.Biomes
{
    public class WoodsBiome : Biome
    {
        public WoodsBiome()
        {
            this.MinRainfall = 0.25f;
            this.MaxRainfall = 0.50f;
            this.MinTemperature = 0.50f;
            this.MaxTemperature = 0.75f;
            this.TreeTypes = new TreeType[] { new GrassTreeType(), new ForestTreeType() };
            this.TreeSpawnOn = new Block[] { Block.GrassBlock };
            this.TreeSpawnChance = 0.4f;
        }
    }
}
