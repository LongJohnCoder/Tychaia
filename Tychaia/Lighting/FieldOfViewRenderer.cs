using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;
using Microsoft.Xna.Framework;

namespace Tychaia.Lighting
{
    public class FieldOfViewRenderer
    {
        private FieldOfView m_FieldOfView = null;

        public FieldOfViewRenderer(FieldOfView fov)
        {
            this.m_FieldOfView = fov;
        }

        private const int Seperation = FieldOfView.BROADPHASE_CELL * Tileset.TILESET_CELL_WIDTH;

        public void Render(GameContext context, XnaGraphics xna)
        {
            Point center = this.m_FieldOfView.World.GetCenter();

            for (int x = 0; x < this.m_FieldOfView.BroadphaseGrid.GetLength(0); x++)
                for (int y = 0; y < this.m_FieldOfView.BroadphaseGrid.GetLength(1); y++)
                {
                    if (!this.m_FieldOfView.BroadphaseGrid[x, y])
                        xna.FillRectangle(new Rectangle(x * Seperation, y * Seperation, Seperation, Seperation), new Color(0f, 0f, 0f, 0.4f).ToPremultiplied());
                    if (x == center.X && y == center.Y)
                        xna.FillRectangle(new Rectangle(x * Seperation, y * Seperation, Seperation, Seperation), new Color(1f, 0f, 0f, 1).ToPremultiplied());
                    //xna.DrawStringLeft(x * Seperation, y * Seperation, this.m_FieldOfView.m_BroadphaseHeightGrid[x, y].ToString());
                    int deg = (int)MathHelper.ToDegrees(this.m_FieldOfView.m_BroadphaseAngleGrid[x, y]);
                    xna.DrawStringLeft(x * Seperation, y * Seperation, deg.ToString(), "SmallArial");
                    /*if (deg < -100)
                        xna.DrawStringLeft(x * Seperation, y * Seperation, Math.Abs(deg + 100).ToString());
                    else if (deg < 0)
                        xna.DrawStringLeft(x * Seperation, y * Seperation, Math.Abs(deg).ToString());
                    else if (deg < 100)
                        xna.DrawStringLeft(x * Seperation, y * Seperation, (deg).ToString());
                    else if (deg < 200)
                        xna.DrawStringLeft(x * Seperation, y * Seperation, (deg - 100).ToString());*/
                }
        }
    }
}
