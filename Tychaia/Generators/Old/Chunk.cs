using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;
using System.Threading;
using Microsoft.Xna.Framework;
using Tychaia.Title;

namespace Tychaia.Generators
{
    public class Chunk
    {
        public const int CHUNK_SIZE = 16;

        public const int Width = 16;
        public const int Height = 16;
        public const int Depth = 256;

        public Block[,,] m_Blocks = null;
        private object m_BlocksLock = new object();
        private HashSet<IPositionable> m_Uniques = null;
        private IGenerator[] m_Generators = null;
        private int m_Seed = TitleWorld.m_StaticSeed; // All chunks are generated from the same seed.
        private Chunk m_Left = null;
        private Chunk m_Right = null;
        private Chunk m_Up = null;
        private Chunk m_Down = null;

        public Chunk(HashSet<IPositionable> uniques, int x, int y)
        {
            this.m_Uniques = uniques;
            this.GlobalX = x;
            this.GlobalY = y;
            this.m_Generators = new IGenerator[]
            {
                new WorldGenerator(),
                //new TemperatureGenerator(),
                //new RainfallGenerator(),
                //new StateValidationGenerator(),
                //new BiomeGenerator()
            };
            this.m_Blocks = new Block[Chunk.Width, Chunk.Height, Chunk.Depth];
            this.Generate();
        }

        public int GlobalX;
        public int GlobalY;

        public Chunk Left
        {
            get
            {
                if (this.m_Left == null)
                {
                    if (this.m_Up != null && this.m_Up.m_Left != null && this.m_Up.m_Left.m_Down != null)
                        this.m_Left = this.m_Up.m_Left.m_Down;
                    else if (this.m_Down != null && this.m_Down.m_Left != null && this.m_Down.m_Left.m_Up != null)
                        this.m_Left = this.m_Down.m_Left.m_Up;
                    else
                        this.m_Left = new Chunk(this.m_Uniques, this.GlobalX - Chunk.Width, this.GlobalY);
                    this.m_Left.m_Right = this;
                }
                return this.m_Left;
            }
        }

        public Chunk Right
        {
            get
            {
                if (this.m_Right == null)
                {
                    if (this.m_Up != null && this.m_Up.m_Right != null && this.m_Up.m_Right.m_Down != null)
                        this.m_Right = this.m_Up.m_Right.m_Down;
                    else if (this.m_Down != null && this.m_Down.m_Right != null && this.m_Down.m_Right.m_Up != null)
                        this.m_Right = this.m_Down.m_Right.m_Up;
                    else
                        this.m_Right = new Chunk(this.m_Uniques, this.GlobalX + Chunk.Width, this.GlobalY);
                    this.m_Right.m_Left = this;
                }
                return this.m_Right;
            }
        }

        public Chunk Up
        {
            get
            {
                if (this.m_Up == null)
                {
                    if (this.m_Left != null && this.m_Left.m_Up != null && this.m_Left.m_Up.m_Right != null)
                        this.m_Up = this.m_Left.m_Up.m_Right;
                    else if (this.m_Right != null && this.m_Right.m_Up != null && this.m_Right.m_Up.m_Left != null)
                        this.m_Up = this.m_Right.m_Up.m_Left;
                    else
                        this.m_Up = new Chunk(this.m_Uniques, this.GlobalX, this.GlobalY - Chunk.Height);
                    this.m_Up.m_Down = this;
                }
                return this.m_Up;
            }
        }

        public Chunk Down
        {
            get
            {
                if (this.m_Down == null)
                {
                    if (this.m_Left != null && this.m_Left.m_Down != null && this.m_Left.m_Down.m_Right != null)
                        this.m_Down = this.m_Left.m_Down.m_Right;
                    else if (this.m_Right != null && this.m_Right.m_Down != null && this.m_Right.m_Down.m_Left != null)
                        this.m_Down = this.m_Right.m_Down.m_Left;
                    else
                        this.m_Down = new Chunk(this.m_Uniques, this.GlobalX, this.GlobalY + Chunk.Height);
                    this.m_Down.m_Up = this;
                }
                return this.m_Down;
            }
        }

        private void Generate()
        {
            Thread t = new Thread(() =>
            {
                ChunkInfo i = new ChunkInfo(this.m_Uniques)
                {
                    Seed = this.m_Seed,
                    Random = new Random(this.m_Seed),
                    Bounds = new Rectangle(this.GlobalX, this.GlobalY, Chunk.Width, Chunk.Height)
                };
                foreach (IGenerator g in this.m_Generators)
                    g.Generate(this.m_Blocks, i);
                /*
                for (int x = 0; x < Chunk.Width; x++)
                    for (int y = 0; y < Chunk.Height; y++)
                    {
                            GeneratedBlock b = g.Generate(i, this.m_GlobalX + x, this.m_GlobalY + y);
                            if (b != null)
                                this.m_Blocks[x, y] = b;
                        }
                        int percent = (int)((double)(y + (x * Chunk.Height)) / (double)total * 100);
                        if (lastPercent < percent)
                        {
                            Log.WriteLine("... " + percent.ToString() + "% (" + (y + (x * Chunk.Height)).ToString() + "/" + total.ToString() + ") complete.");
                            lastPercent = percent;
                        }
                    }*/
            });
            t.IsBackground = true;
            t.Start();
        }

        /*public GeneratedBlock GetBlockAt(int x, int y)
        {
            if (this.m_Generating)
                return null;
            if (!this.m_Generated)
            {
                this.Generate();
                return null;
            }
            return this.m_Blocks[x, y];
        }*/
    }

    public class ChunkInfo
    {
        public ChunkInfo(HashSet<IPositionable> uniques)
        {
            this.Objects = new List<object>();
            this.Uniques = uniques;
        }

        public int Seed
        {
            get;
            set;
        }

        public Random Random
        {
            get;
            set;
        }

        public Rectangle Bounds
        {
            get;
            set;
        }

        public List<object> Objects
        {
            get;
            private set;
        }

        public HashSet<IPositionable> Uniques
        {
            get;
            private set;
        }
    }
}
