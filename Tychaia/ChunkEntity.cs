using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;
using Microsoft.Xna.Framework;

namespace Tychaia
{
    public class ChunkEntity : Entity
    {
        private World m_World = null;

        protected ChunkEntity(World world)
        {
            this.m_World = world;
            base.X = 0;
            base.Y = 0;
        }

        public int WorldX
        {
            get
            {
                return (int)base.X / Tileset.TILESET_CELL_WIDTH;
            }
        }

        public int WorldY
        {
            get
            {
                return (int)base.Y / Tileset.TILESET_CELL_HEIGHT;
            }
        }

        public int WorldZ
        {
            get;
            set;
        }

        // TODO: Use interpolation for smooth movement!

        public override float X
        {
            get
            {
                Point p = (this.m_World.Tileset as ChunkTileset).GetEntityAdjustmentPoint();
                return base.X + p.X;
            }
            set
            {
                Point p = (this.m_World.Tileset as ChunkTileset).GetEntityAdjustmentPoint();
                base.X = value - p.X;
            }
        }

        public override float Y
        {
            get
            {
                Point p = (this.m_World.Tileset as ChunkTileset).GetEntityAdjustmentPoint();
                return base.Y + p.Y;
            }
            set
            {
                Point p = (this.m_World.Tileset as ChunkTileset).GetEntityAdjustmentPoint();
                base.Y = value - p.Y;
            }
        }
    }
}
