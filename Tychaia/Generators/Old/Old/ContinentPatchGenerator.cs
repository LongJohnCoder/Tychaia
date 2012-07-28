using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tychaia.Generators
{
#if false
    class ContinentPatchGenerator : IGenerator
    {
        public GeneratedBlock Generate(ChunkInfo info, int x, int y)
        {
            // Check continents to see where we're inside one.
            foreach (Continent c in info.Objects.Where(obj => obj is Continent))
            {
                // Determine sizes.
                int MaximumOutsidePatchDistance = 10;// (int)((c.Width / 3f - c.Height / 3f) / 2f);
                int MaximumPatchDistanceModifier = 10;// (int)(c.Width / 10f);
                int AveragePatchSize = 5;// (int)(30);
                int AveragePatchCount = c.Width;// / (AveragePatchSize / 3);

                // Skip, we're not inside this.
                if (x < c.X - MaximumOutsidePatchDistance || y < c.Y - MaximumOutsidePatchDistance ||
                    x > c.X + c.Width + MaximumOutsidePatchDistance || y > c.Y + c.Height + MaximumOutsidePatchDistance ||
                    !info.Bounds.Contains(x, y))
                {
                    for (int i = 0; i < info.Random.NextGuassian(0, 10); i++)
                        info.Random.NextGuassian(0, 10);
                    continue;
                }

                // If we are entirely inside the terrain and outside the effect of the
                // patch distance, instantly return a grass tile.
                if (x >= c.X && y >= c.Y &&
                    x <= c.X + c.Width && y <= c.Y + c.Height)
                    return new GeneratedBlock { Type = GeneratedBlock.BlockType.Grass };

                // Apply patches.
                int count = (int)info.Random.NextGuassian(0, AveragePatchCount * 2);
                for (int i = 0; i <= AveragePatchCount; i++)
                {
                    int w = (int)info.Random.NextGuassian(0, AveragePatchSize * 2);
                    int h = (int)info.Random.NextGuassian(0, AveragePatchSize * 2);
                    c.Patches.Add(new Rectangle(
                        c.X - w / 2,
                        (int)(c.Y + ((double)c.Height / (double)AveragePatchCount * i) + info.Random.NextGuassian(-MaximumPatchDistanceModifier, MaximumPatchDistanceModifier)) - h / 2,
                        w,
                        h
                        ));
                }
                for (int i = 0; i <= AveragePatchCount; i++)
                {
                    int w = (int)info.Random.NextGuassian(0, AveragePatchSize * 2);
                    int h = (int)info.Random.NextGuassian(0, AveragePatchSize * 2);
                    c.Patches.Add(new Rectangle(
                        c.X + c.Width - w / 2,
                        (int)(c.Y + ((double)c.Height / (double)AveragePatchCount * i) + info.Random.NextGuassian(-MaximumPatchDistanceModifier, MaximumPatchDistanceModifier)) - h / 2,
                        w,
                        h
                        ));
                }
                for (int i = 0; i <= AveragePatchCount; i++)
                {
                    int w = (int)info.Random.NextGuassian(0, AveragePatchSize * 2);
                    int h = (int)info.Random.NextGuassian(0, AveragePatchSize * 2);
                    c.Patches.Add(new Rectangle(
                        (int)(c.X + ((double)c.Width / (double)AveragePatchCount * i) + info.Random.NextGuassian(-MaximumPatchDistanceModifier, MaximumPatchDistanceModifier)) - w / 2,
                        c.Y - h / 2,
                        w,
                        h
                        ));
                }
                for (int i = 0; i <= AveragePatchCount; i++)
                {
                    int w = (int)info.Random.NextGuassian(0, AveragePatchSize * 2);
                    int h = (int)info.Random.NextGuassian(0, AveragePatchSize * 2);
                    c.Patches.Add(new Rectangle(
                        (int)(c.X + ((double)c.Width / (double)AveragePatchCount * i) + info.Random.NextGuassian(-MaximumPatchDistanceModifier, MaximumPatchDistanceModifier)) - w / 2,
                        c.Y + c.Height - h / 2,
                        w,
                        h
                        ));
                }

                for (int i = 0; i < c.Patches.Count; i++)
                    if (c.Patches[i].Contains(x, y))
                        return new GeneratedBlock { Type = i % 2 == 0 ? GeneratedBlock.BlockType.Grass : GeneratedBlock.BlockType.Water };
            }

            return null;
        }
    }
#endif
}
