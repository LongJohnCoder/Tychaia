using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;

namespace Tychaia.Generators.Biomes
{
    public class Biome
    {
        public const int MOUNTAINS_START = 0;
        public const int MOUNTAINS_END = 20;
        public const int HIGHLANDS_START = 20;
        public const int HIGHLANDS_END = 30;
        public const int MIDHEIGHT_START = 30;
        public const int MIDHEIGHT_END = 35;
        public const int VALLEY_START = 35;
        public const int VALLEY_END = 45;
        public const int DEPTHS_START = 45;
        public const int DEPTHS_END = Chunk.Depth;

        /*public byte MinHeight
        {
            get;
            protected set;
        }

        public byte AvgHeight
        {
            get;
            protected set;
        }

        public byte MaxHeight
        {
            get;
            protected set;
        }*/

        public float MinTemperature
        {
            get;
            protected set;
        }

        public float AvgTemperature
        {
            get;
            protected set;
        }

        public float MaxTemperature
        {
            get;
            protected set;
        }

        public float MinRainfall
        {
            get;
            protected set;
        }

        public float AvgRainfall
        {
            get;
            protected set;
        }

        public float MaxRainfall
        {
            get;
            protected set;
        }

        public Block GroundBlock
        {
            get;
            protected set;
        }

        public TreeType[] TreeTypes
        {
            get;
            protected set;
        }

        public float TreeSpawnChance
        {
            get;
            protected set;
        }

        public Block[] TreeSpawnOn
        {
            get;
            protected set;
        }

        public virtual void ReplaceGround(BiomeData data, Block[, ,] blocks)
        {
        }

        private bool HasRoom(TreeType tree, Block[, ,] blocks, int x, int y, int z)
        {
            for (int ax = -tree.MaxTreeSpan; ax <= tree.MaxTreeSpan; ax++)
                for (int ay = -tree.MaxTreeSpan; ay <= tree.MaxTreeSpan; ay++)
                    for (int az = -tree.MaxHeight; az <= 0; az++)
                        if (x + ax >= blocks.GetLowerBound(0) && x + ax <= blocks.GetUpperBound(0))
                            if (y + ay >= blocks.GetLowerBound(1) && y + ay <= blocks.GetUpperBound(1))
                                if (z + az >= blocks.GetLowerBound(2) && z + az <= blocks.GetUpperBound(2))
                                    if (blocks[x + ax, y + ay, z + az] != null && blocks[x + ax, y + ay, z + az].Impassable)
                                        return false;
            return true;
        }

        public virtual void InsertTrees(BiomeData data, Block[, ,] blocks)
        {
            if (this.TreeTypes == null || this.TreeTypes.Length == 0 || this.TreeSpawnOn == null || this.TreeSpawnOn.Length == 0)
                return;

            PerlinNoise perlin = new PerlinNoise(data.ChunkInfo.Seed + PerlinAllocations.TREES);

            for (int x = 0; x < blocks.GetLength(0); x++)
                for (int y = 0; y < blocks.GetLength(1); y++)
                    if (this.SuitableSampleAverage(data, x, y))
                        for (int z = 0; z < blocks.GetLength(2); z++)
                        {
                            // Determine tree type.
                            TreeType t = this.TreeTypes[data.ChunkInfo.Random.Next(0, this.TreeTypes.Length)];
                            if (this.TreeSpawnOn.Contains(blocks[x, y, z]) && this.HasRoom(t, blocks, x, y, z - 1))
                                if (data.ChunkInfo.Random.NextDouble() > 1 - this.TreeSpawnChance)
                                {
                                    // Determine tree size.
                                    int height = (int)data.ChunkInfo.Random.NextGuassianClamped(t.MinHeight, t.MaxHeight);
                                    int span = (int)data.ChunkInfo.Random.NextGuassianClamped(t.MinTreeSpan, t.MinTreeSpan);
                                    //Console.WriteLine("Generating tree with height " + height + " and span " + span + ".");

                                    // Generate trunk.
                                    if (t.TrunkBlock != null)
                                        for (int i = -1; i > -(height+1); i--)
                                            blocks[x, y, z + i] = t.TrunkBlock;

                                    // Generate treespan.
                                    if (t.LeafBlock != null)
                                        for (int ax = -span; ax <= span; ax++)
                                            for (int ay = -span; ay <= span; ay++)
                                                for (int az = -span / 2; az <= span / 2; az++)
                                                    if (x + ax >= blocks.GetLowerBound(0) && x + ax <= blocks.GetUpperBound(0))
                                                        if (y + ay >= blocks.GetLowerBound(1) && y + ay <= blocks.GetUpperBound(1))
                                                            if (z + az - height + span / 2 >= blocks.GetLowerBound(2) && z + az - height + span / 2 <= blocks.GetUpperBound(2))
                                                                if (blocks[x + ax, y + ay, z - height + az + span / 2] == null)
                                                                    if (Math.Pow(ax, 2) + Math.Pow(ay, 2) + Math.Pow(az, 2) < Math.Pow(span, 2))
                                                                        blocks[x + ax, y + ay, z - height + az + span / 2] = t.LeafBlock;
                                }
                        }

        }

        protected bool Suitable(BiomeData data, int x, int y)
        {
            return (//data.Heightmap.Heightmap[x, y] >= this.MinHeight && data.Heightmap.Heightmap[x, y] <= this.MaxHeight &&
                    data.Rainfall.Rainfall[x, y] >= this.MinRainfall && data.Rainfall.Rainfall[x, y] <= this.MaxRainfall &&
                    data.Temperature.Temperature[x, y] >= this.MinTemperature && data.Temperature.Temperature[x, y] <= this.MaxTemperature);
        }

        private const int SAMPLE_SIZE = 3;

        protected bool SuitableSampleAverage(BiomeData data, int x, int y)
        {
            int ox = data.Heightmap.Heightmap.GetLength(0) / 2;
            int oy = data.Heightmap.Heightmap.GetLength(1) / 2;
            float avgHeight = 0;
            float avgRainfall = 0;
            float avgTemperature = 0;
            int count = 0;
            for (int ax = -SAMPLE_SIZE; ax <= SAMPLE_SIZE; ax++)
                for (int ay = -SAMPLE_SIZE; ay <= SAMPLE_SIZE; ay++)
                {
                    avgHeight += data.Heightmap.NeighbouringHeightmap[x + ax + ox, y + ay + oy];
                    avgRainfall += data.Rainfall.NeighbouringRainfall[x + ax + ox, y + ay + oy];
                    avgTemperature += data.Temperature.NeighbouringTemperature[x + ax + ox, y + ay + oy];
                    count++;
                }
            avgHeight /= count;
            avgRainfall /= count;
            avgTemperature /= count;
            return (//avgHeight >= this.MinHeight && avgHeight <= this.MaxHeight &&
                    avgRainfall >= this.MinRainfall && avgRainfall <= this.MaxRainfall &&
                    avgTemperature >= this.MinTemperature && avgTemperature <= this.MaxTemperature);
        }
    }

    public class BiomeData
    {
        public ChunkInfo ChunkInfo;
        public HeightmapInformation Heightmap;
        public RainfallInformation Rainfall;
        public TemperatureInformation Temperature;
    }
}
