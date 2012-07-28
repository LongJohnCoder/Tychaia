using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tychaia.Generators;
using Microsoft.Xna.Framework;
using Protogame;

namespace Tychaia.Lighting
{
    public class FieldOfView
    {
        private RPGWorld m_World = null;
        private ChunkTileset m_Tileset = null;
        public int[,] m_BroadphaseHeightGrid;
        public float[,] m_BroadphaseAngleGrid;
        public const int BROADPHASE_CELL = 1;

        public FieldOfView(RPGWorld world)
        {
            this.m_World = world;
            this.m_Tileset = this.m_World.Tileset as ChunkTileset;
            this.m_BroadphaseHeightGrid = new int[Chunk.Width / BROADPHASE_CELL, Chunk.Height / BROADPHASE_CELL];
            this.m_BroadphaseAngleGrid = new float[Chunk.Width / BROADPHASE_CELL, Chunk.Height / BROADPHASE_CELL];
            this.BroadphaseGrid = new bool[Chunk.Width / BROADPHASE_CELL, Chunk.Height / BROADPHASE_CELL];
        }

        public void Recalculate()
        {
            this.PerformBroadphase();
        }

        public bool[,] BroadphaseGrid
        {
            get;
            private set;
        }

        public RPGWorld World
        {
            get
            {
                return this.m_World;
            }
        }

        private void PerformBroadphase()
        {
            Point center = this.m_World.GetCenter();

            // Set up patch grid.
            for (int x = 0; x < this.m_BroadphaseHeightGrid.GetLength(0); x++)
                for (int y = 0; y < this.m_BroadphaseHeightGrid.GetLength(1); y++)
                {
                    Vector2 ray = new Vector2(center.X, center.Y) - new Vector2(x, y);
                    float rdir = (float)Math.Atan2(ray.Y, ray.X);

                    this.m_BroadphaseHeightGrid[x, y] = this.SamplePatch(x, y);
                    this.m_BroadphaseAngleGrid[x, y] = rdir;
                    this.BroadphaseGrid[x, y] = false;
                }

            // Start at the patch that will contain the player.
            bool[,] visited = new bool[Chunk.Width / BROADPHASE_CELL, Chunk.Height / BROADPHASE_CELL];
            List<BroadphasePoolVector> pool = new List<BroadphasePoolVector>() { new BroadphasePoolVector(center.X, center.Y, MathHelper.ToRadians(-90)) };

            this.PooledBroadphase(this.m_BroadphaseHeightGrid, visited, pool, center.X, center.Y);

            //this.RecursiveBroadphase(this.m_BroadphaseHeightGrid, visited, MathHelper.ToRadians(-90), cx, cy, cx, cy);
        }

        private static readonly Vector2[] NeighbouringPoints = new Vector2[] {
            //new Vector2(-1, -1),
            new Vector2(-1, 0),
            //new Vector2(-1, 1),
            new Vector2(0, -1),
            new Vector2(0, 1),
            //new Vector2(1, -1),
            new Vector2(1, 0)//,
            //new Vector2(1, 1)
        };

        private struct BroadphasePoolVector
        {
            public int X;
            public int Y;
            public float ArrivingAngle;

            public BroadphasePoolVector(int x, int y, float angle)
            {
                this.X = x;
                this.Y = y;
                this.ArrivingAngle = angle;
            }

            public override string ToString()
            {
                return "{" + this.X + "," + this.Y + ":" + this.ArrivingAngle + "}";
            }
        }

        private void PooledBroadphase(int[,] heightmap, bool[,] visited, List<BroadphasePoolVector> pool, int cx, int cy)
        {
            int c = 0;
            while (pool.Count > 0)
            {
                BroadphasePoolVector[] arr = pool.ToArray();
                pool.Clear();
                //if (c == 0 && arr.Length != 1)
                //    throw new InvalidOperationException("Lighting calculation did not pool broadphase stage of calculations correctly.");
                //else if (c != 0 && c * 8 != arr.Length)
                //    throw new InvalidOperationException("Lighting calculation did not pool broadphase stage of calculations correctly.");
                foreach (BroadphasePoolVector v in arr)
                {
                    Vector2 p = new Vector2(v.X, v.Y);
                    int a = heightmap[v.X, v.Y];
                    foreach (Vector2 n in FieldOfView.NeighbouringPoints)
                    {
                        if ((int)(n + p).X < 0 ||
                            (int)(n + p).X >= visited.GetLength(0) ||
                            (int)(n + p).Y < 0 ||
                            (int)(n + p).Y >= visited.GetLength(1))
                            continue;
                        if (visited[(int)(n + p).X, (int)(n + p).Y])
                            continue;
                        visited[(int)(n + p).X, (int)(n + p).Y] = true;
                        
                        // Calculate whether this neighbour point would direct us more
                        // than 45 degrees away from our shoot ray.
                        double pdir = Math.Atan2(n.Y, n.X);
                        double rdir = this.m_BroadphaseAngleGrid[(int)p.X, (int)p.Y];
                        float diff = MathHelper.WrapAngle((float)rdir - (float)pdir);
                        if (Math.Abs(diff) > MathHelper.ToDegrees(10))
                            continue;

                        // Calculate whether or not this block flattens out after an increase.
                        Vector2 rel = new Vector2(cx, cy) - n;
                        int b = heightmap[(int)(n + p).X, (int)(n + p).Y];
                        double angle = Math.Atan((b - a) / Math.Abs(n.Length()));
                        if (angle < 0 || angle >= v.ArrivingAngle)
                        {
                            this.BroadphaseGrid[v.X, v.Y] = true;
                            pool.Add(new BroadphasePoolVector((int)(n + p).X, (int)(n + p).Y, (float)angle));
                            //this.RecursiveBroadphase(heightmap, visited, angle, (int)(n + p).X, (int)(n + p).Y, cx, cy);
                        }
                    }
                }
                c += 1;
            }
        }

        private int SamplePatch(int x, int y)
        {
            int i = 0;
            int c = 0;
            for (int a = 0; a < BROADPHASE_CELL; a++)
                for (int b = 0; b < BROADPHASE_CELL; b++)
                {
                    i += this.GetHeightAtPoint(x + a, y + b);
                    c += 1;
                }
            return (int)((float)i / (float)c);
        }

        private int GetHeightAtPoint(int x, int y)
        {
            Chunk c = this.m_World.TilesetDynamicLoader.LocateAtPoint(
                this.m_Tileset.TargetX + x,
                this.m_Tileset.TargetY + y);
            for (int i = 0; i < 64; i++)
                if (c.m_Blocks[this.m_Tileset.TargetX + x - c.GlobalX, this.m_Tileset.TargetY + y - c.GlobalY, i] != null)
                    return i - 1;
            return 0;
        }
    }
}
