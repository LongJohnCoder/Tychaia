using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tychaia.Generators.Biomes.TreeTypes
{
    public class TaigaTreeType : TreeType
    {
        public TaigaTreeType()
        {
            this.MinHeight = 4;
            this.AvgHeight = 5;
            this.MaxHeight = 7;
            this.MinTreeSpan = 3;
            this.AvgTreeSpan = 3;
            this.MaxTreeSpan = 5;
            this.LeafBlock = Block.LeafGreyBlock;
            this.TrunkBlock = Block.TrunkBlock;
        }
    }
}
