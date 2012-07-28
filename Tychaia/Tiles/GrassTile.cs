using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;

namespace Tychaia.Tiles
{
    public class GrassTile : Tile
    {
        public GrassTile()
            : base()
        {
            this.Image = "tiles.grass";
            this.BackImage = "tiles.grass_back";
        }
    }
}
