using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tychaia.Title
{
    public class TitleWorld : World
    {
        //private int m_Tick = 0;
        private List<Button> m_Buttons = new List<Button>();
        private static Random m_Random = new Random();
        public static int m_StaticSeed = 6294563;
        private World m_TargetWorld = null;

        public TitleWorld()
        {
            // TODO: Make buttons centered during update or w/e...
            int CX = 800;
            int OY = 300;
            this.m_Buttons.Add(new Button("Generate World", new Rectangle(CX - 100, OY, 200, 30), () =>
            {
                this.m_TargetWorld = new RPGWorld();
            }));
            this.m_Buttons.Add(new Button("Randomize Seed", new Rectangle(CX - 100, OY + 40, 200, 30), () =>
            {
                m_StaticSeed = m_Random.Next();
            }));
            this.m_Buttons.Add(new Button("Exit", new Rectangle(CX - 100, OY + 80, 200, 30), () =>
            {
                (this.Game as RuntimeGame).Exit();
            }));
        }

        public override bool Update(GameContext context)
        {
            if (this.m_TargetWorld != null)
            {
                (this.Game as RuntimeGame).SwitchWorld(this.m_TargetWorld);
                return false;
            }
            return true;
        }


        public override void DrawBelow(GameContext context)
        {
        }

        public override void DrawAbove(GameContext context)
        {
            XnaGraphics xna = new XnaGraphics(context);
            xna.DrawStringCentered(context.Camera.Width / 2, 50, "τυχαία", "TitleFont");
            xna.DrawStringCentered(context.Camera.Width / 2, 200, "(tychaía)", "SubtitleFont");

            MouseState state = Mouse.GetState();
            foreach (Button b in this.m_Buttons)
                b.Process(xna, state);

            xna.DrawStringCentered(context.Camera.Width / 2, 750, "Using static seed: " + m_StaticSeed.ToString(), "Arial");
        }

        private class Button
        {
            private string m_Text;
            private Rectangle m_Area;
            private Action m_OnClick;
            private double m_PulseValue;
            private bool m_PulseModeUp;

            public Button(string text, Rectangle area, Action onClick)
            {
                this.m_Text = text;
                this.m_Area = area;
                this.m_OnClick = onClick;
                this.m_PulseValue = m_Random.NextDouble();
            }

            public void Process(XnaGraphics xna, MouseState mouse)
            {
                if (this.m_PulseValue >= 1)
                    this.m_PulseModeUp = false;
                else if (this.m_PulseValue <= 0)
                    this.m_PulseModeUp = true;
                this.m_PulseValue += this.m_PulseModeUp ? 0.01 : -0.01;
                if (this.m_Area.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed)
                    this.m_OnClick();
                if (this.m_Area.Contains(mouse.X, mouse.Y))
                    xna.FillRectangle(this.m_Area, new Color(1f, 1f, 1f, 0.25f + (float)(this.m_PulseValue / 2.0)).ToPremultiplied());
                else
                    xna.FillRectangle(this.m_Area, new Color(1f, 1f, 1f, 0.1f + (float)(this.m_PulseValue / 32.0)).ToPremultiplied());
                xna.DrawStringCentered(this.m_Area.X + this.m_Area.Width / 2, this.m_Area.Y + 4, this.m_Text, "ButtonFont");
            }
        }
    }
}
