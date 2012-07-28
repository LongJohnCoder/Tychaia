using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;

namespace Tychaia.Generators
{
    public class WorldGenerator : IGenerator
    {
        private static readonly Dictionary<HeightInfo, Block> HeightMapping =
            new Dictionary<HeightInfo, Block>
            {
                { new HeightInfo(0, 1, 0), Block.GrassBlock },
                { new HeightInfo(1, 5, 2), Block.DirtBlock },
                { new HeightInfo(5, 255, 0), Block.StoneBlock }
            };
        private const byte WaterLevel = 32;
        private const double OCTAVE_SCALE = 80f;
        //private const float PERLIN_COARSE_SCALE = 160f;
        //private const float PERLIN_MEDIUM_SCALE = 80f;
        //private const float PERLIN_FINE_SCALE = 40f;

        private struct HeightInfo
        {
            public int Start;
            public int End;
            public int Varience;

            public HeightInfo(int start, int end, int varience)
            {
                this.Start = start;
                this.End = end;
                this.Varience = varience;
            }
        }

        private Block FindBlockForDepthLevel(ChunkInfo info, int level)
        {
            foreach (var v in HeightMapping)
            {
                if (level >= v.Key.Start && level < v.Key.End)// + (info.Random.NextGuassian(0, 1) * v.Key.Varience))
                    return v.Value;
            }
            return null;
        }

        public void Generate(Block[,,] blocks, ChunkInfo info)
        {
            //PerlinNoise coarse = new PerlinNoise(info.Seed + PerlinAllocations.HEIGHTMAP_COARSE);
            //PerlinNoise medium = new PerlinNoise(info.Seed + PerlinAllocations.HEIGHTMAP_MEDIUM);
            //PerlinNoise fine = new PerlinNoise(info.Seed + PerlinAllocations.HEIGHTMAP_FINE);
            OctaveNoise octave1 = new OctaveNoise(info.Seed + PerlinAllocations.HEIGHTMAP_COARSE, 16);
            OctaveNoise octave2 = new OctaveNoise(info.Seed + PerlinAllocations.HEIGHTMAP_COARSE, 16);
            OctaveNoise octave3 = new OctaveNoise(info.Seed + PerlinAllocations.HEIGHTMAP_COARSE, 8);
            OctaveNoise octave4 = new OctaveNoise(info.Seed + PerlinAllocations.HEIGHTMAP_COARSE, 4);
            OctaveNoise octave5 = new OctaveNoise(info.Seed + PerlinAllocations.HEIGHTMAP_COARSE, 10);
            OctaveNoise octave6 = new OctaveNoise(info.Seed + PerlinAllocations.HEIGHTMAP_COARSE, 16);
            HeightmapInformation heightmap = new HeightmapInformation();

            // Z INDEX 0 IS TOP OF LEVEL!

            // Generate ground surface.
            int ox = info.Bounds.Width / 2;
            int oy = info.Bounds.Height / 2;
            for (int x = -info.Bounds.Width / 2; x < info.Bounds.Width * 1.5; x++)
                for (int y = -info.Bounds.Height / 2; y < info.Bounds.Height * 1.5; y++)
                {
                    /*double noise = coarse.Noise((double)(info.Bounds.X + x) / PERLIN_COARSE_SCALE,
                        (double)(info.Bounds.Y + y) / PERLIN_COARSE_SCALE,
                        0) / 2;
                    noise += medium.Noise((double)(info.Bounds.X + x) / PERLIN_MEDIUM_SCALE,
                        (double)(info.Bounds.Y + y) / PERLIN_MEDIUM_SCALE,
                        0) / 4;
                    noise += fine.Noise((double)(info.Bounds.X + x) / PERLIN_FINE_SCALE,
                        (double)(info.Bounds.Y + y) / PERLIN_FINE_SCALE,
                        0) / 8;
                    noise += PerlinNoise.OFFSET;*/
                    double ex = info.Bounds.X + x;
                    double ey = info.Bounds.Y + y;
                    double noise = octave1.Noise(
                        ex / OCTAVE_SCALE,
                        ey / OCTAVE_SCALE,
                        10);
                    if (x >= 0 && y >= 0 && x < info.Bounds.Width && y < info.Bounds.Height)
                    {
                        heightmap.Heightmap[x, y] = (byte)(((noise + 1) * 128) / 4);

                        // Fill this pillar with blocks.
                        for (byte z = heightmap.Heightmap[x, y]; z < Chunk.Depth; z++)
                            blocks[x, y, z] = this.FindBlockForDepthLevel(info, z - heightmap.Heightmap[x, y]);
                    }

                    heightmap.NeighbouringHeightmap[x + ox, y + oy] = (byte)(((noise + 1) * 128) / 4);

                    // Fill in water.
                    //for (byte z = WaterLevel; z < Chunk.Depth; z++)
                    //    if (blocks[x, y, z] == null)
                    //        blocks[x, y, z] = Block.WaterBlock;
                }

            // Add to information list.
            info.Objects.Add(heightmap);
        }
    }

    public class HeightmapInformation
    {
        public byte[,] Heightmap = new byte[Chunk.Width, Chunk.Height];
        public byte[,] NeighbouringHeightmap = new byte[Chunk.Width * 2, Chunk.Height * 2];
    }
}
