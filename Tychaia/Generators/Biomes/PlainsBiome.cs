using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;
using Tychaia.Generators.Biomes.TreeTypes;

namespace Tychaia.Generators.Biomes
{
    public class PlainsBiome : Biome
    {
        public PlainsBiome()
        {
            this.MinRainfall = 0.25f;
            this.MaxRainfall = 0.50f;
            this.MinTemperature = 0.75f;
            this.MaxTemperature = 1.00f;
            this.TreeTypes = new TreeType[] { new GrassTreeType() };
            this.TreeSpawnOn = new Block[] { Block.GrassBlock };
            this.TreeSpawnChance = 0.3f;
        }
    }
}
