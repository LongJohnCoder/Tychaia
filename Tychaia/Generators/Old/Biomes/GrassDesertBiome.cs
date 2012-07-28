using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tychaia.Generators.Biomes.TreeTypes;

namespace Tychaia.Generators.Biomes
{
    public class GrassDesertBiome : Biome
    {
        public GrassDesertBiome()
        {
            this.MinRainfall = 0f;
            this.MaxRainfall = 0.25f;
            this.MinTemperature = 0.25f;
            this.MaxTemperature = 0.66f;
            this.TreeTypes = new TreeType[] { new GrassTreeType() };
            this.TreeSpawnOn = new Block[] { Block.SandGrassBlock };
            this.TreeSpawnChance = 0.1f;
        }

        public override void ReplaceGround(BiomeData bd, Block[, ,] blocks)
        {
            for (int x = 0; x < blocks.GetLength(0); x++)
                for (int y = 0; y < blocks.GetLength(1); y++)
                    if (this.SuitableSampleAverage(bd, x, y))
                        for (int z = 0; z < blocks.GetLength(2); z++)
                            if (blocks[x, y, z] == Block.GrassBlock)
                                blocks[x, y, z] = Block.SandGrassBlock;

            base.ReplaceGround(bd, blocks);
        }
    }
}
