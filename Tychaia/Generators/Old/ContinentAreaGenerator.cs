using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;
using Microsoft.Xna.Framework;

namespace Tychaia.Generators
{
#if false
    public class ContinentAreaGenerator : IGenerator
    {
        public GeneratedBlock Generate(ChunkInfo info, int x, int y)
        {
            // Create 20 continents.
            for (int i = 0; i < 5; i += 1)
            {
                int cx = info.Random.Next(-3000, 3000);
                int cy = info.Random.Next(-3000, 3000);
                int cw = (int)info.Random.NextGuassian(3000, 4000);
                int ch = (int)info.Random.NextGuassian(3000, 4000);
                info.Objects.Add(new Continent(cx, cy, cw, ch));
                info.Uniques.Add(new ContinentInformation(cx, cy));
            }

            // Don't set anything yet.
            return null;
        }
    }

    public class Continent
    {
        public Continent(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Patches = new List<Rectangle>();
        }

        public int X
        {
            get;
            private set;
        }

        public int Y
        {
            get;
            private set;
        }

        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public List<Rectangle> Patches
        {
            get;
            private set;
        }
    }
#endif
}
