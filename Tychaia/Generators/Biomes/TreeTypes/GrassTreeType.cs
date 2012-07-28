using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tychaia.Generators.Biomes.TreeTypes
{
    public class GrassTreeType : TreeType
    {
        public GrassTreeType()
        {
            this.MinHeight = 1;
            this.AvgHeight = 1;
            this.MaxHeight = 1;
            this.MinTreeSpan = 0;
            this.AvgTreeSpan = 0;
            this.MaxTreeSpan = 0;
            this.TrunkBlock = Block.GrassLeafBlock;
        }
    }
}
