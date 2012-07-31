using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TychaiaWorldGenViewer
{
    internal static class UsefulExtensions
    {
        internal static Point Apply(this Point p, float f)
        {
            return new Point((int)(p.X * f), (int)(p.Y * f));
        }

        internal static Rectangle Apply(this Rectangle p, float f)
        {
            return new Rectangle(
                (int)(p.X * f),
                (int)(p.Y * f),
                (int)(p.Width * f),
                (int)(p.Height * f));
        }
    }
}
