using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;
using Tychaia.Tiles;

namespace Tychaia.Generators
{
    public class Block
    {
        public Tile Tile
        {
            get;
            private set;
        }

        public bool Impassable
        {
            get;
            private set;
        }

        public bool Transparent
        {
            get;
            private set;
        }

        private static WaterTile WaterTile = new WaterTile();
        private static GrassTile GrassTile = new GrassTile();
        private static SnowTile SnowTile = new SnowTile();
        private static LavaTile LavaTile = new LavaTile();
        private static DirtTile DirtTile = new DirtTile();
        private static StoneTile StoneTile = new StoneTile();
        private static TrunkTile TrunkTile = new TrunkTile();
        private static LeafTile LeafTile = new LeafTile();
        private static LeafGreyTile LeafGreyTile = new LeafGreyTile();
        private static SandTile SandTile = new SandTile();
        private static SandGrassTile SandGrassTile = new SandGrassTile();
        private static GrassLeafTile GrassLeafTile = new GrassLeafTile();

        public static Block WaterBlock = new Block { Tile = WaterTile, Impassable = true };
        public static Block GrassBlock = new Block { Tile = GrassTile, Impassable = false };
        public static Block SnowBlock = new Block { Tile = SnowTile, Impassable = false };
        public static Block LavaBlock = new Block { Tile = LavaTile, Impassable = true };
        public static Block DirtBlock = new Block { Tile = DirtTile, Impassable = false };
        public static Block StoneBlock = new Block { Tile = StoneTile, Impassable = false };
        public static Block TrunkBlock = new Block { Tile = TrunkTile, Impassable = false };
        public static Block LeafBlock = new Block { Tile = LeafTile, Impassable = false };
        public static Block LeafGreyBlock = new Block { Tile = LeafGreyTile, Impassable = false };
        public static Block SandBlock = new Block { Tile = SandTile, Impassable = false };
        public static Block SandGrassBlock = new Block { Tile = SandGrassTile, Impassable = false };
        public static Block GrassLeafBlock = new Block { Tile = GrassLeafTile, Impassable = false, Transparent = true };
    }
}
