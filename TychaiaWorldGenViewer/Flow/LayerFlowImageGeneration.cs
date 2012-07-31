using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tychaia.ProceduralGeneration;
using System.Threading.Tasks;
using System.Threading;

namespace TychaiaWorldGenViewer.Flow
{
    public static class LayerFlowImageGeneration
    {
        private static SolidBrush m_UnknownAssociation = new SolidBrush(Color.FromArgb(63, 63, 63));

        public class ImageTask
        {
#if FALSE
            public int Progress;
#endif
            public bool HasResult;
            public Bitmap Result;

            public ImageTask()
            {       
#if FALSE
                this.Progress = 0;
#endif
                this.HasResult = false;
                this.Result = null;
            }
        }

        public delegate void ProgressCallback(int progress, Bitmap bitmap);
        
#if FALSE
        public static ImageTask RegenerateImageForLayerTask(FlowInterfaceControl fic, Layer l, int width, int height, Action act)
        {
            ImageTask it = new ImageTask();
            Thread t = new Thread(() =>
                {
                    ProgressCallback callback = (progress, bitmap) =>
                        {
                            it.Progress = progress;
                            if (bitmap != null)
                            {
                                it.HasResult = true;
                                it.Result = bitmap;
                            }
                            act();
                        };
                    RegenerateImageForLayer(fic, l, width, height, callback);
                });
            t.Start();
            return it;
        }
#endif

        public static ImageTask RegenerateImageForLayerSync(FlowInterfaceControl fic, Layer l, int width, int height, Action act)
        {
            ImageTask it = new ImageTask();
            ProgressCallback callback = (progress, bitmap) =>
            {
#if FALSE
                it.Progress = progress;
#endif
                if (bitmap != null)
                {
                    it.HasResult = true;
                    it.Result = bitmap;
                }
                act();
            };
            RegenerateImageForLayer(fic, l, width, height, callback);
            return it;
        }

        public static int X
        {
            get;
            set;
        }

        public static int Y
        {
            get;
            set;
        }

        private static Bitmap RegenerateImageForLayer(FlowInterfaceControl fic, Layer l, int width, int height, ProgressCallback callback)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            Dictionary<int, Brush> brushes = l.GetLayerColors();
            int[] data = l.GenerateData(LayerFlowImageGeneration.X, LayerFlowImageGeneration.Y, width, height);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    while (true)
                    {
                        try
                        {
                            if (brushes != null && brushes.ContainsKey(data[x + y * width]))
                                g.FillRectangle(
                                    brushes[data[x + y * (width)]],
                                    new Rectangle(x, y, 1, 1)
                                    );
                            else
                                g.FillRectangle(
                                    m_UnknownAssociation,
                                    new Rectangle(x, y, 1, 1)
                                    );
#if FALSE
                            callback((int)((x + y * width) / (double)(width * height) * 100.0), null);
#else
                            callback(0, null);
#endif
                            break;
                        }
                        catch (InvalidOperationException)
                        {
                            // Graphics can be in use elsewhere, but we don't care; just try again.
                        }
                    }
                }
            callback(100, b);
            return b;
        }
    }
}
