using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tychaia.ProceduralGeneration;
using System.Runtime.Serialization;

namespace TychaiaWorldGenViewer.Flow
{
    [DataContract]
    public class LayerFlowConnector : FlowConnector
    {
        public override FlowConnector[] ConnectedTo
        {
            get
            {
                return this.m_LayerOwner.GetConnectorsForLayer(this, this.IsInput);
            }
            set
            {
                this.m_LayerOwner.SetConnectorsForLayer(this, value, this.IsInput);
            }
        }

        [DataMember]
        private Layer m_Layer;

        [DataMember]
        private LayerFlowElement m_LayerOwner;

        public LayerFlowConnector(LayerFlowElement owner, string name, bool isInput, Layer layer)
            : base(owner, name, isInput)
        {
            this.m_Layer = layer;
            this.m_LayerOwner = owner;
        }
    }
}
