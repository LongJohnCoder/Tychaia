using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tychaia.Generators;

namespace Tychaia.Elements
{
    public class Region
    {
        public const int REGION_SIZE = 16 * 10000;

        /// <summary>
        /// The chunks contained within this region.  References either the chunk or
        /// null depending on whether the chunk has been loaded into memory.
        /// </summary>
        private Chunk[,] m_Chunks = new Chunk[REGION_SIZE / 16, REGION_SIZE / 16];

        /// <summary>
        /// The neighbouring regions.
        /// </summary>
        private Region[] m_Neighbours = new Region[Direction.EIGHT_WAY];

        /// <summary>
        /// The region's name.  Randomly generated when the region is first created.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

    }
}
