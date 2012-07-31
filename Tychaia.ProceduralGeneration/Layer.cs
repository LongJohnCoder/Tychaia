using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;

namespace Tychaia.ProceduralGeneration
{
    [DataContract]
    public abstract class Layer
    {
        [DataMember]
        private long m_Seed;

        [DataMember]
        private Layer[] m_Parents;

        public Layer[] Parents
        {
            get
            {
                return this.m_Parents;
            }
        }

        private void TransformSeed()
        {
            // Give this layer's seed some variance from the old parent, since otherwise
            // lower layers will be repeating the same values as the parent layer for cells.
            unchecked
            {
                this.m_Seed *= 2990430311017;
                this.m_Seed += 16099760261113;
            }
        }

        public void SetValues(Layer parent, int seed)
        {
            if (parent == null)
                this.m_Seed = seed;
            else
            {
                this.m_Parents = new Layer[1] { parent };
                this.m_Seed = this.m_Parents[0].m_Seed;
                this.TransformSeed();
            }
        }

        /// <summary>
        /// The world seed.
        /// </summary>
        protected long Seed
        {
            get
            {
                return this.m_Seed;
            }
        }
        
        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public Layer()
        {
        }

        protected Layer(int seed)
        {
            this.m_Parents = new Layer[] { };
            this.m_Seed = seed;
        }

        protected Layer(Layer parent)
        {
            this.m_Parents = new Layer[] { parent };
            if (parent != null)
            {
                this.m_Seed = parent.m_Seed;
                this.TransformSeed();
            }
        }

        protected Layer(Layer[] parents)
        {
            if (parents == null)
                throw new ArgumentNullException("parents");
            if (parents.Length < 1)
                throw new ArgumentOutOfRangeException("parents");
            this.m_Parents = parents;
            if (parents[0] != null)
            {
                this.m_Seed = parents[0].m_Seed;
                this.TransformSeed();
            }
        }

        protected Random GetCellRNG(int x, int y)
        {
            /* From: http://stackoverflow.com/questions/2890040/implementing-gethashcode
             * Although we aren't implementing GetHashCode, it's still a good way to generate
             * a unique number given a limited set of fields */
            unchecked
            {
                long seed = x * 3661988493967 + y;
                seed += x * 2990430311017;
                seed *= y * 14475080218213;
                seed -= y * 28124722524383;
                seed *= x * 16099760261113;
                seed += x * this.m_Seed;
                seed *= y * this.m_Seed;
                // Prevents the seed from being 0 along an axis.
                seed += (x - 199) * (y - 241) * 9018110272013;

                return new Random((int)seed);
            }
        }

        public abstract int[] GenerateData(int x, int y, int width, int height);

        public abstract Dictionary<int, System.Drawing.Brush> GetLayerColors();

        #region Designer Methods

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public virtual string[] GetParentsRequired()
        {
            return new string[] { "Parent" };
        }

        public void SetParent(int idx, Layer parent)
        {
            if (this.m_Parents.Length <= idx)
            {
                // Resize up.
                Layer[] newParents = new Layer[idx + 1];
                for (int i = 0; i < this.m_Parents.Length; i++)
                    newParents[i] = this.m_Parents[i];
                newParents[idx] = parent;
                this.m_Parents = newParents;
            }
            else
                this.m_Parents[idx] = parent;

            // Adjust seed if needed.
            if (idx == 0)
            {
                this.m_Seed = this.m_Parents[idx].m_Seed;
                this.TransformSeed();
            }
        }

        #endregion
    }
}
