using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tychaia.ProceduralGeneration;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TychaiaWorldGenViewer.Flow
{
    [DataContract]
    public class LayerFlowElement : FlowElement
    {
        [DataMember]
        private Layer m_Layer;
        private FlowInterfaceControl m_Control;
        private LayerFlowImageGeneration.ImageTask m_ImageTask = new LayerFlowImageGeneration.ImageTask();
        [DataMember]
        private List<FlowConnector> m_InputConnectors = new List<FlowConnector>();
        [DataMember]
        private List<FlowConnector> m_OutputConnectors = new List<FlowConnector>();
        private Bitmap m_CachedBitmap;

        public override Bitmap Image
        {
            get
            {
                if (this.m_ImageTask == null)
                    this.m_ImageTask = new LayerFlowImageGeneration.ImageTask();
                if (!this.m_ImageTask.HasResult)
                {
                    if (this.m_CachedBitmap == null)
                        return null;
#if FALSE
                    Graphics g = Graphics.FromImage(this.m_ProgressBitmap);
                    g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, this.ImageWidth, this.ImageHeight));
                    g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(10, 10, (int)((this.ImageWidth - 20) * (this.m_ImageTask.Progress / 100.0)), 10));
                    g.DrawString("Reprocessing..", SystemFonts.DefaultFont, SystemBrushes.ControlText, new PointF(10, 10));
#endif
                    return this.m_CachedBitmap;
                }
                else
                {
                    this.m_CachedBitmap = this.m_ImageTask.Result;
                    return this.m_ImageTask.Result;
                }
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        public LayerFlowElement(FlowInterfaceControl control, Layer l)
        {
            this.m_Control = control;
            this.m_Layer = l;
            this.Name = l.ToString();
            this.ImageWidth = 128;
            this.ImageHeight = 128;
            this.ObjectPropertyUpdated();

            // Create input / output connectors.
            foreach (string s in this.m_Layer.GetParentsRequired())
                this.m_InputConnectors.Add(new LayerFlowConnector(this, s, true, l));
            this.m_OutputConnectors.Add(new LayerFlowConnector(this, "Output", false, l));
        }

#if FALSE

        private void RefreshImage()
        {
            this.m_ImageTask = LayerFlowImageGeneration.RegenerateImageForLayerTask(this.m_Control, this.m_Layer, this.ImageWidth, this.ImageHeight, () =>
                {
                    this.m_Control.Invalidate(this.Region.Apply(this.m_Control.Zoom));
                }/*, () =>
                {
                    this.m_Control.Invalidate(this.InvalidatingRegion.Apply(this.m_Control.Zoom));
                }*/);
        }

#endif

        private void RefreshImageSync()
        {
            this.m_ImageTask = new LayerFlowImageGeneration.ImageTask();
            this.m_Control.Invalidate(this.Region.Apply(this.m_Control.Zoom));
            this.m_ImageTask = LayerFlowImageGeneration.RegenerateImageForLayerSync(this.m_Control, this.m_Layer, this.ImageWidth, this.ImageHeight, () =>
            {
            });
            this.m_Control.Invalidate(this.Region.Apply(this.m_Control.Zoom));
        }

        private int ParentsIndexOf(Layer find)
        {
            for (int i = 0; i < this.m_Layer.Parents.Length; i++)
                if (this.m_Layer.Parents[i] == find)
                    return i;
            return -1;
        }

        public override void SetDeserializationData(FlowInterfaceControl control)
        {
            this.m_Control = control;
        }

        public FlowConnector[] GetConnectorsForLayer(FlowConnector connector, bool isInput)
        {
            if (isInput)
                return this.m_Control.Elements
                        .Where(v => v is LayerFlowElement)
                        .Select(v => v as LayerFlowElement)
                        .Where(v => this.m_Layer.Parents.Contains(v.m_Layer))
                        .Where(v => this.m_InputConnectors.IndexOf(connector) == this.ParentsIndexOf(v.m_Layer))
                        .Select(v => v.m_OutputConnectors[0])
                        .ToArray();
            else
            {
                IEnumerable<LayerFlowElement> lfe = this.m_Control.Elements
                        .Where(v => v is LayerFlowElement)
                        .Select(v => v as LayerFlowElement)
                        .Where(v => v.m_Layer.Parents.Contains(this.m_Layer));

                // TODO: Probably can be moved into LINQ query above.
                List<FlowConnector> fll = new List<FlowConnector>();
                foreach (LayerFlowElement el in lfe)
                {
                    for (int i = 0; i < el.m_InputConnectors.Count; i++)
                    {
                        if ((el.m_InputConnectors[i] as LayerFlowConnector).ConnectedTo.Contains(this.m_OutputConnectors[0]))
                        {
                            fll.Add(el.m_InputConnectors[i]);
                        }
                    }
                }
                return fll.ToArray();
            }
        }

        public void SetConnectorsForLayer(LayerFlowConnector connector, FlowConnector[] targets, bool isInput)
        {
            if (isInput)
            {
                // We are an input connector, we must clear our layer's current
                // parent and set it to the new value.
                if (targets.Length != 1)
                    throw new InvalidOperationException("An input can not be connected to more than one output.");
                this.m_Layer.SetParent(
                    this.m_InputConnectors.IndexOf(connector),
                    (targets[0].Owner as LayerFlowElement).m_Layer
                );
                this.ObjectPropertyUpdated();
            }
            else
            {
                // We are an output connector, we must add ourselves as the target's
                // parent.  We can do this as a reverse operation on our targets.
                foreach (FlowConnector t in targets)
                {
                    (t.Owner as LayerFlowElement).SetConnectorsForLayer(
                        t as LayerFlowConnector,
                        new LayerFlowConnector[] { connector },
                        true);
                }
            }

            // Invalidate the control area.
            foreach (Rectangle r in this.GetConnectorRegionsToInvalidate())
                this.m_Control.Invalidate(r);
        }

        public override object GetObjectToInspect()
        {
            return this.m_Layer;
        }

        public override void ObjectPropertyUpdated()
        {
            this.m_Control.PushForReprocessing(this);

            // Update children.
            foreach (FlowConnector output in this.m_OutputConnectors)
            {
                FlowConnector[] children = this.GetConnectorsForLayer(output, false);
                foreach (FlowConnector fc in children)
                {
                    if (fc is LayerFlowConnector)
                    {
                        ((fc as LayerFlowConnector).Owner as LayerFlowElement).ObjectPropertyUpdated();
                    }
                }
            }
        }

        public override void ObjectReprocessRequested()
        {
            this.RefreshImageSync();
        }

        #region Overridden Properties

        public override List<FlowConnector> InputConnectors
        {
            get
            {
                return this.m_InputConnectors;
            }
        }

        public override List<FlowConnector> OutputConnectors
        {
            get
            {
                return this.m_OutputConnectors;
            }
        }

        #endregion
    }
}
