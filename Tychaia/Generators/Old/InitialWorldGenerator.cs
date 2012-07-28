using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tychaia.Generators
{
#if false
    /// <summary>
    /// Initial base generation that just fills with water.
    /// </summary>
    class InitialWorldGenerator : IGenerator
    {
        public GeneratedBlock Generate(ChunkInfo info, int x, int y)
        {
            return new GeneratedBlock { Type = GeneratedBlock.BlockType.Water };
        }
    }
#endif
}
