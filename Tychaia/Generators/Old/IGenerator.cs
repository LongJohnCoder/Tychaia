using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tychaia.Generators
{
    public interface IGenerator
    {
        void Generate(Block[,,] blocks, ChunkInfo info);
    }

    public static class PerlinAllocations
    {
        public const int HEIGHTMAP_COARSE = 0;
        public const int HEIGHTMAP_MEDIUM = 10;
        public const int HEIGHTMAP_FINE = 20;
        public const int RAINFALL_COARSE = 30;
        public const int RAINFALL_MEDIUM = 40;
        public const int RAINFALL_FINE = 50;
        public const int TEMPERATURE_COARSE = 60;
        public const int TEMPERATURE_MEDIUM = 70;
        public const int TEMPERATURE_FINE = 80;
        public const int TREES = 90;
    }
}
