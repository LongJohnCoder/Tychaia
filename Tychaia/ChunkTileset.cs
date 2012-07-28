using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;
using Tychaia.Generators;
using Microsoft.Xna.Framework;

namespace Tychaia
{
    public class ChunkTileset : Tileset
    {
        private TilesetDynamicLoader m_DynamicLoader = null;
        private Chunk m_ActiveTopLeftChunk = null;

        public ChunkTileset(TilesetDynamicLoader loader)
        {
            this.m_DynamicLoader = loader;
            this.RefreshChunkReference(0, 0);
        }

        public void RefreshChunkReference(int x, int y)
        {
            this.TargetX = x / Tileset.TILESET_CELL_WIDTH;
            this.TargetY = y / Tileset.TILESET_CELL_HEIGHT;
            this.m_ActiveTopLeftChunk = this.m_DynamicLoader.LocateAtPoint(x / Tileset.TILESET_CELL_WIDTH, y / Tileset.TILESET_CELL_HEIGHT);
        }

        public Point GetEntityAdjustmentPoint()
        {
            return new Point(-this.TargetX * Tileset.TILESET_CELL_WIDTH, -this.TargetY * Tileset.TILESET_CELL_HEIGHT);
        }

        public int TargetX;
        public int TargetY;

        public Block GetBlock(int x, int y, int z)
        {
            Chunk c = null;
            float rx = (x + this.TargetX) * Tileset.TILESET_CELL_WIDTH; // in px
            float ry = (y + this.TargetY) * Tileset.TILESET_CELL_HEIGHT;
            float chunkWidth = Chunk.Width * Tileset.TILESET_CELL_WIDTH; // in px
            float chunkHeight = Chunk.Height * Tileset.TILESET_CELL_HEIGHT;
            float gX = this.m_ActiveTopLeftChunk.GlobalX * Tileset.TILESET_CELL_WIDTH; // in px
            float gY = this.m_ActiveTopLeftChunk.GlobalY * Tileset.TILESET_CELL_HEIGHT;
            if (rx >= gX && rx < gX + chunkWidth &&
                ry >= gY && ry < gY + chunkHeight)
                c = this.m_ActiveTopLeftChunk;
            else if (rx >= gX + chunkWidth && rx < gX + chunkWidth * 2 &&
                ry >= gY && ry < gY + chunkHeight)
                c = this.m_ActiveTopLeftChunk.Right;
            else if (rx >= gX && rx < gX + chunkWidth &&
                ry >= gY + chunkHeight && ry < gY + chunkHeight * 2)
                c = this.m_ActiveTopLeftChunk.Down;
            else if (rx >= gX + chunkWidth && rx < gX + chunkWidth * 2 &&
                ry >= gY + chunkHeight && ry < gY + chunkHeight * 2)
                c = this.m_ActiveTopLeftChunk.Right.Down;
            else
                return null;
            if (z < 0 || z >= c.m_Blocks.GetLength(2))
                return null;
            return c.m_Blocks[
                (int)(rx / Tileset.TILESET_CELL_WIDTH - c.GlobalX),
                (int)(ry / Tileset.TILESET_CELL_HEIGHT - c.GlobalY),
                z];
        }

        public override Tile this[int x, int y, int z]
        {
            get
            {
                Block b = this.GetBlock(x, y, z);
                if (b == null) return null;
                return b.Tile;
            }
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}
