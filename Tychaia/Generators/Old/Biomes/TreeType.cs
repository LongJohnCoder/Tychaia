using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tychaia.Generators.Biomes
{
    public class TreeType
    {
        public int MinHeight
        {
            get;
            protected set;
        }

        public int MaxHeight
        {
            get;
            protected set;
        }

        public int AvgHeight
        {
            get;
            protected set;
        }

        public int MinTreeSpan
        {
            get;
            protected set;
        }

        public int AvgTreeSpan
        {
            get;
            protected set;
        }

        public int MaxTreeSpan
        {
            get;
            protected set;
        }

        public Block LeafBlock
        {
            get;
            protected set;
        }

        public Block TrunkBlock
        {
            get;
            protected set;
        }
    }
}
