using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;
using Tychaia.Generators.Biomes.TreeTypes;

namespace Tychaia.Generators.Biomes
{
    public class SwampBiome : Biome
    {
        public SwampBiome()
        {
            this.MinRainfall = 0.75f;
            this.MaxRainfall = 1.00f;
            this.MinTemperature = 0.50f;
            this.MaxTemperature = 0.75f;
        }

        public override void ReplaceGround(BiomeData bd, Block[, ,] blocks)
        {
            for (int x = 0; x < blocks.GetLength(0); x++)
                for (int y = 0; y < blocks.GetLength(1); y++)
                    if (this.SuitableSampleAverage(bd, x, y))
                        for (int z = 0; z < blocks.GetLength(2); z++)
                            if (blocks[x, y, z] == Block.GrassBlock || blocks[x, y, z] == Block.DirtBlock)
                                blocks[x, y, z] = Block.WaterBlock;

            base.ReplaceGround(bd, blocks);
        }
    }
}
