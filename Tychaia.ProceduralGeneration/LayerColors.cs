using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tychaia.ProceduralGeneration
{
    public static class LayerColors
    {
        public static Dictionary<int, Brush> ContinentBrushes = new Dictionary<int, Brush>
        {
            { 0  /* water   */, new SolidBrush(Color.FromArgb(0,0,255)) },
            { 1  /* grass   */, new SolidBrush(Color.FromArgb(0,255,0)) }
        };

        public static Dictionary<int, Brush> BiomeBrushes = new Dictionary<int, Brush>
        {
            { 0  /* water   */, new SolidBrush(Color.FromArgb(0,0,255)) },
            { 1  /* grass   */, new SolidBrush(Color.FromArgb(0,255,0)) },
            { 2  /* desert  */, new SolidBrush(Color.FromArgb(255,255,0)) },
            { 3  /* forest  */, new SolidBrush(Color.FromArgb(0,127,0)) },
            { 4  /* snow    */, new SolidBrush(Color.FromArgb(255,255,255)) },
            { 5  /* red     */, new SolidBrush(Color.FromArgb(255,0,0)) }
        };

        public static Dictionary<int, Brush> VoronoiBrushes = new Dictionary<int, Brush>
        {
            { 0  /* none     */, new SolidBrush(Color.FromArgb(63, 63, 63)) },
            { 1  /* original */, new SolidBrush(Color.FromArgb(255, 0, 0)) },
            { 2  /* vertex   */, new SolidBrush(Color.FromArgb(0, 255, 0)) },
            { 3  /* edge     */, new SolidBrush(Color.FromArgb(0, 0, 255)) },
        };

        /// <summary>
        /// Returns a list of brushes used as a gradient over between the minValue
        /// and maxValue parameters.
        /// </summary>
        /// <param name="minValue">The minimum value in the integer field.</param>
        /// <param name="maxValue">The maximum value in the integer field.</param>
        /// <returns></returns>
        public static Dictionary<int, Brush> GetGradientBrushes(int minValue, int maxValue)
        {
            Dictionary<int, Brush> brushes = new Dictionary<int, Brush>();
            for (int i = 0; i < maxValue - minValue; i++)
            {
                int a = (int)(256 * (i / (double)(maxValue - minValue)));
                brushes.Add(i + minValue, new SolidBrush(Color.FromArgb(a, a, a)));
            }
            return brushes;
        }
    }
}
