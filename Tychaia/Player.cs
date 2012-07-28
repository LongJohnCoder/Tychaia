using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;
using Microsoft.Xna.Framework.Input;

namespace Tychaia
{
    public class Player : ChunkEntity
    {
        public Player(World world) : base(world)
        {
            this.Images = this.GetTexture("chars.player.player");
            this.Width = 16;
            this.Height = 16;
        }
    }
}
