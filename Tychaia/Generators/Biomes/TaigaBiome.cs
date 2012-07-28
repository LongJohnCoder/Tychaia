using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;
using Tychaia.Generators.Biomes.TreeTypes;

namespace Tychaia.Generators.Biomes
{
    public class TaigaBiome : Biome
    {
        public TaigaBiome()
        {
            this.MinRainfall = 0.25f;
            this.MaxRainfall = 0.75f;
            this.MinTemperature = 0.25f;
            this.MaxTemperature = 0.50f;
            this.TreeTypes = new TreeType[] { new TaigaTreeType() };
            this.TreeSpawnOn = new Block[] { Block.SnowBlock };
            this.TreeSpawnChance = 0.6f;
        }

        public override void ReplaceGround(BiomeData bd, Block[, ,] blocks)
        {
            for (int x = 0; x < blocks.GetLength(0); x++)
                for (int y = 0; y < blocks.GetLength(1); y++)
                    if (this.SuitableSampleAverage(bd, x, y))
                        for (int z = 0; z < blocks.GetLength(2); z++)
                            if (blocks[x, y, z] == Block.GrassBlock)
                                blocks[x, y, z] = Block.SnowBlock;

            base.ReplaceGround(bd, blocks);
        }
    }
}
