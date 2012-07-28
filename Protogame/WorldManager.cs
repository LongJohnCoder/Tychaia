using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Protogame.Particles;
using Protogame.RTS;

namespace Protogame
{
    public class WorldManager
    {
        private void DrawSpriteAt(GameContext context, float x, float y, int width, int height, string image, Color color, bool flipX)
        {
            if (flipX)
                context.SpriteBatch.Draw(
                    context.Textures[image],
                    new Rectangle((int)x, (int)y, width, height),
                    null,
                    color.ToPremultiplied(),
                    0,
                    new Vector2(0, 0),
                    SpriteEffects.FlipHorizontally,
                    0
                    );
            else
                context.SpriteBatch.Draw(
                    context.Textures[image],
                    new Rectangle((int)x, (int)y, width, height),
                    color.ToPremultiplied()
                    );
        }

        private void HandleRenderOfEntity(GameContext context, IEntity a)
        {
            this.DrawSpriteAt(context, (float)a.X, (float)a.Y, a.Width, a.Height, a.Image, a.Color, a.ImageFlipX);

            // Check to see if this entity is residing on an edge of the screen.
            /*if (a.X >= Tileset.TILESET_PIXEL_WIDTH) a.X -= Tileset.TILESET_PIXEL_WIDTH;
            if (a.X < 0) a.X += Tileset.TILESET_PIXEL_WIDTH;
            if (a.X > Tileset.TILESET_PIXEL_WIDTH - a.Width)
            {
                // Draw a mirror image on the left side of the screen.
                this.DrawSpriteAt(context, (float)a.X - Tileset.TILESET_PIXEL_WIDTH, (float)a.Y, a.Width, a.Height, a.Image, a.Color, a.ImageFlipX);
            }*/
        }

        private const int MAX_DRAW_DEPTH = 30;

        protected virtual void DrawTilesBelow(GameContext context)
        {
            // Render tiles.
            int c = 0;
            for (int z = Math.Max((byte)0, context.World.RenderDepthValue); z < Math.Min(context.World.RenderDepthValue + context.World.RenderDepthDownRange, 63); z++)
            {
                for (int x = Math.Min(Math.Max(0, context.Camera.X / Tileset.TILESET_CELL_WIDTH), Tileset.TILESET_WIDTH);
                            x <= Math.Min((context.Camera.X + context.Camera.Width) / Tileset.TILESET_CELL_WIDTH,
                                            Tileset.TILESET_WIDTH);
                            x += 1)
                {
                    for (int y = Math.Min(Math.Max(0, context.Camera.Y / Tileset.TILESET_CELL_HEIGHT), Tileset.TILESET_HEIGHT);
                            y <= Math.Min((context.Camera.Y + context.Camera.Height) / Tileset.TILESET_CELL_HEIGHT,
                                            Tileset.TILESET_HEIGHT);
                            y += 1)
                    {
                        Tile p = context.World.Tileset[x, y, z - 1];
                        if (z != context.World.RenderDepthValue && p != null && !(p is TransparentTile))
                            continue;
                        Tile t = context.World.Tileset[x, y, z];
                        if (t == null) continue;
                        if (t.Image == null) continue;
                        float f = ((context.World.RenderDepthDownRange - c) / (float)context.World.RenderDepthDownRange);
                        context.SpriteBatch.Draw(
                            context.Textures[t.Image],
                            new Rectangle(x * Tileset.TILESET_CELL_WIDTH, y * Tileset.TILESET_CELL_HEIGHT, t.Width, t.Height),
                            null,
                            new Color(1f, 1f, 1f, f).ToPremultiplied()
                            );
                    }
                }
                c++;
            }
        }

        protected virtual void DrawTilesAbove(GameContext context)
        {
            // Render tiles above.
            int c = 0;
            for (int z = Math.Max(context.World.RenderDepthValue - context.World.RenderDepthUpRange, 0); z < context.World.RenderDepthValue; z++)
            {
                for (int x = Math.Min(Math.Max(0, context.Camera.X / Tileset.TILESET_CELL_WIDTH), Tileset.TILESET_WIDTH);
                            x <= Math.Min((context.Camera.X + context.Camera.Width) / Tileset.TILESET_CELL_WIDTH,
                                            Tileset.TILESET_WIDTH);
                            x += 1)
                {
                    for (int y = Math.Min(Math.Max(0, context.Camera.Y / Tileset.TILESET_CELL_HEIGHT), Tileset.TILESET_HEIGHT);
                            y <= Math.Min((context.Camera.Y + context.Camera.Height) / Tileset.TILESET_CELL_HEIGHT,
                                            Tileset.TILESET_HEIGHT);
                            y += 1)
                    {
                        Tile t = context.World.Tileset[x, y, z];
                        if (t == null) continue;
                        if (t.Image == null) continue;
                        float f = ((context.World.RenderDepthUpRange - c) / (float)context.World.RenderDepthUpRange);
                        context.SpriteBatch.Draw(
                            context.Textures[t.Image],
                            new Rectangle(x * Tileset.TILESET_CELL_WIDTH, y * Tileset.TILESET_CELL_HEIGHT, t.Width, t.Height),
                            null,
                            new Color(1f, 1f, 1f, f * 0.4f).ToPremultiplied()
                            );
                    }
                }
                c++;
            }
        }

        public void Draw(GameContext context)
        {
            // Clear the screen.
            context.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, context.Camera.GetTransformationMatrix());

            // Draw world below.
            context.World.DrawBelow(context);

            // Render tiles below.
            if (context.World.Tileset != null)
                this.DrawTilesBelow(context);

            // Render all of the actors.
            foreach (IEntity a in context.World.Entities)
                if ((a is ParticleEntity) && (a as ParticleEntity).Definition.RenderMode == ParticleMode.Background)
                    this.HandleRenderOfEntity(context, a);
            foreach (IEntity a in context.World.Entities)
                if (a.Image != null && !(a is ParticleEntity) && (!(a is IDynamicRenderingEntity) || (a as IDynamicRenderingEntity).ShouldRender(context.World)))
                    this.HandleRenderOfEntity(context, a);
            foreach (IEntity a in context.World.Entities)
                if ((a is ParticleEntity) && (a as ParticleEntity).Definition.RenderMode == ParticleMode.Foreground)
                    this.HandleRenderOfEntity(context, a);
            XnaGraphics gr = new XnaGraphics(context);
            foreach (IEntity a in context.World.Entities)
                if (!(a is IDynamicRenderingEntity) || (a as IDynamicRenderingEntity).ShouldRender(context.World))
                    a.Draw(context.World, gr);

            // Render tiles above.
            if (context.World.Tileset != null)
                this.DrawTilesAbove(context);

            // Draw world above.
            context.World.DrawAbove(context);

            // Finish rendering.
            context.SpriteBatch.End();
        }

        public void Update(GameContext context)
        {
            bool handle = context.World.Update(context);
            if (!handle) return;

            // Update all of the actors.
            foreach (IEntity a in context.World.Entities.ToArray())
                a.Update(context.World);

            // Update tick.
            context.World.Tick += 1;
        }
    }
}
