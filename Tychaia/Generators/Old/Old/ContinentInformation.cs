using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tychaia.Generators
{
#if false
    public class ContinentInformation : IPositionable, IEquatable<ContinentInformation>
    {
        public ContinentInformation(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public bool Equals(ContinentInformation other)
        {
            return (this.X == other.X && this.Y == other.Y);
        }

        public override bool Equals(object obj)
        {
            return (obj is ContinentInformation && this.Equals(obj as ContinentInformation));
        }

        public override int GetHashCode()
        {
            return new Point(this.X, this.Y).GetHashCode();
        }

        public override string ToString()
        {
            return "Continent at: " + this.X + ", " + this.Y;
        }
    }
#endif
}
