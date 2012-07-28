using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tychaia.Generators.Biomes
{
    public class DesertBiome : Biome
    {
        public DesertBiome()
        {
            this.MinRainfall = 0f;
            this.MaxRainfall = 0.25f;
            this.MinTemperature = 0.66f;
            this.MaxTemperature = 1f;
        }

        public override void ReplaceGround(BiomeData bd, Block[, ,] blocks)
        {
            for (int x = 0; x < blocks.GetLength(0); x++)
                for (int y = 0; y < blocks.GetLength(1); y++)
                    if (this.SuitableSampleAverage(bd, x, y))
                        for (int z = 0; z < blocks.GetLength(2); z++)
                            if (blocks[x, y, z] == Block.GrassBlock || blocks[x, y, z] == Block.DirtBlock)
                                blocks[x, y, z] = Block.SandBlock;

            base.ReplaceGround(bd, blocks);
        }
    }
}
