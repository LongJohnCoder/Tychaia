using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tychaia.Generators
{
#if false
    public class ContinentVarianceGenerator : IGenerator
    {
        public GeneratedBlock Generate(ChunkInfo info, int x, int y)
        {
            // Check continents to see where we're inside one.
            foreach (Continent c in info.Objects.Where(obj => obj is Continent))
            {
                // Skip, we're not inside this.
                if (!(x >= c.X - c.Width && y >= c.Y - c.Height &&
                      x <= c.X + c.Width && y <= c.Y + c.Height))
                    return null;

                // Temporary.
                if (x >= c.X - c.Width && y >= c.Y - c.Height &&
                      x <= c.X + c.Width && y <= c.Y + c.Height)
                    return new GeneratedBlock { Type = GeneratedBlock.BlockType.Grass };

                // Calculate shape. 
                //int middleX = c.X;
                //int middleY = c.Y;

            }

            return null;
        }

        private class Pointer
        {
            private int m_CenterX;
            private int m_CenterY;
            private Random m_Random;
            public int X;
            public int Y;
            public double Direction;

            public Pointer(Random rand, int centerX, int centerY, int totalWidth, int totalHeight)
            {
                this.m_Random = rand;
                this.m_CenterX = centerX;
                this.m_CenterY = centerY;
                this.X = centerX - totalWidth / 2;
                this.Y = totalHeight;
                this.Direction = 270;
            }

            public void Move()
            {
                double diradjust = this.m_Random.NextGuassian(-20, 20);
                this.Direction += diradjust;
            }
        }
    }
#endif
}
