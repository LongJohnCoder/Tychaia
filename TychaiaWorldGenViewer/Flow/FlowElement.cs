using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace TychaiaWorldGenViewer.Flow
{
    [DataContract]
    public abstract class FlowElement
    {
        #region General Properties

        [DataMember]
        public string Name
        {
            get;
            protected set;
        }

        [DataMember]
        public int X
        {
            get;
            set;
        }

        [DataMember]
        public int Y
        {
            get;
            set;
        }

        [DataMember]
        public int Width
        {
            get;
            protected set;
        }

        [DataMember]
        public int Height
        {
            get;
            protected set;
        }

        public virtual Bitmap Image
        {
            get;
            protected set;
        }

        public int ImageWidth
        {
            get
            {
                return this.Width - 2;
            }
            set
            {
                this.Width = value + 2;
            }
        }

        public int ImageHeight
        {
            get
            {
                return this.Height - 22;
            }
            set
            {
                this.Height = value + 22;
            }
        }

        public Rectangle Region
        {
            get
            {
                return new Rectangle(
                    this.X,
                    this.Y,
                    this.Width,
                    this.Height
                    );
            }
        }

        public Rectangle InvalidatingRegion
        {
            get
            {
                return new Rectangle(
                    this.X
                        - FlowConnector.CONNECTOR_PADDING * 2
                        - FlowConnector.CONNECTOR_SIZE
                        - (this.InputConnectors.Count == 0 ? 0 : this.InputConnectors.Max(v => v.InvalidationWidth)),
                    this.Y,
                    this.Width
                        + (FlowConnector.CONNECTOR_PADDING * 3 + FlowConnector.CONNECTOR_SIZE) * 2
                        + (this.InputConnectors.Count == 0 ? 0 : this.InputConnectors.Max(v => v.InvalidationWidth))
                        + (this.OutputConnectors.Count == 0 ? 0 : this.OutputConnectors.Max(v => v.InvalidationWidth)),
                    this.Height
                    );
            }
        }

        public Rectangle TitleRegion
        {
            get
            {
                return new Rectangle(
                    this.X,
                    this.Y,
                    this.Width,
                    20
                    );
            }
        }

        private static readonly List<FlowConnector> EmptyFlowConnectorList = new List<FlowConnector>();

        public virtual List<FlowConnector> InputConnectors
        {
            get
            {
                return EmptyFlowConnectorList;
            }
        }

        public virtual List<FlowConnector> OutputConnectors
        {
            get
            {
                return EmptyFlowConnectorList;
            }
        }

        #endregion

        #region Static Methods and Rendering

        private static SolidBrush m_TitleHighlight = new SolidBrush(Color.FromArgb(255, 255, 192));

        internal static void RenderTo(FlowElement el, RenderAttributes re, bool selected)
        {
            int ex = (int)(el.X * re.Zoom);
            int ey = (int)(el.Y * re.Zoom);
            int ew = (int)(el.Width * re.Zoom);
            int eh = (int)(el.Height * re.Zoom);
            int eiw = (int)Math.Floor(el.ImageWidth * re.Zoom);
            int eih = (int)Math.Floor(el.ImageHeight * re.Zoom);
            int etx = (int)((el.X + 4) * re.Zoom);
            int ety = (int)((el.Y + 4) * re.Zoom);

            re.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            re.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            re.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            re.Graphics.FillRectangle(selected ? m_TitleHighlight : SystemBrushes.Control, ex, ey, ew - 1 * re.Zoom, eh - 1 * re.Zoom);
            re.Graphics.DrawRectangle(Pens.Black, ex, ey, ew - 1 * re.Zoom, eh - 1 * re.Zoom);
            re.Graphics.DrawString(el.Name, re.Font, SystemBrushes.ControlText, new PointF(etx, ety));
            Image img = el.Image;
            if (img != null)
                re.Graphics.DrawImage(img, ex + 1 * re.Zoom, ey + 21 * re.Zoom, eiw, eih);

            foreach (FlowConnector fl in el.OutputConnectors)
                FlowConnector.RenderTo(fl, re);
            foreach (FlowConnector fl in el.InputConnectors)
                FlowConnector.RenderTo(fl, re);
        }

        #endregion

        internal static int GetConnectorIndex(FlowElement el, FlowConnector fl)
        {
            if (fl.IsInput)
                return el.InputConnectors.IndexOf(fl);
            else
                return el.OutputConnectors.IndexOf(fl);
        }

        internal IEnumerable<Rectangle> GetConnectorRegionsToInvalidate()
        {
            foreach (FlowConnector f in this.InputConnectors)
                foreach (Rectangle r in f.GetConnectorRegionsToInvalidate())
                    yield return r;
            foreach (FlowConnector f in this.OutputConnectors)
                foreach (Rectangle r in f.GetConnectorRegionsToInvalidate())
                    yield return r;
        }

        public virtual object GetObjectToInspect()
        {
            return null;
        }

        public virtual void ObjectPropertyUpdated()
        {
        }

        public virtual void ObjectReprocessRequested()
        {
        }

        public virtual void SetDeserializationData(FlowInterfaceControl control)
        {
        }
    }
}
