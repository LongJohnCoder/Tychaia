using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protogame.Noise;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Protogame;
using Microsoft.Xna.Framework;

namespace Tychaia.Generators
{
    public class StateValidationGenerator : IGenerator
    {
        public void Generate(Block[,,] blocks, ChunkInfo info)
        {
            while (RuntimeGame.DeviceForStateValidationOutput == null)
                continue;

            lock (RuntimeGame.LockForStateValidationOutput)
            {
                Console.Write("Locked graphics.  Rendering graph to file...");
                RainfallInformation rainfall = info.Objects.First(val => val is RainfallInformation) as RainfallInformation;
                TemperatureInformation temperature = info.Objects.First(val => val is TemperatureInformation) as TemperatureInformation;
                PresentationParameters pp = RuntimeGame.DeviceForStateValidationOutput.PresentationParameters;
                RenderTarget2D renderTarget = new RenderTarget2D(RuntimeGame.DeviceForStateValidationOutput, 200, 200, true, RuntimeGame.DeviceForStateValidationOutput.DisplayMode.Format, DepthFormat.Depth24);
                RuntimeGame.DeviceForStateValidationOutput.SetRenderTarget(renderTarget);
                RuntimeGame.ContextForStateValidationOutput.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);//, SaveStateMode.None, Matrix.Identity);
                //graphics.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Point;
                //graphics.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Point;
                //graphics.GraphicsDevice.SamplerStates[0].MipFilter = TextureFilter.Point;

                XnaGraphics graphics = new XnaGraphics(RuntimeGame.ContextForStateValidationOutput);
                if (File.Exists("state.png"))
                {
                    using (StreamReader reader = new StreamReader("state.png"))
                    {
                        Texture2D tex = Texture2D.FromStream(RuntimeGame.DeviceForStateValidationOutput, reader.BaseStream);
                        RuntimeGame.ContextForStateValidationOutput.SpriteBatch.Draw(tex, new Rectangle(0, 0, 200, 200), Color.White);
                    }
                }
                else
                {
                    graphics.FillRectangle(0, 0, 200, 200, Color.Red);
                    graphics.FillRectangle(0, 0, 100, 100, Color.White);
                }

                for (int x = 0; x < info.Bounds.Width; x++)
                    for (int y = 0; y < info.Bounds.Height; y++)
                    {
                        int r = 100 - (int)(rainfall.Rainfall[x, y] * 100);
                        int t = 100 - (int)(temperature.Temperature[x, y] * 100);

                        graphics.DrawLine(r, t, r + 1, t + 1, 1, new Color(0f, 0f, 0f, 0.1f).ToPremultiplied());
                    }

                RuntimeGame.ContextForStateValidationOutput.SpriteBatch.End();
                RuntimeGame.DeviceForStateValidationOutput.SetRenderTarget(null);
                using (StreamWriter writer = new StreamWriter("state.png"))
                {
                    renderTarget.SaveAsPng(writer.BaseStream, 200, 200);
                }
                Console.WriteLine(" done.");
            }
        }
    }
}
