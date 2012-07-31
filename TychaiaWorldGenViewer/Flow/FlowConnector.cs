using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace TychaiaWorldGenViewer.Flow
{
    [DataContract]
    public class FlowConnector
    {
        [DataMember]
        public FlowElement Owner
        {
            get;
            private set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public bool IsInput
        {
            get;
            set;
        }

        public bool IsOutput
        {
            get
            {
                return !this.IsInput;
            }
            set
            {
                this.IsInput = !value;
            }
        }

        public virtual FlowConnector[] ConnectedTo
        {
            get;
            set;
        }

        private float m_CenterZoomAdjustment = 1.0f;

        public Point Center
        {
            get
            {
                var ax = ((this.IsInput ? -this.InvalidationWidth : this.InvalidationWidth) / this.m_CenterZoomAdjustment);
                int idx = FlowElement.GetConnectorIndex(this.Owner, this);
                var xx = (this.IsInput ? this.Owner.X - CONNECTOR_PADDING - CONNECTOR_SIZE : this.Owner.X + this.Owner.Width + CONNECTOR_PADDING + CONNECTOR_SIZE);
                return new Point((int)(xx + ax + CONNECTOR_SIZE / 2), this.Owner.Y + idx * (CONNECTOR_SIZE + CONNECTOR_PADDING) + CONNECTOR_SIZE / 2);
            }
        }

        internal int InvalidationWidth
        {
            get;
            private set;
        }

        public FlowConnector(FlowElement owner, bool isInput)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            this.Owner = owner;
            this.IsInput = isInput;
        }

        public FlowConnector(FlowElement owner, string name, bool isInput)
            : this(owner, isInput)
        {
            this.Name = name;

            // Precalculate the invalidation width.
            this.InvalidationWidth = 200;
        }

        internal const int CONNECTOR_SIZE = 10;
        internal const int CONNECTOR_PADDING = 8;

        internal static void RenderTo(FlowConnector el, RenderAttributes re)
        {
            re.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            re.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            // If we are input, we draw on the left hand side of the flow element.
            int idx = FlowElement.GetConnectorIndex(el.Owner, el);
            int sx = (int)Math.Ceiling(re.Graphics.MeasureString(el.Name, re.Font).Width);
            float xx = (el.IsInput ? el.Owner.X - CONNECTOR_PADDING : el.Owner.X + el.Owner.Width + CONNECTOR_PADDING) * re.Zoom;
            float yy = (el.Owner.Y + idx * (CONNECTOR_SIZE + CONNECTOR_PADDING)) * re.Zoom;
            if (el.IsInput)
                re.Graphics.DrawString(el.Name, re.Font, SystemBrushes.ControlText, new PointF(xx - sx + CONNECTOR_PADDING * re.Zoom, yy));
            else
                re.Graphics.DrawString(el.Name, re.Font, SystemBrushes.ControlText, new PointF(xx, yy));
            el.InvalidationWidth = sx;
            el.m_CenterZoomAdjustment = re.Zoom;
            sx += CONNECTOR_PADDING;
            re.Graphics.DrawRectangle(Pens.Black, xx + (el.IsInput ? -sx : sx), yy, CONNECTOR_SIZE * re.Zoom, CONNECTOR_SIZE * re.Zoom);
            if (el.IsInput && el.ConnectedTo != null)
                foreach (FlowConnector ct in el.ConnectedTo)
                    re.Graphics.DrawLine(Pens.Blue, el.Center.Apply(re.Zoom), ct.Center.Apply(re.Zoom));
        }

        internal IEnumerable<Rectangle> GetConnectorRegionsToInvalidate()
        {
            foreach (FlowConnector ct in this.ConnectedTo)
            {
                Rectangle r = new Rectangle(
                    this.Center.X,
                    this.Center.Y,
                    ct.Center.X - this.Center.X,
                    ct.Center.Y - this.Center.Y);
                r.Inflate(r.Width > 0 ? 10 : -10, r.Height > 0 ? 10 : -10);
                yield return r;
            }
        }
    }
}
