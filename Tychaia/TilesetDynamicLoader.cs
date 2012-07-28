using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tychaia.Generators;
using Protogame;

namespace Tychaia
{
    public class TilesetDynamicLoader
    {
        private Chunk m_ZerothChunk = null;
        private HashSet<IPositionable> m_Uniques = null;

        public TilesetDynamicLoader(Chunk zerothChunk, HashSet<IPositionable> uniques)
        {
            this.m_Uniques = uniques;
            if (zerothChunk == null)
                zerothChunk = new Chunk(this.m_Uniques, 0, 0);
            this.m_ZerothChunk = zerothChunk;
        }

        public Chunk ZerothChunk
        {
            get
            {
                return this.m_ZerothChunk;
            }
        }

        public Chunk LocateAtPoint(int targetX, int targetY)
        {
            int x = 0;
            int y = 0;
            Chunk c = this.m_ZerothChunk;
            while (targetX < x && targetX <= x + Chunk.Width)
            {
                x -= Chunk.Width;
                c = c.Left;
            }
            while (targetX > x && targetX >= x + Chunk.Width)
            {
                x += Chunk.Width;
                c = c.Right;
            }
            while (targetY < y && targetY <= y + Chunk.Height)
            {
                y -= Chunk.Height;
                c = c.Up;
            }
            while (targetY > y && targetY >= y + Chunk.Height)
            {
                y += Chunk.Height;
                c = c.Down;
            }
            return c;
        }

#if false
        private void CopyInto(Tileset set, Chunk chunk, int activeX, int activeY)
        {
            int startX = Math.Max(0, activeX - chunk.GlobalX);
            int startY = Math.Max(0, activeY - chunk.GlobalY);
            int endX = Math.Max(Chunk.Width, activeX - chunk.GlobalX + Chunk.Width);
            int endY = Math.Max(Chunk.Height, activeY - chunk.GlobalY + Chunk.Height);

            for (int x = startX; x < endX; x++)
                for (int y = startY; y < endY; y++)
                {
                    GeneratedBlock b = chunk.GetBlockAt(x, y);
                    if (b != null)
                        set[x, y, 0] = b.Tile;
                }
        }

        public void LoadInto(Tileset activeSet, int activeX, int activeY)
        {
            Chunk tlChunk = this.LocateAtPoint(activeX, activeY);
            Chunk trChunk = this.LocateAtPoint(activeX + Chunk.Width, activeY);
            Chunk blChunk = this.LocateAtPoint(activeX, activeY + Chunk.Height);
            Chunk brChunk = this.LocateAtPoint(activeX + Chunk.Width, activeY + Chunk.Height);
            
            // Reset the active tileset.
            /*for (int x = 0; x < Tileset.TILESET_WIDTH; x++)
                for (int y = 0; y < Tileset.TILESET_HEIGHT; y++)
                    for (int z = 0; z < Tileset.TILESET_DEPTH; z++)
                        activeSet[x, y, z] = null;*/

            // We have each chunk.  We need to copy the data into the tileset.
            this.CopyInto(activeSet, tlChunk, activeX, activeY);
            this.CopyInto(activeSet, trChunk, activeX, activeY);
            this.CopyInto(activeSet, blChunk, activeX, activeY);
            this.CopyInto(activeSet, brChunk, activeX, activeY);
        }
#endif

        public void Regenerate(Tileset activeSet, int activeX, int activeY)
        {
            this.m_ZerothChunk = new Chunk(this.m_Uniques, 0, 0);
            //this.LoadInto(activeSet, activeX, activeY);
        }
    }
}
